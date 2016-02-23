using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Submarine.Base;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Submarine.LicenseServerTerminal
{
    public enum RequestType
    {
        SOFTWARES, SOFTWARE_LICENCES_ON, SOFTWARE_LICENCES_OFF, SOFTWARE_VERSIONS_ON, SOFTWARE_VERSIONS_OFF, SOFTWARE_CUSTOMERS, USERS,
    }

    static class Program
    {
        static LicenseServerTerminalApplicationContext context;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            context = new LicenseServerTerminalApplicationContext();
        }
    }

    public class LicenseServerTerminalApplicationContext //: ApplicationContext
    {
        private Form1 form1;
        private SslTcpClient.Session session;
        private BinaryFormatter bformatter;

        public LicenseServerTerminalApplicationContext()
        {
            LoggingIn = false;
            LoggedIn = false;
            PageSize = 1000;
            UserLoggedIn = new UserRow();
            ConsoleForm = new ConsoleForm();
            form1 = new Form1();
            form1.AppContext = this;
            bformatter = new BinaryFormatter();
            Application.Run(form1);
        }

        internal bool LoggingIn
        {
            get;
            set;
        }

        internal bool LoggedIn {
            get;
            set;
        }

        internal int PageSize
        {
            get;
            set;
        }

        internal UserRow UserLoggedIn {
            get;
            set;
        }

        internal ConsoleForm ConsoleForm {
            get;
            set;
        }

        int softwareId = -1;
        internal int SoftwareId
        {
            get
            {
                return this.softwareId;
            }
            set
            {
                this.softwareId = value;
                Console.WriteLine("set SoftwareId:" + this.softwareId);
            }
        }

        int licenseId = -1;
        internal int LicenseId {
            get
            {
                return this.licenseId;
            }
            set
            {
                this.licenseId = value;
                Console.WriteLine("set LicenseId:" + this.licenseId);
            }
        }

        int versionId = -1;
        internal int VersionId {
            get
            {
                return this.versionId;
            }
            set
            {
                this.versionId = value;
                Console.WriteLine("set VersionId:" + this.versionId);
            }
        }

        int userId = -1;
        internal int UserId {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
                Console.WriteLine("set UserId:" + this.userId);
            }
        }

        internal void LogIn()
        {
            try
            {
                LoggingIn = true;
                form1.UpdateLoginUi();
                Console.WriteLine("LogIn()");

                int port;
                int.TryParse(form1.portTextBox.Text, out port);
                session = SslTcpClient.RunClient(form1.ipUrlTextBox.Text, "VM114070.tradersvps.net", null, port);

                UserLoggedIn.LoginId = form1.uidNameTextBox.Text;
                UserLoggedIn.EncryptedPassword = BaseUtil.EncryptPassword(form1.passwordUiTextBox.Text);

                // Write a message to the server.
                session.SslStream.WriteByte((byte)ServerOp.AUTHENTICATE);
                bformatter.Serialize(session.SslStream, UserLoggedIn);
                session.SslStream.Flush();
                UserLoggedIn = (UserRow)bformatter.Deserialize(session.SslStream);
                if (UserLoggedIn.LoginId == null)
                {
                    LoggedIn = false;
                    UserLoggedIn = new UserRow();
                    Console.WriteLine("Authentication failed,  user:" + UserLoggedIn.LoginId);
                    MessageBox.Show("Authentication failed.");
                    LogOut();
                }
                else
                {
                    LoggedIn = true;
                    Console.WriteLine("Authenticated,  user:" + UserLoggedIn.LoginId);
                }
                //List<Software> softwares = bformatter.Deserialize(session.SslStream);

            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to connect,  ip/host:" + form1.ipUrlTextBox.Text + " port:" + form1.portTextBox.Text);
                MessageBox.Show("Unable to connect.  " + e.Message);
                Console.WriteLine("Exception:");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                LogOut();
            }
            finally
            {
                LoggingIn = false;
                if (LoggedIn)
                {
                    Console.WriteLine("LogIn()->LoadInitPage()");
                    form1.LoadInitPage();
                }
                else
                {
                    form1.UpdateLoginUi();
                }
            }
        }

        internal void LogOut()
        {
            try
            {
                LoggingIn = true;
                form1.UpdateLoginUi();

                Console.WriteLine("LogOut()");
                try
                {
                    session.SslStream.WriteByte((byte)ServerOp.LOG_OUT);
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
                try
                {
                    session.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                LoggedIn = false;
            }
            finally
            {
                LoggingIn = false;
                form1.UpdateLoginUi();
            }
        }

        internal OnlineStatsRow GetOnlineStats()
        {
            try
            {
                Console.WriteLine("AppContext.GetOnlineStats()");

                // Write a message to the server.
                session.SslStream.WriteByte((byte)ServerOp.GET_OL_STATS);
                session.SslStream.Flush();
                bformatter.Serialize(session.SslStream, 0);
                bformatter.Serialize(session.SslStream, 0);
                bformatter.Serialize(session.SslStream, PageSize);
                bformatter.Serialize(session.SslStream, "");
                OnlineStatsRow onlineStats = (OnlineStatsRow)bformatter.Deserialize(session.SslStream);
                return onlineStats;
            }
            catch (Exception e)
            {
                HandleServiceEx(e);
                return null;
            }
        }

        internal QueryResult<SoftwareRow> GetSoftwares(int from = 0, int count = 0, string orderBy = "SoftwareId")
        {
            try
            {
                if (count == 0)
                {
                    count = PageSize;
                }
                Console.WriteLine("AppContext.GetSoftwares()");

                // Write a message to the server.
                session.SslStream.WriteByte((byte)ServerOp.GET_SOFTWARES);
                session.SslStream.Flush();
                bformatter.Serialize(session.SslStream, SoftwareId);
                bformatter.Serialize(session.SslStream, from);
                bformatter.Serialize(session.SslStream, PageSize);
                bformatter.Serialize(session.SslStream, orderBy);
                QueryResult<SoftwareRow> softwares = (QueryResult<SoftwareRow>)bformatter.Deserialize(session.SslStream);
                Console.WriteLine("softwares.TotalCnt : " + softwares.TotalCnt);
                return softwares;
            }
            catch (Exception e)
            {
                HandleServiceEx(e);
                return null;
            }
        }

        internal QueryResult<LicenseRow> GetSoftwareLicences(bool online, int from = 0, int count = 0, string orderBy = "LicenseId")
        {
            try
            {
                if (count == 0)
                {
                    count = PageSize;
                }
                Console.WriteLine("AppContext.GetSoftwareLicences(from:" + from + " orderBy:" + orderBy + " online:" + online + ")");

                // Write a message to the server.
                if (online)
                    session.SslStream.WriteByte((byte)ServerOp.GET_SOFTWARE_LICENSES_OL);
                else
                    session.SslStream.WriteByte((byte)ServerOp.GET_SOFTWARE_LICENSES);
                session.SslStream.Flush();
                bformatter.Serialize(session.SslStream, LicenseId);
                bformatter.Serialize(session.SslStream, from);
                bformatter.Serialize(session.SslStream, PageSize);
                bformatter.Serialize(session.SslStream, orderBy);
                bformatter.Serialize(session.SslStream, SoftwareId);
                QueryResult<LicenseRow> licenses = (QueryResult<LicenseRow>)bformatter.Deserialize(session.SslStream);
                Console.WriteLine("licenses.TotalCnt : " + licenses.TotalCnt);
                return licenses;
            }
            catch (Exception e)
            {
                HandleServiceEx(e);
                return null;
            }
        }

        internal QueryResult<VersionRow> GetSoftwareVersions(bool online, int from = 0, int count = 0, string orderBy = "VersionId")
        {
            try
            {
                if (count == 0)
                {
                    count = PageSize;
                }
                Console.WriteLine("AppContext.GetSoftwareVersions(from:" + from + " orderBy:" + orderBy + " online:" + online + ")");

                // Write a message to the server.
                if (online)
                    session.SslStream.WriteByte((byte)ServerOp.GET_SOFTWARE_VERSIONS_OL);
                else
                    session.SslStream.WriteByte((byte)ServerOp.GET_SOFTWARE_VERSIONS);
                session.SslStream.Flush();
                bformatter.Serialize(session.SslStream, VersionId);
                bformatter.Serialize(session.SslStream, from);
                bformatter.Serialize(session.SslStream, PageSize);
                bformatter.Serialize(session.SslStream, orderBy);
                bformatter.Serialize(session.SslStream, SoftwareId);
                QueryResult<VersionRow> licenses = (QueryResult<VersionRow>)bformatter.Deserialize(session.SslStream);
                Console.WriteLine("licenses.TotalCnt : " + licenses.TotalCnt);
                return licenses;
            }
            catch (Exception e)
            {
                HandleServiceEx(e);
                return null;
            }
        }

        internal QueryResult<CustomerRow> GetSoftwareCustomers(int from = 0, int count = 0, string orderBy = "LicenseId")
        {
            try
            {
                if (count == 0)
                {
                    count = PageSize;
                }
                Console.WriteLine("AppContext.GetSoftwareCustomers(from:" + from + " orderBy:" + orderBy + ")");

                // Write a message to the server.
                session.SslStream.WriteByte((byte)ServerOp.GET_SOFTWARE_CUSTOMERS);
                session.SslStream.Flush();
                bformatter.Serialize(session.SslStream, LicenseId);
                bformatter.Serialize(session.SslStream, from);
                bformatter.Serialize(session.SslStream, PageSize);
                bformatter.Serialize(session.SslStream, orderBy);
                bformatter.Serialize(session.SslStream, SoftwareId);
                QueryResult<CustomerRow> customers = (QueryResult<CustomerRow>)bformatter.Deserialize(session.SslStream);
                Console.WriteLine("customers.TotalCnt : " + customers.TotalCnt);
                return customers;
            }
            catch (Exception e)
            {
                HandleServiceEx(e);
                return null;
            }
        }

        internal QueryResult<UserRow> GetLicenseServerUsers(int from=0, int count=0, string orderBy="UserId")
        {
            try
            {
                if (count == 0)
                {
                    count = PageSize;
                }
                Console.WriteLine("AppContext.GetLicenseServerUsers(from:" + from + " orderBy:" + orderBy + ")");

                // Write a message to the server.
                session.SslStream.WriteByte((byte)ServerOp.GET_LICENSE_SERVER_USERS);
                session.SslStream.Flush();
                bformatter.Serialize(session.SslStream, UserId);
                bformatter.Serialize(session.SslStream, from);
                bformatter.Serialize(session.SslStream, PageSize);
                bformatter.Serialize(session.SslStream, orderBy);
                QueryResult<UserRow> users = (QueryResult<UserRow>)bformatter.Deserialize(session.SslStream);
                Console.WriteLine("users.TotalCnt : " + users.TotalCnt);
                return users;
            }
            catch (Exception e)
            {
                HandleServiceEx(e);
                return null;
            }
        }

        internal IQueryResult DoRequest(RequestType Type, int from = 0, int count = 0, string orderBy = null)
        {
            switch (Type)
            {
                case RequestType.SOFTWARES:
                    if (orderBy==null)
                        return GetSoftwares(from, count);
                    else
                        return GetSoftwares(from, count, orderBy);
                case RequestType.SOFTWARE_LICENCES_OFF:
                    if (orderBy == null)
                        return GetSoftwareLicences(false, from, count);
                    else
                        return GetSoftwareLicences(false, from, count, orderBy);
                case RequestType.SOFTWARE_LICENCES_ON:
                    if (orderBy == null)
                        return GetSoftwareLicences(true, from, count);
                    else
                        return GetSoftwareLicences(true, from, count, orderBy);
                case RequestType.SOFTWARE_VERSIONS_OFF:
                    if (orderBy == null)
                        return GetSoftwareVersions(false, from, count);
                    else
                        return GetSoftwareVersions(false, from, count, orderBy);
                case RequestType.SOFTWARE_VERSIONS_ON:
                    if (orderBy == null)
                        return GetSoftwareVersions(true, from, count);
                    else
                        return GetSoftwareVersions(true, from, count, orderBy);
                case RequestType.SOFTWARE_CUSTOMERS:
                    if (orderBy == null)
                        return GetSoftwareCustomers(from, count);
                    else
                        return GetSoftwareCustomers(from, count, orderBy);
                case RequestType.USERS:
                    if (orderBy == null)
                        return GetLicenseServerUsers(from, count);
                    else
                        return GetLicenseServerUsers(from, count, orderBy);
                default:
                    return null;
            }
        }

        internal object CreateRowObj(RequestType Type)
        {
            switch (Type)
            {
                case RequestType.SOFTWARES:
                    if (SoftwareId == -1)
                        return null;
                    else
                        return new SoftwareRow(SoftwareId);
                case RequestType.SOFTWARE_LICENCES_OFF:
                case RequestType.SOFTWARE_LICENCES_ON:
                    if (LicenseId == -1)
                        return null;
                    else
                        return new LicenseRow(LicenseId);
                case RequestType.SOFTWARE_VERSIONS_OFF:
                case RequestType.SOFTWARE_VERSIONS_ON:
                    if (VersionId == -1)
                        return null;
                    else
                        return new VersionRow(VersionId);
                case RequestType.SOFTWARE_CUSTOMERS:
                    if (LicenseId == -1)
                        return null;
                    else
                        return new CustomerRow(LicenseId);
                case RequestType.USERS:
                    if (UserId == -1)
                        return null;
                    else
                        return new UserRow(UserId);
                default:
                    return null;
            }
        }

        void HandleServiceEx(Exception e)
        {
            Console.WriteLine("Exception:");
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            MessageBox.Show("Error downloading data.  " + e.Message);
            LogOut();
        }

        internal void Exit()
        {
            Console.WriteLine("Exiting...");
            Thread.Sleep(10000);
            Application.Exit();
        }
    }
}
