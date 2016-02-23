using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Submarine.Base;
using System.Collections;
using System.Collections.Generic;

namespace Submarine.LicenseServerTerminal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TabPage1 TabPage1 { get; set; }
        TabPage2 TabPage2 { get; set; }
        TabPage3 TabPage3 { get; set; }

        internal LicenseServerTerminalApplicationContext AppContext {get; set; }

        internal void UpdateLoginUi()
        {
            if (AppContext.LoggingIn)
            {
                ipUrlTextBox.ReadOnly = true;
                portTextBox.ReadOnly = true;
                uidNameTextBox.ReadOnly = true;
                passwordUiTextBox.ReadOnly = true;
                disConnectButton.Enabled = false;
                tabControl1.Enabled = false;
            }
            else if (AppContext.LoggedIn)
            {
                ipUrlTextBox.ReadOnly = true;
                portTextBox.ReadOnly = true;
                uidNameTextBox.ReadOnly = true;
                uidNameLabel.Text = "Name";
                uidNameTextBox.Text = AppContext.UserLoggedIn.Name;
                passwordUidLabel.Text = "Login Id";
                passwordUiTextBox.ReadOnly = true;
                passwordUiTextBox.PasswordChar = (char)0;
                passwordUiTextBox.Text = AppContext.UserLoggedIn.LoginId;
                disConnectButton.Enabled = true;
                disConnectButton.Text = "Disconnect";
                tabControl1.Enabled = true;
            }
            else
            {
                ipUrlTextBox.ReadOnly = false;
                portTextBox.ReadOnly = false;
                uidNameTextBox.ReadOnly = false;
                uidNameLabel.Text = "Login Id";
                uidNameTextBox.Text = "wmorrison";
                passwordUidLabel.Text = "Password";
                passwordUiTextBox.ReadOnly = false;
                passwordUiTextBox.PasswordChar = '*';
                passwordUiTextBox.Text = "start";
                disConnectButton.Enabled = true;
                disConnectButton.Text = "Connect";
                tabControl1.Enabled = true;
                ClearAllData();
            }
        }

        void ClearTable(DataGridView table)
        {
            if (table.DataSource is IList)
            {
                IList l = (IList)table.DataSource;
                table.DataSource = null;
                l.Clear();
                table.DataSource = l;
            }
        }

        void SelectTableRow(DataGridView table, object item)
        {
            if (table.DataSource != null)
            {
                int index = ((IList)table.DataSource).IndexOf(item);
                if (index != -1)
                {
                    table.CurrentCell = table.Rows[index].Cells[0];
                }
            }
        }

        void ClearAllData()
        {
            ClearTable(softwares1GridView);
            ClearTable(licenses1GridView);
            ClearTable(versions1GridView);
            ClearTable(softwares2GridView);
            ClearTable(softwareLicenses3GridView);
            ClearTable(softwareVersions4GridView);
            ClearTable(softwareCustomers5GridView);
            ClearTable(licenseServerUsers6GridView);
            ClearTable(softwareVersions7GridView);
            ClearTable(licenses7GridView);
            ClearTable(licenseServerUsers7GridView);
        }

        internal void LoadInitPage()
        {
            Console.WriteLine("LoadInitPage()");
            TabPage3 = TabPage3.SOFTWARE_VERSIONS;
            Select(TabPage2.LICENSES);
            UpdateLoginUi();
        }

        void Select(TabPage1 tabPage1)
        {
            Select1(tabPage1.Id);
        }

        void Select1(int tabPage1Id)
        {
            Console.WriteLine("Select1");
            bool changed = TabPage1 == null || TabPage1.Id != tabPage1Id;
            tabControl1.SelectedIndex = tabPage1Id; 

            overviewMi1.Checked = false;
            softwaresMi1.Checked = false;
            softwareLicensesMi1.Checked = false;
            softwareVersionsMi1.Checked = false;
            softwareCustomersMi1.Checked = false;
            licenseServerUsersMi1.Checked = false;
            showOnlineConnectionsMi1.Checked = false;

            switch (tabPage1Id)
            {
                case 0: TabPage1 = TabPage1.OVERVIEW;
                        overviewMi1.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            setupGridView<SoftwareRow>(RequestType.SOFTWARES, softwares1GridView, softwares1Nav);
                        }
                        Select2(TabPage2.Id, false);
                        break;
                case 1: TabPage1 = TabPage1.SOFTWARES;
                        softwaresMi1.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            setupGridView<SoftwareRow>(RequestType.SOFTWARES, softwares2GridView, softwares2Nav);
                        }
                        break;
                case 2: TabPage1 = TabPage1.SOFTWARE_LICENSES;
                        softwareLicensesMi1.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            softwaresCombo3.DataSource = AppContext.GetSoftwares().List;
                            softwaresCombo3.SelectedItem = AppContext.CreateRowObj(RequestType.SOFTWARES);
                            setupGridView<LicenseRow>(RequestType.SOFTWARE_LICENCES_OFF, softwareLicenses3GridView, softwareLicenses3Nav);
                        }
                        break;
                case 3: TabPage1 = TabPage1.SOFTWARE_VERSIONS;
                        softwareVersionsMi1.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            softwaresCombo4.DataSource = AppContext.GetSoftwares().List;
                            softwaresCombo4.SelectedItem = AppContext.CreateRowObj(RequestType.SOFTWARES);
                            setupGridView<VersionRow>(RequestType.SOFTWARE_VERSIONS_OFF, softwareVersions4GridView, softwareVersions4Nav);
                        }
                        break;
                case 4: TabPage1 = TabPage1.SOFTWARE_CUSTOMERS;
                        softwareCustomersMi1.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            softwaresCombo5.DataSource = AppContext.GetSoftwares().List;
                            softwaresCombo5.SelectedItem = AppContext.CreateRowObj(RequestType.SOFTWARES);
                            setupGridView<LicenseRow>(RequestType.SOFTWARE_CUSTOMERS, softwareCustomers5GridView, softwareCustomers5Nav);
                        }
                        break;
                case 5: TabPage1 = TabPage1.LICENSE_SERVER_USERS;
                        licenseServerUsersMi1.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            setupGridView<UserRow>(RequestType.USERS, licenseServerUsers6GridView, licenseServerUsers6Nav);
                        }
                        break;
                case 6: TabPage1 = TabPage1.SHOW_ONLINE_COLLECTIONS;
                        showOnlineConnectionsMi1.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            OnlineStatsRow onlineStats = AppContext.GetOnlineStats();
                            numSoftwaresAllBox.Text = "" + onlineStats.NumAllSoftwares;
                            numSoftwaresOlBox.Text = "" + onlineStats.NumOnlineSoftwares;
                            numLicenseServerUsersAllBox.Text = "" + onlineStats.NumAllUsers;
                            numLicenseServerUsersOlBox.Text = "" + onlineStats.NumOnlineUsers;
                            numLicensesAllBox.Text = "" + onlineStats.NumAllLicenses;
                            numLicensesOlBox.Text = "" + onlineStats.NumOnlineLicenses;
                            numCustomersAllBox.Text = "" + onlineStats.NumAllCustomers;
                            numCustomersOlBox.Text = "" + onlineStats.NumOnlineCustomers;
                        }
                        Select3(TabPage3.Id, false);
                        break;
            }
        }


        void Select(TabPage2 tabPage2)
        {
            Select2(tabPage2.Id);
        }
       
        void Select2(int tabPage2Id, bool selectTopLevel=true)
        {
            Console.WriteLine("Select2");
            bool changed = TabPage2 == null || TabPage2.Id != tabPage2Id || !selectTopLevel;
            if (selectTopLevel)
            {
                switch (tabPage2Id)
                {
                    case 0: TabPage2 = TabPage2.LICENSES;
                        break;
                    case 1: TabPage2 = TabPage2.VERSIONS;
                        break;
                }
                Select1(0);
            }

            tabControl2.SelectedIndex = tabPage2Id;

            licensesMi2.Checked = false;
            versionsMi2.Checked = false;
            switch (tabPage2Id)
            {
                case 0: TabPage2 = TabPage2.LICENSES;
                        licensesMi2.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            setupGridView<LicenseRow>(RequestType.SOFTWARE_LICENCES_OFF, licenses1GridView, licenses1Nav);
                        }
                        break;
                case 1: TabPage2 = TabPage2.VERSIONS;
                        versionsMi2.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            setupGridView<VersionRow>(RequestType.SOFTWARE_VERSIONS_OFF, versions1GridView, versions1Nav);
                        }
                        break;
            }
        }


        void Select(TabPage3 tabPage3)
        {
            Select3(tabPage3.Id);
        }

        void Select3(int tabPage3Id, bool selectTopLevel = true)
        {
            Console.WriteLine("Select3");
            bool changed = TabPage3 == null || TabPage3.Id != tabPage3Id;
            if (selectTopLevel)
            {
                switch (tabPage3Id)
                {
                    case 0: TabPage3 = TabPage3.SOFTWARE_VERSIONS;
                        break;
                    case 1: TabPage3 = TabPage3.LICENSES;
                        break;
                    case 2: TabPage3 = TabPage3.LICENSE_SERVER_USERS;
                        break;
                }
                Select1(6);
            }

            tabControl3.SelectedIndex = tabPage3Id;

            softwareVersionsMi3.Checked = false;
            licensesMi3.Checked = false;
            licenseServerUsersMi3.Checked = false;
            switch (tabPage3Id)
            {
                case 0: TabPage3 = TabPage3.SOFTWARE_VERSIONS;
                        softwareVersionsMi3.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            setupGridView<VersionRow>(RequestType.SOFTWARE_VERSIONS_ON, softwareVersions7GridView, versions7Nav);
                        }
                        break;
                case 1: TabPage3 = TabPage3.LICENSES;
                        licensesMi3.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            setupGridView<LicenseRow>(RequestType.SOFTWARE_LICENCES_ON, licenses7GridView, licenses7Nav);
                        }
                        break;
                case 2: TabPage3 = TabPage3.LICENSE_SERVER_USERS;
                        licenseServerUsersMi3.Checked = true;
                        if (changed && AppContext.LoggedIn)
                        {
                            setupGridView<UserRow>(RequestType.USERS, licenseServerUsers7GridView, licenseServerUsers7Nav);
                        }
                        break;
            }
        }

        private void setupGridView<T>(RequestType Type, DataGridView gridView, BindingNavigator bindingNavigator, int from = 0, int count = 0, string orderBy = null)
        {
            Console.WriteLine("setupGridView");
            T selectedRow = (T)AppContext.CreateRowObj(Type);
            QueryResult<T> result = (QueryResult<T>)AppContext.DoRequest(Type, from, count, orderBy);

            TableBindingList<T> bl = new TableBindingList<T>(result);
            gridView.DataSource = bl;
            SelectTableRow(gridView, selectedRow);
            setupGridViewPager<T>(Type, gridView, bindingNavigator);
        }

        private void setupGridViewPager<T>(RequestType Type, DataGridView gridView, BindingNavigator bindingNavigator)
        {
            Console.WriteLine("setupGridViewPager");
            if (gridView.DataSource is BindingSource || gridView.DataSource==null)
            {
                return;
            }
            TableBindingList<T> bl = (TableBindingList<T>)gridView.DataSource;

            int dc = (gridView.DisplayRectangle.Height - gridView.ColumnHeadersHeight) / gridView.RowTemplate.Height;

            int pages = (int)Math.Ceiling(bl.QueryResult.TotalCnt / (double)dc);
            List<int> pageList = new List<int>();
            for (int i = 1; i <= pages; i++)
            {
                pageList.Add(i);
            }

            if (bindingNavigator.BindingSource == null)
            {
                bindingNavigator.BindingSource = new BindingSource();
                bindingNavigator.BindingSource.CurrentChanged += delegate(object sender, EventArgs e)   
                      {
                          gridViewPageSelected<T>(Type, gridView, bindingNavigator);
                      };  
            }
            bindingNavigator.BindingSource.DataSource = pageList;
        }

        private void selectGridViewPage<T>(DataGridView gridView, BindingNavigator bindingNavigator, bool force=false)
        {
            Console.WriteLine("selectGridViewPage");
            if (bindingNavigator.BindingSource != null)
            {
                TableBindingList<T> bl = (TableBindingList<T>)gridView.DataSource;

                int dc = (gridView.DisplayRectangle.Height - gridView.ColumnHeadersHeight) / gridView.RowTemplate.Height;

                int pgind_old = (int)bindingNavigator.BindingSource.Current;
                int pgind = (int)Math.Ceiling((bl.QueryResult.From + gridView.FirstDisplayedScrollingRowIndex + 1) / (double)dc);

                List<int> pageList = (List<int>)bindingNavigator.BindingSource.DataSource;
                if (pageList.Count > 0)
                {
                    bindingNavigator.BindingSource.Position = pgind - pageList[0];

#if (DEBUG)
                    Console.WriteLine("selectGridViewPage    pgind_old:" + pgind_old + " pgind:" + pgind + "    rowInd:" + gridView.CurrentRow.Index);
#endif
                }
            }
        }

        private void gridViewPageSelected<T>(RequestType Type, DataGridView gridView, BindingNavigator bindingNavigator)
        {
            Console.WriteLine("gridViewPageSelected");
            TableBindingList<T> bl = (TableBindingList<T>)gridView.DataSource;

            int dc = (gridView.DisplayRectangle.Height - gridView.ColumnHeadersHeight) / gridView.RowTemplate.Height;

            int pgind = (int)bindingNavigator.BindingSource.Current;
            int pgind_old = (int)Math.Ceiling((bl.QueryResult.From + gridView.FirstDisplayedScrollingRowIndex + 1) / (double)dc);

            int rowIndOld = gridView.FirstDisplayedScrollingRowIndex;
            int rowInd = (pgind - 1) * dc - bl.QueryResult.From;
            rowInd += rowIndOld % dc;

            if (rowInd < AppContext.PageSize / 5)
            {
                int from = Math.Max(0, (pgind - 1) * dc - 2 * AppContext.PageSize / 5);
                setupGridView<T>(Type, gridView, bindingNavigator, from);
                Console.WriteLine("gridViewPageSelected up from:" + from);
            }
            else if (rowInd > 4 * AppContext.PageSize / 5)
            {
                int from = Math.Min(bl.QueryResult.TotalCnt - AppContext.PageSize, (pgind - 1) * dc - 3 * AppContext.PageSize / 5);
                setupGridView<T>(Type, gridView, bindingNavigator, from);
                Console.WriteLine("gridViewPageSelected down from:" + from);
            }
            rowInd = (pgind - 1) * dc - bl.QueryResult.From;
            rowInd += rowIndOld % dc;

            gridView.FirstDisplayedScrollingRowIndex = rowInd;
#if (DEBUG)
            Console.WriteLine("gridViewPageSelected   pgind_old:" + pgind_old + " pgind:" + pgind + "    rowIndOld:" + gridView.CurrentRow.Index + " rowInd:" + rowInd);
#endif
        }

        private void gridViewScroll<T>(DataGridView gridView, BindingNavigator bindingNav, out int from, out T scrolledItem)
        {
            Console.WriteLine("gridViewScroll");
            from = -1;
            scrolledItem = default(T); 
            TableBindingList<T> bl = (TableBindingList<T>)gridView.DataSource;

            if (bl != null)
            {
                if (bl.QueryResult.From > 0 && gridView.FirstDisplayedScrollingRowIndex < AppContext.PageSize / 5)
                {
                    from = Math.Max(0, bl.QueryResult.From - AppContext.PageSize / 5);
                    scrolledItem = (T)gridView.Rows[gridView.FirstDisplayedScrollingRowIndex].DataBoundItem;
                    Console.WriteLine("gridViewScroll up from:" + from + " " + gridView.FirstDisplayedScrollingRowIndex);
                }
                else if (bl.QueryResult.From < bl.QueryResult.TotalCnt - 1 && gridView.FirstDisplayedScrollingRowIndex > 4 * AppContext.PageSize / 5)
                {
                    from = Math.Min(bl.QueryResult.TotalCnt - AppContext.PageSize, bl.QueryResult.From + AppContext.PageSize / 5);
                    scrolledItem = (T)gridView.Rows[gridView.FirstDisplayedScrollingRowIndex].DataBoundItem;
                    Console.WriteLine("gridViewScroll down from:" + from + " " + gridView.FirstDisplayedScrollingRowIndex);
                }
            }
        }

        private delegate void ScrollDelegate(RequestType Type, DataGridView gridView, BindingNavigator bindingNav);

        private void doScroll<T>(RequestType Type, DataGridView gridView, BindingNavigator bindingNav)
        {
            // !!! BeginInvoke !!!
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("doScroll");
                    int from;
                    T scrolledItem;
                    gridViewScroll<T>(gridView, bindingNav, out from, out scrolledItem);
                    if (from >= 0)
                    {
                        setupGridView<T>(Type, gridView, bindingNav, from);
                        TableBindingList<T> bl = (TableBindingList<T>)gridView.DataSource;
                        gridView.FirstDisplayedScrollingRowIndex = bl.QueryResult.List.IndexOf(scrolledItem);
                        Console.WriteLine("doScroll " + Type + " FirstDisplayedScrollingRowIndex:" + gridView.FirstDisplayedScrollingRowIndex);
                    }
                    selectGridViewPage<T>(gridView, bindingNav, true);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void applySort<T>(RequestType Type, DataGridView gridView, BindingNavigator bindingNav)
        {
            Console.WriteLine("applySort");
            TableBindingList<T> bl = (TableBindingList<T>)gridView.DataSource;
            setupGridView<T>(Type, gridView, bindingNav, 0, 0, bl.SortProperty.Name);
        }

        bool _gettingItemsFromServer_ = false;

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select1(e.TabPageIndex);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select2(e.TabPageIndex);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void tabControl3_Selected(object sender, TabControlEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select3(e.TabPageIndex);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void overviewMi1_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select1(0);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwaresMi1_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select1(1);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareLicensesMi1_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select1(2);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersionsMi1_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select1(3);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareCustomersMi1_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select1(4);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsersMi1_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select1(5);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void showOnlineConnectionsMi1_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select1(6);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licensesMi2_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select2(0);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void versionsMi2_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select2(1);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersionsMi3_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select3(0);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licensesMi3_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select3(1);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsersMi3_Click(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Select3(2);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("Form1_Load");
                    LoadInitPage();
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void disConnectButton_Click(object sender, EventArgs e)
        {
            if (AppContext.LoggedIn)
            {
                AppContext.LogOut();
            }
            else
            {
                AppContext.LogIn();
            }
        }

        private void showConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppContext.ConsoleForm.Show();
        }

        private void softwares1GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwares1GridView_SelectionChanged");
                    if (softwares1GridView.SelectedRows.Count == 0)
                    {
                        AppContext.SoftwareId = -1;
                    }
                    else
                    {
                        AppContext.SoftwareId = ((SoftwareRow)softwares1GridView.SelectedRows[0].DataBoundItem).SoftwareId;
                    }
                    Select2(TabPage2.Id, false);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenses1GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenses1GridView_SelectionChanged");
                    if (licenses1GridView.SelectedRows.Count == 0)
                    {
                        AppContext.LicenseId = -1;
                    }
                    else
                    {
                        AppContext.LicenseId = ((LicenseRow)licenses1GridView.SelectedRows[0].DataBoundItem).LicenseId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void versions1GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("versions1GridView_SelectionChanged");
                    if (versions1GridView.SelectedRows.Count == 0)
                    {
                        AppContext.VersionId = -1;
                    }
                    else
                    {
                        AppContext.VersionId = ((VersionRow)versions1GridView.SelectedRows[0].DataBoundItem).VersionId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwares2GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwares2GridView_SelectionChanged");
                    if (softwares2GridView.SelectedRows.Count == 0)
                    {
                        AppContext.SoftwareId = -1;
                    }
                    else
                    {
                        AppContext.SoftwareId = ((SoftwareRow)softwares2GridView.SelectedRows[0].DataBoundItem).SoftwareId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareLicenses3GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareLicenses3GridView_SelectionChanged");
                    if (softwareLicenses3GridView.SelectedRows.Count == 0)
                    {
                        AppContext.LicenseId = -1;
                    }
                    else
                    {
                        AppContext.LicenseId = ((LicenseRow)softwareLicenses3GridView.SelectedRows[0].DataBoundItem).LicenseId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersions4GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareVersions4GridView_SelectionChanged");
                    if (softwareVersions4GridView.SelectedRows.Count == 0)
                    {
                        AppContext.VersionId = -1;
                    }
                    else
                    {
                        AppContext.VersionId = ((VersionRow)softwareVersions4GridView.SelectedRows[0].DataBoundItem).VersionId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareCustomers5GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareCustomers5GridView_SelectionChanged");
                    if (softwareCustomers5GridView.SelectedRows.Count == 0)
                    {
                        AppContext.LicenseId = -1;
                    }
                    else
                    {
                        AppContext.LicenseId = ((CustomerRow)softwareCustomers5GridView.SelectedRows[0].DataBoundItem).LicenseId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsers6GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenseServerUsers6GridView_SelectionChanged");
                    if (licenseServerUsers6GridView.SelectedRows.Count == 0)
                    {
                        AppContext.UserId = -1;
                    }
                    else
                    {
                        AppContext.UserId = ((UserRow)licenseServerUsers6GridView.SelectedRows[0].DataBoundItem).UserId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersions7GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareVersions7GridView_SelectionChanged");
                    if (softwareVersions7GridView.SelectedRows.Count == 0)
                    {
                        AppContext.VersionId = -1;
                    }
                    else
                    {
                        AppContext.VersionId = ((VersionRow)softwareVersions7GridView.SelectedRows[0].DataBoundItem).VersionId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenses7GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenses7GridView_SelectionChanged");
                    if (licenses7GridView.SelectedRows.Count == 0)
                    {
                        AppContext.LicenseId = -1;
                    }
                    else
                    {
                        AppContext.LicenseId = ((LicenseRow)licenses7GridView.SelectedRows[0].DataBoundItem).LicenseId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsers7GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenseServerUsers7GridView_SelectionChanged");
                    if (licenseServerUsers7GridView.SelectedRows.Count == 0)
                    {
                        AppContext.UserId = -1;
                    }
                    else
                    {
                        AppContext.UserId = ((UserRow)licenseServerUsers7GridView.SelectedRows[0].DataBoundItem).UserId;
                    }
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwaresCombo3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwaresCombo3_SelectedIndexChanged");
                    if (softwaresCombo3.SelectedItem == null)
                    {
                        AppContext.SoftwareId = -1;
                    }
                    else
                    {
                        AppContext.SoftwareId = ((SoftwareRow)softwaresCombo3.SelectedItem).SoftwareId;
                    }
                    setupGridView<LicenseRow>(RequestType.SOFTWARE_LICENCES_OFF, softwareLicenses3GridView, softwareLicenses3Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwaresCombo4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwaresCombo4_SelectedIndexChanged");
                    if (softwaresCombo4.SelectedItem == null)
                    {
                        AppContext.SoftwareId = -1;
                    }
                    else
                    {
                        AppContext.SoftwareId = ((SoftwareRow)softwaresCombo4.SelectedItem).SoftwareId;
                    }
                    setupGridView<VersionRow>(RequestType.SOFTWARE_VERSIONS_OFF, softwareVersions4GridView, softwareVersions4Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwaresCombo5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwaresCombo5_SelectedIndexChanged");
                    if (softwaresCombo5.SelectedItem == null)
                    {
                        AppContext.SoftwareId = -1;
                    }
                    else
                    {
                        AppContext.SoftwareId = ((SoftwareRow)softwaresCombo5.SelectedItem).SoftwareId;
                    }
                    setupGridView<CustomerRow>(RequestType.SOFTWARE_CUSTOMERS, softwareCustomers5GridView, softwareCustomers5Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }


        private void softwares1GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwares1GridView_SizeChanged");
                    setupGridViewPager<SoftwareRow>(RequestType.SOFTWARES, softwares1GridView, softwares1Nav);
                    selectGridViewPage<SoftwareRow>(softwares1GridView, softwares1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenses1GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenses1GridView_SizeChanged");
                    setupGridViewPager<LicenseRow>(RequestType.SOFTWARE_LICENCES_OFF, licenses1GridView, licenses1Nav);
                    selectGridViewPage<LicenseRow>(licenses1GridView, licenses1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void versions1GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("versions1GridView_SizeChanged");
                    setupGridViewPager<VersionRow>(RequestType.SOFTWARE_VERSIONS_OFF, versions1GridView, versions1Nav);
                    selectGridViewPage<VersionRow>(versions1GridView, versions1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwares2GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwares2GridView_SizeChanged");
                    setupGridViewPager<SoftwareRow>(RequestType.SOFTWARES, softwares2GridView, softwares2Nav);
                    selectGridViewPage<SoftwareRow>(softwares2GridView, softwares2Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareLicenses3GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareLicenses3GridView_SizeChanged");
                    setupGridViewPager<LicenseRow>(RequestType.SOFTWARE_LICENCES_OFF, softwareLicenses3GridView, softwareLicenses3Nav);
                    selectGridViewPage<LicenseRow>(softwareLicenses3GridView, softwareLicenses3Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersions4GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareVersions4GridView_SizeChanged");
                    setupGridViewPager<VersionRow>(RequestType.SOFTWARE_VERSIONS_OFF, softwareVersions4GridView, softwareVersions4Nav);
                    selectGridViewPage<VersionRow>(softwareVersions4GridView, softwareVersions4Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareCustomers5GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareCustomers5GridView_SizeChanged");
                    setupGridViewPager<CustomerRow>(RequestType.SOFTWARE_CUSTOMERS, softwareCustomers5GridView, softwareCustomers5Nav);
                    selectGridViewPage<CustomerRow>(softwareCustomers5GridView, softwareCustomers5Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsers6GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenseServerUsers6GridView_SizeChanged");
                    setupGridViewPager<UserRow>(RequestType.USERS, licenseServerUsers6GridView, licenseServerUsers6Nav);
                    selectGridViewPage<UserRow>(licenseServerUsers6GridView, licenseServerUsers6Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersions7GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareVersions7GridView_SizeChanged");
                    setupGridViewPager<VersionRow>(RequestType.SOFTWARE_VERSIONS_ON, softwareVersions7GridView, versions7Nav);
                    selectGridViewPage<VersionRow>(softwareVersions7GridView, versions7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenses7GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenses7GridView_SizeChanged");
                    setupGridViewPager<LicenseRow>(RequestType.SOFTWARE_LICENCES_ON, licenses7GridView, licenses7Nav);
                    selectGridViewPage<LicenseRow>(licenses7GridView, licenses7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsers7GridView_SizeChanged(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenseServerUsers7GridView_SizeChanged");
                    setupGridViewPager<UserRow>(RequestType.USERS, licenseServerUsers7GridView, licenseServerUsers7Nav);
                    selectGridViewPage<UserRow>(licenseServerUsers7GridView, licenseServerUsers7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwares1GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwares1GridView_Sorted");
                    applySort<SoftwareRow>(RequestType.SOFTWARES, softwares1GridView, softwares1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenses1GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenses1GridView_Sorted");
                    applySort<LicenseRow>(RequestType.SOFTWARE_LICENCES_OFF, licenses1GridView, licenses1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void versions1GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("versions1GridView_Sorted");
                    applySort<VersionRow>(RequestType.SOFTWARE_VERSIONS_OFF, versions1GridView, versions1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwares2GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwares2GridView_Sorted");
                    applySort<SoftwareRow>(RequestType.SOFTWARES, softwares2GridView, softwares2Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareLicenses3GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareLicenses3GridView_Sorted");
                    applySort<LicenseRow>(RequestType.SOFTWARE_LICENCES_OFF, softwareLicenses3GridView, softwareLicenses3Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersions4GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareVersions4GridView_Sorted");
                    applySort<VersionRow>(RequestType.SOFTWARE_VERSIONS_OFF, softwareVersions4GridView, softwareVersions4Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareCustomers5GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareCustomers5GridView_Sorted");
                    applySort<LicenseRow>(RequestType.SOFTWARE_CUSTOMERS, softwareCustomers5GridView, softwareCustomers5Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsers6GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenseServerUsers6GridView_Sorted");
                    applySort<UserRow>(RequestType.USERS, licenseServerUsers6GridView, licenseServerUsers6Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersions7GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareVersions7GridView_Sorted");
                    applySort<VersionRow>(RequestType.SOFTWARE_VERSIONS_ON, softwareVersions7GridView, versions7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenses7GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenses7GridView_Sorted");
                    applySort<LicenseRow>(RequestType.SOFTWARE_LICENCES_ON, licenses7GridView, licenses7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsers7GridView_Sorted(object sender, EventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenseServerUsers7GridView_Sorted");
                    applySort<UserRow>(RequestType.USERS, licenseServerUsers7GridView, licenseServerUsers7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwares1GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwares1GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<SoftwareRow>), RequestType.SOFTWARES, softwares1GridView, softwares1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenses1GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenses1GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<LicenseRow>), RequestType.SOFTWARE_LICENCES_OFF, licenses1GridView, licenses1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void versions1GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("versions1GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<VersionRow>), RequestType.SOFTWARE_VERSIONS_OFF, versions1GridView, versions1Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwares2GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwares2GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<SoftwareRow>), RequestType.SOFTWARES, softwares2GridView, softwares2Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareLicenses3GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareLicenses3GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<LicenseRow>), RequestType.SOFTWARE_LICENCES_OFF, softwareLicenses3GridView, softwareLicenses3Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersions4GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareVersions4GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<VersionRow>), RequestType.SOFTWARE_VERSIONS_OFF, softwareVersions4GridView, softwareVersions4Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareCustomers5GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareCustomers5GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<LicenseRow>), RequestType.SOFTWARE_CUSTOMERS, softwareCustomers5GridView, softwareCustomers5Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsers6GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenseServerUsers6GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<UserRow>), RequestType.USERS, licenseServerUsers6GridView, licenseServerUsers6Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void softwareVersions7GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("softwareVersions7GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<VersionRow>), RequestType.SOFTWARE_VERSIONS_ON, softwareVersions7GridView, versions7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenses7GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenses7GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<LicenseRow>), RequestType.SOFTWARE_LICENCES_ON, licenses7GridView, licenses7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }

        private void licenseServerUsers7GridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_gettingItemsFromServer_)
            {
                try
                {
                    _gettingItemsFromServer_ = true;

                    Console.WriteLine("licenseServerUsers7GridView_Scroll");
                    BeginInvoke(new ScrollDelegate(doScroll<UserRow>), RequestType.USERS, licenseServerUsers7GridView, licenseServerUsers7Nav);
                }
                finally
                {
                    _gettingItemsFromServer_ = false;
                }
            }
        }
    }

    class TabPage
    {
        protected internal TabPage(int id, string title) {
            this.Id = id;
            this.Title = title;
        }
        public int Id { get; set; }
        public string Title { get; set; }
    }

    internal class TabPage1 : TabPage {
        public static TabPage1 OVERVIEW = new TabPage1(0,"OverView");
        public static TabPage1 SOFTWARES = new TabPage1(1,"Softwares");
        public static TabPage1 SOFTWARE_LICENSES = new TabPage1(2,"Software Licenses");
        public static TabPage1 SOFTWARE_VERSIONS = new TabPage1(3,"Software Versions");
        public static TabPage1 SOFTWARE_CUSTOMERS = new TabPage1(4,"Software Customers");
        public static TabPage1 LICENSE_SERVER_USERS = new TabPage1(5,"License Server Users");
        public static TabPage1 SHOW_ONLINE_COLLECTIONS = new TabPage1(6,"Show Online Collections");

        TabPage1(int id, string title)
            : base(id, title)
        {
        }
    }

    internal class TabPage2 : TabPage
    {
        public static TabPage2 LICENSES = new TabPage2(0,"Licenses");
        public static TabPage2 VERSIONS = new TabPage2(1,"Versions");

        TabPage2(int id, string title)
            : base(id, title)
        {
        }
    }

    internal class TabPage3 : TabPage
    {
        public static TabPage3 SOFTWARE_VERSIONS = new TabPage3(0,"Software Versions");
        public static TabPage3 LICENSES = new TabPage3(1,"Licenses");
        public static TabPage3 LICENSE_SERVER_USERS = new TabPage3(2,"License Server Users");

        TabPage3(int id, string title)
            : base(id, title)
        {
        }
    }
}
