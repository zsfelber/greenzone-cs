using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Data.Entity;
using Submarine.Base;
using System.Net.Sockets;
using System.Net.Security;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Linq.Expressions;
using System.Data.Linq;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Linq.Dynamic;

namespace Submarine.LicenseServer
{
    public class LicenseServerLauncher
    {
        static LicenseServerApplicationContext context;
        public static void Start()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(context = new LicenseServerApplicationContext());
        }
        public static void Exit()
        {
            context.Exit();
        }
    }

    public class LicenseServerApplicationContext : ApplicationContext
    {
        private Form1 form1;

        public LicenseServerApplicationContext()
        {
            form1 = new Form1();
            form1.AppContext = this;
            Start();
        }

        internal bool Running { get; set; }

        internal SslTcpServer.Session Session { get; set; }

        internal void Start()
        {
            var t = new Thread(RunProgram);
            t.IsBackground = true;
            Running = true;
            form1.UpdateIsRunning();
            t.Start();
        }

        internal void Stop()
        {
            Running = false;
            if (Session != null)
                Session.Listener.Stop();
            // ...
            form1.UpdateIsRunning();
        }

        void RunProgram()
        {
            try
            {

                int port;
                int.TryParse(form1.portTextBox.Text, out port);
                Session = SslTcpServer.RunServer("VM114070.tradersvps.net.cer", port);
                Console.WriteLine("Waiting for a client to connect...");

                while (true)
                {
                    new ClientConnection(this);
                }
            }
            catch (InvalidOperationException e)
            {
                if (Running)
                {
                    Console.WriteLine("Exception:");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Exit();
                }
                else
                {
                    Console.WriteLine("InvalidOperationException : no error : service stopped");
                }
            }
            catch (SocketException e)
            {
                if (Running)
                {
                    Console.WriteLine("Exception:");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Exit();
                }
                else
                {
                    Console.WriteLine("SocketException : no error : service stopped");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Exit();
            }
        }
        internal void Exit()
        {
            Console.WriteLine("Exiting...");
            Stop();
            Thread.Sleep(10000);
            form1.Exit();
        }
    }

    internal class ClientConnection
    {
        SslTcpServer.Connection connection;
        BinaryFormatter bformatter;
        byte[] buffer = { 0 };

        internal ClientConnection(LicenseServerApplicationContext AppContext) {
            this.AppContext = AppContext;
            this.UserLoggedIn = new UserRow();
            bformatter = new BinaryFormatter();

            connection = new SslTcpServer.Connection(AppContext.Session);
            Console.WriteLine("New client connection : " + this);

            connection.SslStream.BeginRead(buffer, 0, 1, ReadCallback, null);
        }

        void Close()
        {
            Console.WriteLine("Closing client connection : " + this);
            connection.Close();
        }

        internal LicenseServerApplicationContext AppContext { get; set; }

        internal UserRow UserLoggedIn { get; set; }

        void ReadCallback(IAsyncResult ar)
        {
            try
            {
                connection.SslStream.EndRead(ar);
                if (buffer[0] >= Enum.GetNames(typeof(ServerOp)).Length)
                {
                    Console.WriteLine(this + " invalid opcode:" + buffer[0]+"  Closing connection...");
                    Close();
                    return;
                }
                ServerOp opcode = (ServerOp)buffer[0];
                buffer[0] = 10;
                Console.WriteLine(this + " opcode:" + opcode);

                switch (opcode)
                {
                    case ServerOp.LOG_OUT:
                        Close();
                        return;
                    case ServerOp.AUTHENTICATE:
                        {
                            UserRow user = (UserRow)bformatter.Deserialize(connection.SslStream);
                            try
                            {
                                UserRow result = ServerUtil.Authenticate(user);

                                UserLoggedIn = result;
                                Console.WriteLine(this + "  Authenticated.");
                            }
                            catch (InvalidOperationException e)
                            {
                                UserLoggedIn = new UserRow();
                                Console.WriteLine(this + "  Authentication failed :  " + user.LoginId);
                            }
                            bformatter.Serialize(connection.SslStream, UserLoggedIn);

                            // !
                            connection.SslStream.BeginRead(buffer, 0, 1, ReadCallback, null);
                            return;
                        }
                    default:
                        if (UserLoggedIn == null)
                        {
                            Console.WriteLine(this + "  Not authenticated!  Closing connection...");
                            bformatter.Serialize(connection.SslStream, new List<SoftwareRow>());
                            Close();
                            return;
                        }

                        break;
                }

                int selectedItemId = (int)bformatter.Deserialize(connection.SslStream);
                int from = (int)bformatter.Deserialize(connection.SslStream);
                int count = (int)bformatter.Deserialize(connection.SslStream);
                string orderBy = (string)bformatter.Deserialize(connection.SslStream);
                Console.WriteLine(this + "opcode:" + opcode + " selectedItemId:" + selectedItemId + " from:" + from + " count:" + count + " orderBy:" + orderBy);

                switch (opcode)
                {
                    case ServerOp.GET_OL_STATS:
                        {
                            bformatter.Serialize(connection.SslStream, ServerUtil.CreateOnlineStatsRow());

                            break;
                        }
                    case ServerOp.GET_SOFTWARES:
                        {
                            QueryResult<SoftwareRow> result = ServerUtil.GetSoftwares(selectedItemId, from, count, orderBy);
                            bformatter.Serialize(connection.SslStream, result);

                            break;
                        }
                    case ServerOp.GET_SOFTWARE_LICENSES:
                    case ServerOp.GET_SOFTWARE_LICENSES_OL:
                        {
                            int softwareId = (int)bformatter.Deserialize(connection.SslStream);
                            QueryResult<LicenseRow> result = ServerUtil.GetSoftwareLicenses(selectedItemId, softwareId, opcode, from, count, orderBy);
                            bformatter.Serialize(connection.SslStream, result);

                            break;
                        }
                    case ServerOp.GET_SOFTWARE_VERSIONS:
                    case ServerOp.GET_SOFTWARE_VERSIONS_OL:
                        {
                            int softwareId = (int)bformatter.Deserialize(connection.SslStream);
                            QueryResult<VersionRow> result = ServerUtil.GetSoftwareVersions(selectedItemId, softwareId, opcode, from, count, orderBy);
                            bformatter.Serialize(connection.SslStream, result);

                            break;
                        }
                    case ServerOp.GET_SOFTWARE_CUSTOMERS:
                        {
                            int softwareId = (int)bformatter.Deserialize(connection.SslStream);
                            QueryResult<CustomerRow> result = ServerUtil.GetSoftwareCustomers(selectedItemId, softwareId, from, count, orderBy);
                            bformatter.Serialize(connection.SslStream, result);

                            break;
                        }
                    case ServerOp.GET_LICENSE_SERVER_USERS:
                        {
                            QueryResult<UserRow> result = ServerUtil.GetLicenseServerUsers(selectedItemId, from, count, orderBy);
                            bformatter.Serialize(connection.SslStream, result);

                            break;
                        }
                }

                // !
                connection.SslStream.BeginRead(buffer, 0, 1, ReadCallback, null);

            
            }
            catch (InvalidOperationException e)
            {
                if (AppContext.Running)
                {
                    Console.WriteLine(this + "Exception:");
                    Console.WriteLine(this + e.Message);
                    Console.WriteLine(this + e.StackTrace);
                }
                else
                {
                    Console.WriteLine(this + "InvalidOperationException : no error : service stopped");
                }
                Close();
            }
            catch (SocketException e)
            {
                if (AppContext.Running)
                {
                    Console.WriteLine(this + "Exception:");
                    Console.WriteLine(this + e.Message);
                    Console.WriteLine(this + e.StackTrace);
                }
                else
                {
                    Console.WriteLine(this + "SocketException : no error : service stopped");
                }
                Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(this + "Exception:");
                Console.WriteLine(this + e.Message);
                Console.WriteLine(this + e.StackTrace);
                Close();
            }
        }


        public override string ToString() {
            string r = "";
            if (connection.Client!=null)
                r += connection.Client.Client.RemoteEndPoint;
            r += " user:" + UserLoggedIn.LoginId;
            return r;
        }
    }


    public partial class ServerUtil
    {
        public static SubmarineDbContext CONTEXT;

        public static void InitContextOnServer()
        {
            if (CONTEXT == null)
            {
                CONTEXT = new SubmarineDbContext();
            }
        }

        public static void CreateDemoDatabase()
        {
            InitContextOnServer();
            CONTEXT.Database.Delete();
            CONTEXT.SaveChanges();
            CONTEXT.Dispose();
            CONTEXT = null;


            InitContextOnServer();

            User user1 = new User(1, "Web User", "", "", BaseUtil.EncryptPassword("webuser"), BaseUtil.EncryptPassword("07856023547023230"), false);
            User user2 = new User(2, "James Sullivan", "themail00@gmail.com", "", "jsullivan", BaseUtil.EncryptPassword("start"), false);
            User user3 = new User(3, "William Morrison", "themail00@gmail.com", "", "wmorrison", BaseUtil.EncryptPassword("start"), true);
            user1.LastLoginTime = DateTime.Parse("11/01/2011");
            user1.LastLogoutTime = DateTime.Parse("11/02/2011");
            user2.LastLoginTime = DateTime.Parse("10/21/2011");
            user2.LastLogoutTime = DateTime.Parse("10/21/2011");
            user3.LastLoginTime = DateTime.Parse("11/04/2011");
            user3.LastLogoutTime = DateTime.Parse("11/04/2011");
            CONTEXT.Users.Add(user1);
            CONTEXT.Users.Add(user2);
            CONTEXT.Users.Add(user3);
            CONTEXT.SaveChanges();

            Software soft1 = new Software(1, "Magic Never Win Ea");
            Software soft2 = new Software(2, "Wonder Robot");
            soft1.UpdateLog = new UpdateLog(1, DateTime.Parse("11/01/2011"), user1, DateTime.Now, user2);
            soft2.UpdateLog = new UpdateLog(2, DateTime.Parse("01/03/2012"), user1, DateTime.Now, user3);
            CONTEXT.Softwares.Add(soft1);
            CONTEXT.Softwares.Add(soft2);
            CONTEXT.SaveChanges();

            Customer cus1 = new Customer(1, "Ben B", "themail@gmail.com", "0001221211222");
            cus1.UpdateLog = new UpdateLog(3, DateTime.Parse("11/02/2011"), user1, DateTime.Now, user2);
            CONTEXT.Customers.Add(cus1);
            CONTEXT.SaveChanges();

            License lic1 = new License(1, "CWE2323CDC11212000012", "", DateTime.Parse("2/16/2008"), DateTime.Parse("01/11/2012"));
            License lic2 = new License(2, "CWE2323CDC11212000013", "", DateTime.Parse("01/11/2012"), DateTime.Parse("01/01/2013"));
            lic1.LastLoginTime = DateTime.Parse("11/04/2011");
            lic1.LastLogoutTime = DateTime.Parse("11/01/2011");
            lic1.Customer = cus1;
            lic1.Software = soft1;
            lic1.UpdateLog = new UpdateLog(4, DateTime.Parse("10/02/2011"), user1, DateTime.Now, user2);
            lic2.LastLoginTime = DateTime.Parse("11/05/2011");
            lic2.LastLogoutTime = DateTime.Parse("11/02/2011");
            lic2.Customer = cus1;
            lic2.Software = soft1;
            lic2.UpdateLog = new UpdateLog(5, DateTime.Parse("01/13/2012"), user1, DateTime.Now, user3);
            CONTEXT.Licenses.Add(lic1);
            CONTEXT.Licenses.Add(lic2);
            CONTEXT.SaveChanges();

            Version ver1 = new Version(1, "1.0.0", DateTime.Parse("02/09/2012"), DateTime.Parse("01/12/2012"));
            Version ver2 = new Version(2, "1.0.1", DateTime.Parse("01/11/2012"), DateTime.MaxValue);
            Version ver3 = new Version(3, "1.0", DateTime.Parse("02/11/2012"), DateTime.MaxValue);
            ver1.Software = soft1;
            ver1.UpdateLog = new UpdateLog(6, DateTime.Parse("10/20/2011"), user1, DateTime.Now, user2);
            ver2.Software = soft1;
            ver2.UpdateLog = new UpdateLog(7, DateTime.Parse("10/21/2011"), user1, DateTime.Now, user3);
            ver3.Software = soft2;
            ver3.UpdateLog = new UpdateLog(8, DateTime.Parse("10/22/2011"), user1, DateTime.Now, user3);
            lic1.CurrentVersion = ver1;
            lic2.CurrentVersion = ver1;
            soft1.CurrentVersion = ver2;
            soft2.CurrentVersion = ver3;
            CONTEXT.Versions.Add(ver1);
            CONTEXT.Versions.Add(ver2);
            CONTEXT.Versions.Add(ver3);
            CONTEXT.SaveChanges();

            string dir = "E:\\downloads\\submarine_randompersons";
            string f = null;
            string l = null;
            try
            {
                // CreateTime|Name|Email|Phone|OriginalId|ValidFrom

                Console.WriteLine("Checking " + dir + "...");

                var cfs = from file in Directory.EnumerateFiles(dir, "*.csv")
                          select file;
                foreach (var cf in cfs)
                {
                    string contents = File.ReadAllText(cf);
                    contents = contents.Replace("\r\n", "\n");
                    contents = contents.Replace("\n", "\r\n");
                    File.WriteAllText(cf, contents);
                }

                bool valid = true;
                var clrecs = from file in Directory.EnumerateFiles(dir, "*.csv")
                             from line in File.ReadLines(file)
                             select new { file, line };
                foreach (var clrec in clrecs)
                {
                    if (!clrec.line.Equals(""))
                    {
                        string[] cols = clrec.line.Split('|');
                        if (cols.Length != 6) {
                            valid = false;
                            Console.WriteLine("error in " + clrec.file + " :");
                            Console.WriteLine(clrec.line);
                            Console.WriteLine();

                            Process p = new Process();
                            //p.StartInfo.UseShellExecute = false;
                            p.StartInfo.FileName = "L:\\Program Files\\Notepad++\\notepad++.exe";
                            p.StartInfo.Arguments = clrec.file;
                            p.Start();
                        }
                    }
                }

                if (!valid)
                {
                    return;
                }

                var files = from file in Directory.EnumerateFiles(dir, "*.csv")
                            select file;
                int i = 0;
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                    f = file;
                    var lines = from line in File.ReadLines(file)
                                select line;
                    int j = 0;
                    foreach (var line in lines)
                    {
                        l = line;
                        if (j != 0 && !l.Equals(""))
                        {
                            string[] cols = l.Split('|');
                            //CONTEXT.Database.ExecuteSqlCommand("SET IDENTITY_INSERT UpdateLog ON");
                            //CONTEXT.Database.ExecuteSqlCommand("SET IDENTITY_INSERT UpdateLog OFF");
                            CONTEXT.Database.ExecuteSqlCommand("insert into UpdateLogs (CreateTime,UpdateTime,CreatedBy_UserId,UpdatedBy_UserId) values ({0},{1},{2},{3})",
                                /*UpdateLogId: 2 * i + 9, */DateTime.Parse("11/02/2011"), DateTime.Now, user1.UserId, user2.UserId
                                );
                            CONTEXT.Database.ExecuteSqlCommand("insert into UpdateLogs (CreateTime,UpdateTime,CreatedBy_UserId,UpdatedBy_UserId) values ({0},{1},{2},{3})",
                                /*UpdateLogId: 2 * i + 10, */DateTime.Parse("11/02/2011"), DateTime.Now, user1.UserId, user2.UserId
                                );
                            CONTEXT.Database.ExecuteSqlCommand("insert into Customers (Name,Email,Phone,UpdateLog_UpdateLogId) values ({0},{1},{2},{3})",
                                /*CustomerId: i+2, */cols[1], cols[2], cols[3], 2 * i + 9
                                );
                            CONTEXT.Database.ExecuteSqlCommand("insert into Licenses (OriginalId,[Key],ValidFrom,ValidUntil,LastLoginTime,LastLogoutTime,SoftwareId,CustomerId,CurrentVersion_VersionId,UpdateLog_UpdateLogId) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})",
                                /*LicenseId: i + 3, */cols[4], "", DateTime.Parse(cols[5]), DateTime.Parse(cols[5]).AddMonths(6), DateTime.Now.AddHours(-1), DateTime.Now.AddHours(-14), soft2.SoftwareId, i+2, ver3.VersionId, 2 * i + 10
                                );

                            i++;
                        }
                        j++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine("file : " + f);
                Console.WriteLine("line : " + l);
                Console.WriteLine(e.StackTrace);
            }
        }

        public static OnlineStatsRow CreateOnlineStatsRow()
        {
            OnlineStatsRow result = new OnlineStatsRow();

            var query_AllSoftwares =    from s in CONTEXT.Softwares
                                        select s;
            var query_OnlineSoftwares = from l in CONTEXT.Licenses
                                        where
                                           l.LastLoginTime == null ?
                                           false :
                                           l.LastLogoutTime == null || l.LastLogoutTime < l.LastLoginTime
                                        select l.Software;
            var query_AllLicenses =     from l in CONTEXT.Licenses
                                        select l;
            var query_OnlineLicenses =  from l in CONTEXT.Licenses
                                        where
                                           l.LastLoginTime == null ?
                                           false :
                                           l.LastLogoutTime == null || l.LastLogoutTime < l.LastLoginTime
                                        select l;
            var query_AllCustomers =    from c in CONTEXT.Customers
                                        select c;
            var query_OnlineCustomers = from l in CONTEXT.Licenses
                                        where
                                           l.LastLoginTime == null ?
                                           false :
                                           l.LastLogoutTime == null || l.LastLogoutTime < l.LastLoginTime
                                        select l.Customer;
            var query_AllUsers =        from u in CONTEXT.Users
                                        select u;
            var query_OnlineUsers =     from u in CONTEXT.Users
                                        where
                                           u.LastLoginTime == null ?
                                           false :
                                           u.LastLogoutTime == null || u.LastLogoutTime < u.LastLoginTime
                                        select u;
            result.NumAllSoftwares = query_AllSoftwares.Count();
            result.NumOnlineSoftwares = query_OnlineSoftwares.Distinct().Count();
            result.NumAllLicenses = query_AllLicenses.Count();
            result.NumOnlineLicenses = query_OnlineLicenses.Count();
            result.NumAllCustomers = query_AllCustomers.Count();
            result.NumOnlineCustomers = query_OnlineCustomers.Distinct().Count();
            result.NumAllUsers = query_AllUsers.Count();
            result.NumOnlineUsers = query_OnlineUsers.Count();

            return result;
        }

        static SoftwareRow CreateSoftwareRow(Software software)
        {
            SoftwareRow result = new SoftwareRow();
            result.SoftwareId = software.SoftwareId;
            result.Title = software.Title;
            if (software.CurrentVersion != null)
            {
                result.CurrentVersionId = software.CurrentVersion.VersionId;
                result.CurrentVersionTitle = software.CurrentVersion.Title;
                result.CurrentVersionAvailableFrom = software.CurrentVersion.AvailableFrom;
                result.CurrentVersionAvailableUntil = software.CurrentVersion.AvailableUntil;
            }
            if (software.UpdateLog != null) {
                result.UpdateLogId = software.UpdateLog.UpdateLogId;
                result.CreateTime = software.UpdateLog.CreateTime;
                result.CreatedByName = software.UpdateLog.CreatedBy.Name;
                result.UpdateTime = software.UpdateLog.UpdateTime;
                result.UpdatedByName = software.UpdateLog.UpdatedBy.Name;
            }
            return result;
        }

        static LicenseRow CreateLicenseRow(License license)
        {
            LicenseRow result = new LicenseRow();
            result.LicenseId = license.LicenseId;
            result.OriginalId = license.OriginalId;
            result.Key = license.Key;
            result.ValidFrom = license.ValidFrom;
            result.ValidUntil= license.ValidUntil;
            result.OriginalId = license.OriginalId;
            result.LastLoginTime = license.LastLoginTime;
            result.LastLogoutTime = license.LastLogoutTime;
            result.Online = license.Online;

            if (license.Software != null)
            {
                result.SoftwareId = license.SoftwareId;
                result.SoftwareTitle = license.Software.Title;
            }
            if (license.Customer != null)
            {
                result.CustomerId = license.CustomerId;
                result.CustomerName = license.Customer.Name;
                result.CustomerEmail = license.Customer.Email;
                result.SoftwareTitle = license.Software.Title;
                result.SoftwareTitle = license.Software.Title;
                result.SoftwareTitle = license.Software.Title;
            }
            if (license.CurrentVersion != null)
            {
                result.CurrentVersionId = license.CurrentVersion.VersionId;
                result.CurrentVersionTitle = license.CurrentVersion.Title;
                result.CurrentVersionAvailableFrom = license.CurrentVersion.AvailableFrom;
                result.CurrentVersionAvailableUntil = license.CurrentVersion.AvailableUntil;
            }
            if (license.UpdateLog != null)
            {
                result.UpdateLogId = license.UpdateLog.UpdateLogId;
                result.CreateTime = license.UpdateLog.CreateTime;
                result.CreatedByName = license.UpdateLog.CreatedBy.Name;
                result.UpdateTime = license.UpdateLog.UpdateTime;
                result.UpdatedByName = license.UpdateLog.UpdatedBy.Name;
            }
            return result;
        }
        static VersionRow CreateVersionRow(Version version, ServerOp serverOp)
        {
            VersionRow result = new VersionRow();
            result.VersionId = version.VersionId;
            result.Title = version.Title;
            result.AvailableFrom = version.AvailableFrom;
            result.AvailableUntil = version.AvailableUntil;

            result.SoftwareId = version.SoftwareId;
            result.Title = version.Title;
            if (version.Software != null)
            {
                result.SoftwareId = version.SoftwareId;
                result.SoftwareTitle = version.Software.Title;
                result.Current = version.Software.CurrentVersion.VersionId == version.VersionId;
            }
            if (version.UpdateLog != null)
            {
                result.UpdateLogId = version.UpdateLog.UpdateLogId;
                result.CreateTime = version.UpdateLog.CreateTime;
                result.CreatedByName = version.UpdateLog.CreatedBy.Name;
                result.UpdateTime = version.UpdateLog.UpdateTime;
                result.UpdatedByName = version.UpdateLog.UpdatedBy.Name;
            }
            if (serverOp == ServerOp.GET_SOFTWARE_VERSIONS_OL)
            {
                var query_AllLicenses =     from l in CONTEXT.Licenses
                                            where l.CurrentVersion.VersionId == version.VersionId
                                            select l;
                var query_OnlineLicenses = from l in CONTEXT.Licenses
                                           where    l.CurrentVersion.VersionId == version.VersionId &&
                                                    (l.LastLoginTime == null ?
                                                    false :
                                                    l.LastLogoutTime == null || l.LastLogoutTime < l.LastLoginTime)
                                            select l;
                var query_AllCustomers =    from l in CONTEXT.Licenses
                                            where l.CurrentVersion.VersionId == version.VersionId
                                            select l.Customer;
                var query_OnlineCustomers = from l in CONTEXT.Licenses
                                            where   l.CurrentVersion.VersionId == version.VersionId &&
                                                    (l.LastLoginTime == null ?
                                                    false :
                                                    l.LastLogoutTime == null || l.LastLogoutTime < l.LastLoginTime)
                                            select l.Customer;
                result.NumAllLicenses = query_AllLicenses.Count();
                result.NumOnlineLicenses = query_OnlineLicenses.Count();
                result.NumAllCustomers = query_AllCustomers.Distinct().Count();
                result.NumOnlineCustomers = query_OnlineCustomers.Distinct().Count();
            }

            return result;
        }

        static CustomerRow CreateCustomerRow(License customerLicense)
        {
            CustomerRow result = new CustomerRow();
            result.Name = customerLicense.Customer.Name;
            result.Email = customerLicense.Customer.Email;
            result.Phone = customerLicense.Customer.Phone;
            result.CustomerId = customerLicense.CustomerId;

            result.LicenseId = customerLicense.LicenseId;
            result.LicenseOriginalId = customerLicense.OriginalId;
            result.LicenseValidFrom = customerLicense.ValidFrom;
            result.LicenseValidUntil = customerLicense.ValidUntil;
            result.LastLoginTime = customerLicense.LastLoginTime;
            result.LastLogoutTime = customerLicense.LastLogoutTime;
            result.Online = customerLicense.Online;

            if (customerLicense.Customer.UpdateLog != null)
            {
                result.UpdateLogId = customerLicense.Customer.UpdateLog.UpdateLogId;
                result.CreateTime = customerLicense.Customer.UpdateLog.CreateTime;
                result.CreatedByName = customerLicense.Customer.UpdateLog.CreatedBy.Name;
                result.UpdateTime = customerLicense.Customer.UpdateLog.UpdateTime;
                result.UpdatedByName = customerLicense.Customer.UpdateLog.UpdatedBy.Name;
            }
            return result;
        }

        static UserRow CreateUserRow(User user)
        {
            UserRow result = new UserRow();
            result.Name = user.Name;
            result.Email = user.Email;
            result.Phone = user.Phone;
            result.LastLoginTime = user.LastLoginTime;
            result.LastLogoutTime = user.LastLogoutTime;
            result.Online = user.Online;
            result.UserId = user.UserId;
            result.LoginId = user.LoginId;
            result.EncryptedPassword = user.EncryptedPassword;
            result.Admin = user.Admin;
            return result;
        }

        static IOrderedQueryable<T> AddOrderBy<T>(IQueryable<T> query, string param, string orderBy)
        {
            var parameterExpression = Expression.Parameter(typeof(T), param);
            var getPropExpression = Expression.Property(parameterExpression, orderBy);

            var keySelector0 = Expression.Lambda(getPropExpression, parameterExpression);
            IOrderedQueryable<T> query2;
            if (!AddOrderBy2<T, int>(query, keySelector0, out query2))
            {
                if (!AddOrderBy2<T, string>(query, keySelector0, out query2))
                {
                    if (!AddOrderBy2<T, DateTime>(query, keySelector0, out query2))
                    {
                        if (!AddOrderBy2<T, object>(query, keySelector0, out query2))
                        {
                            throw new InvalidOperationException("wrong orderBy:"+orderBy);
                        }
                    }
                }
            }
            return query2;
        }

        static bool AddOrderBy2<T,TKey>(IQueryable<T> query, LambdaExpression keySelector0, out IOrderedQueryable<T> query2)
        {
            if (keySelector0 is Expression<Func<T, TKey>>)
            {
                var keySelector = (Expression<Func<T, TKey>>)keySelector0;
                query2 = query.OrderBy(keySelector);
                return true;
            }
            else
            {
                query2 = null;
                return false;
            }
        }

        public static UserRow Authenticate(UserRow user)
        {
            InitContextOnServer();
            var query = from u in CONTEXT.Users
                        where u.LoginId.Equals(user.LoginId) && u.EncryptedPassword.Equals(user.EncryptedPassword)
                        orderby u.UserId
                        select u;
            User user2 = query.Single();
            if (user.LoginId.Equals(user2.LoginId))
            {
                user2.LastLoginTime = DateTime.Now;
                CONTEXT.SaveChanges();
                return CreateUserRow(user2);
            }
            else
            {
                return null;
            }
        }

        public static QueryResult<SoftwareRow> GetSoftwares(int selectedItemId, int from, int count, string orderBy)
        {
            InitContextOnServer();
            var query = from s in CONTEXT.Softwares
                        select s;
            query = AddOrderBy(query, "s", orderBy);

            int totalCnt = query.Count();
            List<SoftwareRow> result = new List<SoftwareRow>();
            if (from == 0 && selectedItemId != -1)
            {
                Console.WriteLine("selectedItemId:" + selectedItemId);

                IQueryable query01 = DynamicQueryable.Where(query, "SoftwareId==@0", selectedItemId);
                query01 = DynamicQueryable.Select(query01, orderBy, null);
                object ordFieldVal = null;
                foreach (var s in query01)
                {
                    ordFieldVal = s;
                }

                if (ordFieldVal != null)
                {
                    Console.WriteLine("ordFieldVal:" + ordFieldVal);
                    IQueryable query02 = DynamicQueryable.Where(query, orderBy + " < @0", ordFieldVal);

                    from = Math.Max(0, query02.Count() - 3 * count / 5);
                }
                else
                {
                    Console.WriteLine("ordFieldVal==null");
                }
            }

            foreach (var s in query.Skip(from).Take(count))
            {
                result.Add(CreateSoftwareRow(s));
            }

            return new QueryResult<SoftwareRow>(result, totalCnt, from);
        }

        public static QueryResult<LicenseRow> GetSoftwareLicenses(int selectedItemId, int softwareId, ServerOp serverOp, int from, int count, string orderBy)
        {
            InitContextOnServer();
            IQueryable<License> query;
            if (serverOp == ServerOp.GET_SOFTWARE_LICENSES_OL)
            {
                query = from l in CONTEXT.Licenses
                        select l;
            }
            else
            {
                query = from l in CONTEXT.Licenses
                        where l.SoftwareId == softwareId
                        select l;
            }
            query = AddOrderBy(query, "l", orderBy);

            int totalCnt = query.Count();
            List<LicenseRow> result = new List<LicenseRow>();
            if (from == 0 && selectedItemId != -1)
            {
                Console.WriteLine("selectedItemId:" + selectedItemId);

                IQueryable query01 = DynamicQueryable.Where(query, "LicenseId==@0", selectedItemId);
                query01 = DynamicQueryable.Select(query01, orderBy, null);
                object ordFieldVal = null;
                foreach (var s in query01)
                {
                    ordFieldVal = s;
                }

                if (ordFieldVal != null)
                {
                    Console.WriteLine("ordFieldVal:" + ordFieldVal);
                    IQueryable query02 = DynamicQueryable.Where(query, orderBy + " < @0", ordFieldVal);

                    from = Math.Max(0, query02.Count() - 3 * count / 5);
                }
                else
                {
                    Console.WriteLine("ordFieldVal==null");
                }
            }

            foreach (var l in query.Skip(from).Take(count))
            {
                result.Add(CreateLicenseRow(l));
            }
            return new QueryResult<LicenseRow>(result, totalCnt, from);
        }

        public static QueryResult<VersionRow> GetSoftwareVersions(int selectedItemId, int softwareId, ServerOp serverOp, int from, int count, string orderBy)
        {
            InitContextOnServer();
            IQueryable<Version> query;
            if (serverOp == ServerOp.GET_SOFTWARE_VERSIONS_OL)
            {
                query = from v in CONTEXT.Versions
                        orderby v.VersionId
                        select v;
            }
            else
            {
                query = from v in CONTEXT.Versions
                        where v.SoftwareId == softwareId
                        orderby v.VersionId
                        select v;
            }
            query = AddOrderBy(query, "v", orderBy);

            int totalCnt = query.Count();
            List<VersionRow> result = new List<VersionRow>();
            foreach (var v in query.Skip(from).Take(count))
            {
                result.Add(CreateVersionRow(v, serverOp));
            }
            return new QueryResult<VersionRow>(result, totalCnt, from);
        }

        public static QueryResult<CustomerRow> GetSoftwareCustomers(int selectedItemId, int softwareId, int from, int count, string orderBy)
        {
            InitContextOnServer();
            var query = from l in CONTEXT.Licenses
                        where l.SoftwareId == softwareId
                        orderby l.LicenseId
                        select l;
            query = AddOrderBy(query, "l", orderBy);

            int totalCnt = query.Count();
            List<CustomerRow> result = new List<CustomerRow>();
            foreach (var l in query.Skip(from).Take(count))
            {
                result.Add(CreateCustomerRow(l));
            }
            return new QueryResult<CustomerRow>(result, totalCnt, from);
        }

        public static QueryResult<UserRow> GetLicenseServerUsers(int selectedItemId, int from, int count, string orderBy)
        {
            InitContextOnServer();
            var query = from u in CONTEXT.Users
                        orderby u.UserId
                        select u;
            query = AddOrderBy(query, "u", orderBy);

            int totalCnt = query.Count();
            List<UserRow> result = new List<UserRow>();
            foreach (var u in query.Skip(from).Take(count))
            {
                result.Add(CreateUserRow(u));
            }
            return new QueryResult<UserRow>(result, totalCnt, from);
        }
    }

    public class Entity
    {
    }

    public class Software : Entity
    {
        [Key]
        public int SoftwareId { get; set; }

        [StringLength(256)]
        public string Title { get; set; }
        public virtual Version CurrentVersion { get; set; }
        public virtual UpdateLog UpdateLog { get; set; }

        [InverseProperty("Software")]
        public virtual List<License> Licenses { get; set; }
        [InverseProperty("Software")]
        public virtual List<Version> Versions { get; set; }

        public Software()
        {
        }
        public Software(int Id, string Title)
        {
            this.SoftwareId = Id;
            this.Title = Title;
        }
    }

    public class License : Entity
    {
        [Key]
        public int LicenseId { get; set; }

        [StringLength(256)]
        public string OriginalId { get; set; }
        [StringLength(1024)]
        public string Key { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        public DateTime LastLoginTime { get; set; }
        public DateTime LastLogoutTime { get; set; }

        [ForeignKey("Software")]
        public int SoftwareId { get; set; }
        public virtual Software Software { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Version CurrentVersion { get; set; }
        public virtual UpdateLog UpdateLog { get; set; }

        public bool Online
        {
            get
            {
                return LastLoginTime == null ?
                       false :
                       LastLogoutTime == null || LastLogoutTime < LastLoginTime;
            }
        }

        public License()
        {
        }
        public License(int Id, string OriginalId, string Key, DateTime ValidFrom, DateTime ValidUntil)
        {
            this.LicenseId = Id;
            this.OriginalId = OriginalId;
            this.Key = Key;
            this.ValidFrom = ValidFrom;
            this.ValidUntil = ValidUntil;
        }
    }

    public class Version : Entity
    {
        [Key]
        public int VersionId { get; set; }

        [StringLength(256)]
        public string Title { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }

        [ForeignKey("Software")]
        public int SoftwareId { get; set; }
        public virtual Software Software { get; set; }
        public virtual UpdateLog UpdateLog { get; set; }
        public Version()
        {
        }
        public Version(int Id, string Title, DateTime AvailableFrom, DateTime AvailableUntil)
        {
            this.VersionId = Id;
            this.Title = Title;
            this.AvailableFrom = AvailableFrom;
            this.AvailableUntil = AvailableUntil;
        }
    }

    public class Person : Entity
    {
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string Phone { get; set; }

        public Person()
        {
        }
        public Person(string Name, string Email, string Phone)
        {
            this.Name = Name;
            this.Email = Email;
            this.Phone = Phone;
        }
    }

    public class Customer : Person
    {
        [Key]
        public int CustomerId { get; set; }

        public virtual UpdateLog UpdateLog { get; set; }
        [InverseProperty("Customer")]
        public virtual List<License> Licenses { get; set; }
        public Customer()
        {
        }
        public Customer(int Id, string Name, string Email, string Phone) :
            base(Name, Email, Phone)
        {
            this.CustomerId = Id;
        }
    }

    public class User : Person
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(32)]
        public string LoginId { get; set; }
        [StringLength(1024)]
        public string EncryptedPassword { get; set; }
        public bool Admin { get; set; }

        public DateTime LastLoginTime { get; set; }
        public DateTime LastLogoutTime { get; set; }

        public bool Online
        {
            get
            {
                return LastLoginTime == null ?
                       false :
                       LastLogoutTime == null || LastLogoutTime < LastLoginTime;
            }
        }

        public User()
        {
        }
        public User(int Id, string Name, string Email, string Phone, string LoginId, string EncryptedPassword, bool Admin) :
            base(Name, Email, Phone)
        {
            this.UserId = Id;
            this.LoginId = LoginId;
            this.EncryptedPassword = EncryptedPassword;
            this.Admin = Admin;
        }
    }

    public class UpdateLog : Entity
    {
        [Key]
        public int UpdateLogId { get; set; }

        public DateTime CreateTime { get; set; }
        public virtual User CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public virtual User UpdatedBy { get; set; }
        public UpdateLog()
        {
        }
        public UpdateLog(int Id, DateTime CreateTime, User CreatedBy, DateTime UpdateTime, User UpdatedBy)
        {
            this.UpdateLogId = Id;
            this.CreateTime = CreateTime;
            this.CreatedBy = CreatedBy;
            this.UpdateTime = UpdateTime;
            this.UpdatedBy = UpdatedBy;
        }
    }

    public class SubmarineDbContext : DbContext
    {
        public DbSet<Software> Softwares { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UpdateLog> UpdateLogs { get; set; }
    }
}
