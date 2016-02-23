using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO.Pipes;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace FxSubmarineTaskbarApp
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MdfxApplicationContext(args));
        }
    }
    public class MdfxApplicationContext : ApplicationContext
    {
        private String[] args;
        private String strSessionId;
        private Form1 form1;

        private String AccountServer, AccountName, AccountNumber, License;
        private bool IsTesting;

        public MdfxApplicationContext(String[] args)
        {
            this.args = args;
            if (args.Length > 0)
            {
                strSessionId = args[0];
            }
            else
            {
                strSessionId = null;
            }
            form1 = new Form1();

            var t = new Thread(RunProgram);
            t.IsBackground = true;
            t.Start();
        }

        void RunProgram()
        {
            try {
                NamedPipeClientStream pipeClient;
                BinaryReader reader;
                BinaryWriter writer;
                BinaryFormatter bformatter;
                if (strSessionId != null)
                {
                    pipeClient = new NamedPipeClientStream(strSessionId);
                    writer = new BinaryWriter(pipeClient);
                    reader = new BinaryReader(pipeClient);
                    bformatter = new BinaryFormatter();

                    pipeClient.Connect();

                    AccountServer = reader.ReadString();
                    AccountName = reader.ReadString();
                    AccountNumber = reader.ReadString();
                    License = reader.ReadString();
                    IsTesting = reader.ReadBoolean();
                }
                else
                {
                    pipeClient = null;
                    writer = null;
                    reader = null;
                    bformatter = null;

                    AccountServer = "not connected";
                    AccountName = "not connected";
                    AccountNumber = "not connected";
                    License = "not connected";
                    IsTesting = false;
                }

                Console.WriteLine("Loading...");
                Console.WriteLine("AccountServer:" + AccountServer);
                Console.WriteLine("AccountName:" + AccountName);
                Console.WriteLine("AccountNumber:" + AccountNumber);
                Console.WriteLine("License:" + License);
                Console.WriteLine("IsTesting:" + IsTesting);

                form1.licenseTextBox.Text = License;

                if (strSessionId != null)
                {

                    int subsqerr = 0;
                    while (true)
                    {
                        try
                        {
                            int methodCode = reader.ReadInt32();
                            switch (methodCode)
                            {
                                case -1:
                                    Console.WriteLine("Robot finished. Exiting...");
                                    Thread.Sleep(10000);
                                    form1.Exit();
                                    subsqerr = 0;
                                    break;
                                case -2:
                                    String message = reader.ReadString();
                                    Console.WriteLine(message);
                                    subsqerr = 0;
                                    break;
                                default:
                                    int time = reader.ReadInt32();
                                    var date = ConvertUnixEpochTime(time);
                                    form1.timeLabel.Text = date.ToString("G");
                                    subsqerr = 0;
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            subsqerr++;
                            if (subsqerr == 10)
                            {
                                throw e;
                            }
                            Console.WriteLine("Exception:");
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Not connected. Exiting...");
                    Thread.Sleep(10000);
                    form1.Exit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Exiting...");
                Thread.Sleep(10000);
                form1.Exit();
            }
        }

        private DateTime ConvertUnixEpochTime(long seconds)
        {
            DateTime Fecha = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Fecha.ToLocalTime().AddSeconds(seconds);
        }
    }
}
