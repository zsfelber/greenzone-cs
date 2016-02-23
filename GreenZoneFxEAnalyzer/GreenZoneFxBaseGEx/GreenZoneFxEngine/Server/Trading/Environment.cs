using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Text;
using GreenZoneUtil.Util;
using System.ComponentModel;
using System.Runtime.Serialization;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{


    [Serializable]
    public class EAnalyzerOptions : EAnalyzerOptionsBase
    {

        private EAnalyzerOptions(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        protected EAnalyzerOptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static EAnalyzerOptions Load(GreenRmiManager rmiManager, bool useAsSingleton = false)
        {
            string eaOptDir = GreenZoneSysUtilsBase.InstallDirectory + "\\config";
            Directory.CreateDirectory(eaOptDir);
            string eaOptFile = eaOptDir + "\\eanalyzer.options";

            EAnalyzerOptions result = null;
            if (File.Exists(eaOptFile))
            {
                try
                {
                    FileStream eaOptStr = null;
                    GZipStream zipStr = null;
                    try
                    {
                        eaOptStr = File.Open(eaOptFile, FileMode.Open, FileAccess.Read);
                        zipStr = new GZipStream(eaOptStr, CompressionMode.Decompress);
                        StreamingContext sc = new StreamingContext(StreamingContextStates.Persistence, rmiManager);
                        BinaryFormatter deserializer = new BinaryFormatter();
                        deserializer.Context = sc;
                        result = (EAnalyzerOptions)deserializer.Deserialize(zipStr);
                    }
                    finally
                    {
                        if (zipStr != null)
                            zipStr.Close();
                        if (eaOptStr != null)
                            eaOptStr.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\nUnable to load options.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (useAsSingleton)
            {
                if (result == null)
                {
                    result = new EAnalyzerOptions(rmiManager);
                    result.EAnalyzerDirectory = new SelectableDir(GreenZoneSysUtilsBase.UserHomeDirectory + "\\GreenZoneFx\\EAnalyzer\\environments");
                    result.BufferSize = 10000;

                    result.Save();
                }
                Singleton = result;
            }
            return result;
        }

        public void Save()
        {
            FileStream eaOptStr = null;
            GZipStream zipStr = null;
            try
            {
                string eaOptDir = GreenZoneSysUtilsBase.InstallDirectory + "\\config";
                string eaOptFile = eaOptDir + "\\eanalyzer.options";

                eaOptStr = File.Open(eaOptFile, FileMode.Create, FileAccess.Write);
                zipStr = new GZipStream(eaOptStr, CompressionMode.Compress);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(zipStr, this);
            }
            finally
            {
                if (zipStr != null)
                    zipStr.Close();
                if (eaOptStr != null)
                    eaOptStr.Close();
            }
        }


        public static EAnalyzerOptions Singleton
        {
            get;
            private set;
        }

        [Browsable(false)]
        public override string DefaultEnvironment
        {
            get
            {
                string defaultEnvironment = base.DefaultEnvironment;
                if (defaultEnvironment != null && !EnvironmentRuntimeRoot.Singleton.environments.ContainsKey(defaultEnvironment))
                {
                    defaultEnvironment = null;
                }
                base.DefaultEnvironment = defaultEnvironment;
                return defaultEnvironment;
            }
            set
            {
                base.DefaultEnvironment = value;
            }
        }

    }




    public class EnvironmentRuntimeRoot
    {
        internal readonly Dictionary<string, ServerEnvironmentRuntime> environments = new Dictionary<string, ServerEnvironmentRuntime>();

        GreenRmiManager rmiManager;

        public static void Init(GreenRmiManager rmiManager)
        {
            Singleton = new EnvironmentRuntimeRoot(rmiManager);
            Singleton.LoadAll();
        }

        EnvironmentRuntimeRoot(GreenRmiManager rmiManager)
        {
            this.rmiManager = rmiManager;
        }

        void LoadAll()
        {
            string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
            Directory.CreateDirectory(eaRootDir);

            int pathlen = eaRootDir.Length + 1;
            string[] envDirs = Directory.GetDirectories(eaRootDir);
            if (envDirs != null)
            {
                foreach (var envDir in envDirs)
                {
                    string envId = envDir.Substring(pathlen);

                    ServerEnvironmentRuntime e = ServerEnvironmentRuntime.Load(rmiManager, envId);
                    if (e == null)
                    {
                        continue;
                    }
                    environments[envId] = e;
                    ServerEnvironmentSession s = ServerEnvironmentSession.Load(rmiManager, envId);
                    if (s == null)
                    {
                        s = new ServerEnvironmentSession(rmiManager, envId);
                    }
                    e.Session = s;
                }
            }
        }

        internal void SaveAll()
        {
            foreach (string envId in Environments)
            {
                var env = environments[envId];

                env.Save();
            }
        }

        public static EnvironmentRuntimeRoot Singleton
        {
            get;
            private set;
        }

        public ISet<string> Environments
        {
            get
            {
                SortedSet<string> result = new SortedSet<string>(environments.Keys);
                return result; 
            }
        }

        public ServerEnvironmentRuntime GetEnvironment(string envId)
        {
            return environments[envId];
        }

        public ServerEnvironmentRuntime AddEnvironment(string envId, 
            string ImportedEnvVersion, string ImportedDir, 
            string HistoryDirectory, params string[] ImportedParameters)
        {
            ServerEnvironmentRuntime environment = new ServerEnvironmentRuntime(rmiManager, envId);
            environments[envId] = environment;

            if (ImportedEnvVersion.Equals("Metatrader 4"))
            {
                environment.EnvironmentType = EnvironmentType.METATRADER4_OFFLINE;
            }
            else if (ImportedEnvVersion.Equals("Metatrader 5"))
            {
                environment.EnvironmentType = EnvironmentType.METATRADER5_OFFLINE;
            }
            else
            {
                environment.EnvironmentType = EnvironmentType.DUKASCOPY_TICKDATA_OFFLINE;
            }

            environment.ImportedFromDirectory = ImportedDir;
            environment.HistoryDirectory = HistoryDirectory;
            environment.AccountServer = ImportedParameters[0];
            environment.AccountCompany = ImportedParameters[1];
            environment.AccountName = ImportedParameters[2];
            environment.AccountNumber = Convert.ToInt32(ImportedParameters[3]);
            environment.AccountCurrency = ImportedParameters[4];
            environment.AccountLeverage = Convert.ToInt32(ImportedParameters[5]);
            environment.AccountFreeMarginMode = Convert.ToInt32(ImportedParameters[6]);
            environment.AccountStopoutLevel = Convert.ToInt32(ImportedParameters[7]);
            environment.AccountStopoutMode = Convert.ToInt32(ImportedParameters[8]);

            return environment;
        }
    }

}
