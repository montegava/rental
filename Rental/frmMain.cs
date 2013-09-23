using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DAL;
using Rental.frm;
using System.Linq;


using log4net;
using log4net.Config;


namespace Rental
{
    public delegate void SetUIProgress();
    public delegate void UIupdate();
    public delegate void SetPageCountForLoad(int count);
    public delegate void SetPageCountLoaded(int count);
    public delegate bool CheckCansel();

    public partial class frmMain : Form
    {
        public static readonly ILog testLogger = LogManager.GetLogger("TestApplication"); 
      

        // List of tab 
        private Dictionary<string, TabPage> pages = new Dictionary<string, TabPage>();
        private volatile int page_count_for_load = 0;
        private volatile int page_count_loaded = 0;
        // Спарсенные за раз объявы
        private List<Advert> m_adverts = new List<Advert>();
        // Список исключений = телефоны, слова
        private List<black_list> m_exclude;
        private BindingSource bindingSource = new BindingSource();


        #region LoadSaveClose
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            testLogger.Error("Test log"); 

            //Login
            frmLogin frm = new frmLogin(this);
          // frm.ShowDialog();


            SetFormSize();
            tabControlAdvList.SelectedIndex = 1;
            FillTree();
            OnVisible(tree.Nodes[0]);

            cbSites.Items.Clear();
            cbSites.ComboBox.DataSource = Convetor.GetParcingModeDataSource();
            cbSites.ComboBox.DisplayMember = "Value";
            cbSites.ComboBox.ValueMember = "Key";

            //cbSites.ComboBox.Selectedva = Properties.Settings.Default.cbSites != null ? Properties.Settings.Default.cbSites : -1;

        }

        private void FillTree()
        {
            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
            {
                var page = mainTabControl.TabPages[i];
                pages.Add(page.Name, page);
                tree.Nodes.Add(page.Name, page.Text);
            }
            tree.ExpandAll();
        }

        /// <summary>
        /// Load data if user click left menu
        /// </summary>
        /// <param name="name"></param>
        private void OnVisible(TreeNode node)
        {
            mainTabControl.TabPages.Clear();
            if (node != null && pages != null && pages[node.Name] != null)
            {
                mainTabControl.TabPages.Add(pages[node.Name]);
                switch (node.Name)
                {
                    case "tabBlackList":
                        FillBlackList();
                        return;
                    case "tabStar":
                        FillFlatGridStar();
                        return;
                }
            }
        }

        /// <summary>
        /// Load windows size
        /// </summary>
        private void SetFormSize()
        {
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
            this.Width = Properties.Settings.Default.Width;
            this.Height = Properties.Settings.Default.Length;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.cbSites = ((int)cbSites.ComboBox.SelectedValue);
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Length = this.Height;
            Properties.Settings.Default.Top = this.Top;
            Properties.Settings.Default.Left = this.Left;
            Properties.Settings.Default.Save();
        }

        #endregion

        /// <summary>
        /// Get black list from db and fill table
        /// </summary>
        private void FillBlackList()
        {
            dataGridViewContactList.DataSource = BlackListManager.GetAllBlackListsAsDataTable();
            dataGridViewContactList.Columns["ID"].Visible = dataGridViewContactList.Columns["TYPE_ID"].Visible = false;
            dataGridViewContactList.Columns["STOP"].Width = dataGridViewContactList.Columns["COMMENT"].Width = 200;
        }

        private void FillFlatGridStar()
        {
            bindingSource.DataSource = FlatManager.GetAllFlatsAsDataTable();
            grdFlats.DataSource = bindingSource;
            int displayIndex = 0;
            Common.SetColumlOption(grdFlats, "ID", "№", 45, ref displayIndex);
            Common.SetColumlOption(grdFlats, "DATA", "ДАТА ДОБВЛЕНИЯ", 102, ref displayIndex);
            Common.SetColumlOption(grdFlats, "ROOM_COUNT", "КОЛИЧЕСТВО КОМНАТ", 107, ref displayIndex);
            Common.SetColumlOption(grdFlats, "ADDRESS", "АДРЕС", 200, ref displayIndex);
            Common.SetColumlOption(grdFlats, "PHONE", "ТЕЛЕФОН", 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, "FURNITURE", "МЕБЕЛЬ", 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, "PRICE", "ЦЕНА", 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, "LESSOR", "КТО СДАЛ", 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, "FLOOR", "ЭТАЖ", 45, ref displayIndex);
            Common.SetColumlOption(grdFlats, "BATH_UNIT", "САНУЗЕЛ", 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, "BUILD", "ВИД ДОМА", 85, ref displayIndex);
            Common.SetColumlOption(grdFlats, "STATE", "СОСТОЯНИЕ", 85, ref displayIndex);
            Common.SetColumlOption(grdFlats, "MECHANIC", "МЕБЕЛЬ", 85, ref displayIndex);
            Common.SetColumlOption(grdFlats, "NAME", "ФИО", 100, ref displayIndex);
            Common.SetColumlOption(grdFlats, "TERM", "СРОК СДАЧИ", 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, "RENT_FROM", "СДАЕТСЯ С", 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, "RENT_TO", "СДАЕТСЯ ПО", 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, "REGION", "РАЙОН", 80, ref displayIndex);

          


        }

        #region Worker
        private void BeforeStart()
        {
            menuStart.Enabled = btnPlay.Enabled = false;
            lvAdverts.Items.Clear();
            lvStars.Items.Clear();
        }

        private void AfterEnd()
        {
            menuStart.Enabled = btnPlay.Enabled = btnStop.Enabled = true;
            AdvertToListView(m_adverts);
            cbCountAll.Text = lvAdverts.Items.Count.ToString();
            cbFilteredCount.Text = lvStars.Items.Count.ToString();
            ProgressBar.Value = 0;
            tabControlAdvList.SelectedIndex = 1;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AfterEnd();
            MessageBox.Show("Загрузка завершена");
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Start();
        }

        private void Start()
        {
            Log.Append("Begin download");
            #region Get Stor words
            if (m_exclude != null)
                m_exclude.Clear();
            m_exclude = BlackListManager.GetAllBlackLists();

            if (m_exclude != null && m_exclude.Count != 0)
                Log.Append("Exclude list count: " + m_exclude.Count);
            else
                Log.Append("Exclude list is emty");
            #endregion

            page_count_for_load = 0;
            page_count_loaded = 0;

            if (m_adverts != null)
                m_adverts.Clear();

            switch ((ParcingMode)Properties.Settings.Default.cbSites)
            //switch ((ParcingMode)cbSites.ComboBox.SelectedValue)
            {
                case ParcingMode.pmAll:

                    var sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    getMoyaReklama(1);
                    getMoyaReklama(2);
                    getMoyaReklama(3);
                    sw.Stop();
                    var MoyaReklamaElapsed = sw.Elapsed;

                    sw.Restart();
                    GetCamelot(true);
                    GetCamelot(false);
                    sw.Stop();
                    var CamelotElapsed = sw.Elapsed;

                    sw.Restart();
                    GetIRR();
                    sw.Stop();
                    var IRRElapsed = sw.Elapsed;

                    sw.Restart();
                    GetAvito(true);
                    GetAvito(false);
                    sw.Stop();
                    var AvitoElapsed = sw.Elapsed;


                    sw.Restart();
                    GetSlando(true);
                    GetSlando(false);
                    sw.Stop();
                    var SlandoElapsed = sw.Elapsed;

                    //MessageBox.Show(String.Format("Моя реклама: {0}\r\nCamelot: {1}\r\nIRR: {2}\r\nAvito: {3}\r\nSlando: {4}", MoyaReklamaElapsed, CamelotElapsed, IRRElapsed, AvitoElapsed, SlandoElapsed));
                    break;
                case ParcingMode.pmCamelotFlat:
                    GetCamelot(true);
                    break;
                case ParcingMode.pmCamelotHouse:
                    GetCamelot(false);
                    break;
                case ParcingMode.pmMoyaReklama1:
                    getMoyaReklama(1);
                    break;
                case ParcingMode.pmMoyaReklama2:
                    getMoyaReklama(2);
                    break;
                case ParcingMode.pmMoyaReklama3:
                    getMoyaReklama(3);
                    break;
                case ParcingMode.pmIRR:
                    GetIRR();
                    break;
                case ParcingMode.pmAvitoFlat:
                    GetAvito(true);
                    break;
                case ParcingMode.pmAvitoHouse:
                    GetAvito(false);
                    break;
                case ParcingMode.pmSlandoFlat:
                    GetSlando(true);
                    break;
                case ParcingMode.pmSlandoHouse:
                    GetSlando(false);
                    break;
            }
        }

        private void getMoyaReklama(int type)
        {
            #region Load 1st page
            Log.Append("MoyaReklama 1 ");

            string urlPattern = Const.CNT_MOYAREKLAMA_URL_1;
            switch (type)
            {
                case 1:
                    urlPattern = Const.CNT_MOYAREKLAMA_URL_1;
                    break;
                case 2:
                    urlPattern = Const.CNT_MOYAREKLAMA_URL_2;
                    break;
                case 3:
                    urlPattern = Const.CNT_MOYAREKLAMA_URL_3;
                    break;
            }

            string url = String.Format(urlPattern, 1);
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel())
                return;
            #endregion

            if (!String.IsNullOrEmpty(error))
                Log.Append("\tERROR on load page " + url + "  " + error);
            else
            {

                MoyaReklama mr = new MoyaReklama(m_exclude);
                mr.onSetUIProgress += onSetUIProgress;
                mr.onSetPageCountForLoad += onSetPageCountForLoad;
                mr.onSetPageCountLoaded += onSetPageCountLoaded;
                mr.onCheckCansel += onCheckCansel;


                #region Get page count
                int page_count = 0;
                var m = Regex.Match(page, @"<div class=""search-counter"">.*?([\d]*?)</div>", RegexOptions.Singleline);
                if (m.Success && m.Groups.Count > 1)
                {
                    int all = Int32.Parse(m.Groups[1].ToString());
                    page_count = ((int)(all / 15)) + (all % 15 > 0 ? 1 : 0);

                }
                Log.Append("\tPage count: " + page_count);
                #endregion

                m_adverts.AddRange(mr.GetAdvertList(page));
                if (page_count > 0)
                    onSetPageCountForLoad(page_count - 1);
                onSetUIProgress();

                if (page_count >= 3)
                    page_count = 3;
                for (int i = 2; i <= page_count; i++)
                {
                    if (onCheckCansel())
                        return;
                    page = WebPage.LoadPage(String.Format(urlPattern, i), Encoding.GetEncoding("utf-8"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (!String.IsNullOrEmpty(error))
                        Log.Append(error);
                    else
                        m_adverts.AddRange(mr.GetAdvertList(page));
                }


            }
        }

        private void GetCamelot(bool forFlat = true)
        {
            #region Load 1st page
            Log.Append("CAMELOT " + (forFlat ? "flat" : "house"));
            string url = forFlat ? Const.CNT_CAMELOT_FLAT_URL : Const.CNT_CAMELOT_BUILD_URL;
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("windows-1251"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel())
                return;
            #endregion

            if (!String.IsNullOrEmpty(error))
                Log.Append("\tERROR on load page " + url + "  " + error);
            else
            {
                DateTime posterDate = Camelot.GetPosterDate(page);
                Log.Append("\t Current poster date: " + posterDate.ToShortDateString());

                #region Get page count and url pattern
                int page_count = 0;
                string url_pattern = String.Empty;
                var m = Regex.Match(page, @"MainInfoText.*<a[\s]class=""pagesController""[\s]href=""(.*?)"".*Предыдущая", RegexOptions.Singleline);
                if (m.Success && m.Groups.Count > 1)
                {
                    url_pattern = m.Groups[1].ToString().Trim();
                    m = Regex.Match(url_pattern, @"\-page\-([\d]*)");
                    if (m.Success && m.Groups.Count > 1)
                    {
                        try
                        {
                            page_count = Int32.Parse(m.Groups[1].ToString());
                            url_pattern = Const.CNT_CAMELOT_DOMAIN + url_pattern.Replace("-page-" + page_count + "-", "-page-{0}-");
                            Log.Append("\tPage count: " + page_count.ToString());
                        }
                        catch (Exception ex)
                        {
                            Log.Append("\tERROR Camelot convert count: " + ex.Message);
                        }
                    }
                    else
                        Log.Append("\tERROR on Camelot: Can't find max page count");
                }
                else
                    Log.Append("\tERROR on Camelot: Can't find max page count");
                #endregion

                Camelot camelot = new Camelot(m_exclude);
                camelot.onSetUIProgress += onSetUIProgress;
                camelot.onSetPageCountForLoad += onSetPageCountForLoad;
                camelot.onSetPageCountLoaded += onSetPageCountLoaded;
                camelot.onCheckCansel += onCheckCansel;

                if (page_count > 1)
                    onSetPageCountForLoad(page_count - 1);
                onSetPageCountLoaded(1);
                onSetUIProgress();

                m_adverts.AddRange(camelot.GetAdvertList(page, posterDate));

                //3. Get All adv
                for (int i = 2; i <= page_count; i++)
                {
                    if (onCheckCansel())
                        return;
                    string next_page = String.Format(url_pattern, i.ToString());
                    page = WebPage.LoadPage(next_page, Encoding.GetEncoding("windows-1251"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (!String.IsNullOrEmpty(error))
                        Log.Append(error);
                    else
                        m_adverts.AddRange(camelot.GetAdvertList(page, posterDate));
                }
            }
        }

        private void GetIRR()
        {
            #region Load 1st page
            Log.Append("IRR");
            string error;
            string page = WebPage.LoadPage(Const.CNT_IRR_URL, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel())
                return;
            #endregion

            if (!String.IsNullOrEmpty(error))
                Log.Append("\tERROR on load page " + Const.CNT_IRR_URL + "  " + error);
            else
            {
                #region Get page count
                int page_count = 0;
                var m = Regex.Match(page, @"<span>...</span>[\s]*</li>[\s]*<li>[\s]*<a[\s]href="".*?"">(.*?)</a>", RegexOptions.Singleline);
                if (!m.Success || (m.Groups.Count < 2) || !(Int32.TryParse(m.Groups[1].ToString(), out page_count)))
                {
                    Log.Append("\tERROR on Camelot: Can't find max page count");
                    return;
                }
                if (page_count > 5)
                    page_count = 5;
                Log.Append("\tCamelot tPage count: " + page_count);
                #endregion

                IRR irr = new IRR(m_exclude);
                irr.onSetUIProgress += onSetUIProgress;
                irr.onSetPageCountForLoad += onSetPageCountForLoad;
                irr.onSetPageCountLoaded += onSetPageCountLoaded;
                irr.onCheckCansel += onCheckCansel;

                if (page_count > 1)
                    onSetPageCountForLoad(page_count - 1);
                onSetPageCountLoaded(1);
                onSetUIProgress();

                m_adverts.AddRange(irr.GetAdvertList(page));

                //3. Get All adv
                for (int i = 2; i <= page_count; i++)
                {
                    if (onCheckCansel())
                        return;
                    page = WebPage.LoadPage(Const.CNT_IRR_URL + "page" + i + "/", Encoding.GetEncoding("windows-1251"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (!String.IsNullOrEmpty(error))
                        Log.Append(error);
                    else
                        m_adverts.AddRange(irr.GetAdvertList(page));
                }
            }
        }

        private void GetAvito(bool forFlat = true)
        {
            #region Load 1st page
            Log.Append("AVITO " + (forFlat ? "flat" : "house"));
            string url = forFlat ? Const.CNT_AVITO_FLAT_URL : Const.CNT_AVITO_BUILD_URL;
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel())
                return;
            #endregion

            if (!String.IsNullOrEmpty(error))
                Log.Append("\tERROR on load page " + url + "  " + error);
            else
            {
                //2. Get page count
                int page_count = forFlat ? 3 : 1;
                Log.Append("\tPage count: " + page_count.ToString());

                if (bw.CancellationPending)
                    return;
                Avito avito = new Avito(m_exclude);
                avito.onSetUIProgress += onSetUIProgress;
                avito.onSetPageCountForLoad += onSetPageCountForLoad;
                avito.onSetPageCountLoaded += onSetPageCountLoaded;
                avito.onCheckCansel += onCheckCansel;

                if (page_count > 1)
                    onSetPageCountForLoad(page_count - 1);
                onSetPageCountLoaded(1);
                onSetUIProgress();

                m_adverts.AddRange(avito.GetAdvertList(page));

                //3. Get All adv
                for (int i = 2; i <= page_count; i++)
                {
                    if (onCheckCansel())
                        return;
                    page = WebPage.LoadPage(url + "/page" + i, Encoding.GetEncoding("windows-1251"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (!String.IsNullOrEmpty(error))
                        Log.Append(error);
                    else
                        m_adverts.AddRange(avito.GetAdvertList(page));
                }
            }
        }

        private void GetSlando(bool forFlat = true)
        {
            Log.Append("Slando");
            string url = forFlat ? Const.CNT_SLANDO_FLAT_URL : Const.CNT_SLANDO_BUILD_URL;
            Log.Append("-----------------------Page number: 1");
            Log.Append("\tLoading....: " + url);
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel()) return;
            if (!String.IsNullOrEmpty(error))
                Log.Append("\tERROR on Avito load page " + url + "  " + error);
            else
            {
                Log.Append("\tPage size: " + page.Length);
                int page_count = forFlat ? 3 : 1;
                if (bw.CancellationPending) return;
                Slando slando = new Slando(m_exclude);
                slando.onSetUIProgress += onSetUIProgress;
                slando.onSetPageCountForLoad += onSetPageCountForLoad;
                slando.onSetPageCountLoaded += onSetPageCountLoaded;
                slando.onCheckCansel += onCheckCansel;

                if (page_count > 1)
                    onSetPageCountForLoad(page_count - 1);
                onSetPageCountLoaded(1);
                onSetUIProgress();

                //Get 1st page
                if (m_adverts == null) m_adverts = new List<Advert>();
                Log.Append("\tCollect links");
                m_adverts.AddRange(slando.GetAdvertList(page));
                //3. Get All adv
                for (int i = 2; i <= page_count; i++)
                {
                    Log.Append("-----------------------Page number: " + i);
                    if (onCheckCansel()) return;
                    Log.Append("\tLoading....: " + url + "?page=" + i);
                    page = WebPage.LoadPage(url + "?page=" + i, Encoding.GetEncoding("windows-1251"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (!String.IsNullOrEmpty(error))
                        Log.Append("\tERROR on Avito load page " + url + "/page" + i + "  " + error);
                    else
                    {
                        Log.Append("\tPage size: " + page.Length);
                        Log.Append("\tCollect links");
                        m_adverts.AddRange(slando.GetAdvertList(page));
                    }
                }
            }
        }

        private void onSetUIProgress()
        {
            if (InvokeRequired)
            {
                SetUIProgress d = new SetUIProgress(onSetUIProgress);
                Invoke(d);
            }
            else
            {
                ProgressBar.Maximum = page_count_for_load;
                if (page_count_loaded >= 0 && page_count_loaded <= ProgressBar.Maximum)
                    ProgressBar.Value = page_count_loaded;
                lblCounter.Text = String.Format("{0}/{1}", page_count_for_load, page_count_loaded);
            }
        }

        private void onSetPageCountForLoad(int count)
        {
            page_count_for_load += count;
        }

        private void onSetPageCountLoaded(int count)
        {
            page_count_loaded += count;
        }

        private bool onCheckCansel()
        {
            return bw.CancellationPending;
        }
        #endregion


        private void AdvertToListView(List<Advert> advertList)
        {
            #region Save current cursor position
            int cursorPossitionAll = 0;
            int cursorPossitionFiltered = 0;
            if (lvAdverts.SelectedItems.Count > 0)
                cursorPossitionAll = lvAdverts.Items.IndexOf(lvAdverts.SelectedItems[0]);
            if (lvStars.SelectedItems.Count > 0)
                cursorPossitionFiltered = lvStars.Items.IndexOf(lvStars.SelectedItems[0]);
            #endregion

            lvAdverts.BeginUpdate();
            lvStars.BeginUpdate();

            lvAdverts.Items.Clear();
            lvStars.Items.Clear();

            try
            {
                if (advertList != null)
                {


                    for (int i = 0; i < advertList.Count; i++)
                    {
                        ListViewItem item = new ListViewItem((lvAdverts.Items.Count + 1).ToString());
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, advertList[i].PhonesAsString()));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, advertList[i].Content));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, advertList[i].Link));
                        item.Tag = advertList[i];


                        if (advertList[i].IsStar)
                            item.ImageIndex = (int)ImageMode.imStar;
                        if (advertList[i].IsBlocked)
                            item.StateImageIndex = advertList[i].ImageIndex + 1;
                        else
                            item.StateImageIndex = advertList[i].ImageIndex;

                        lvAdverts.Items.Add(item);

                        if (!advertList[i].IsBlocked && !advertList[i].IsStar)
                        {
                            ListViewItem itemFilter = new ListViewItem((lvStars.Items.Count + 1).ToString());
                            itemFilter.SubItems.Add(new ListViewItem.ListViewSubItem(itemFilter, advertList[i].HasPhoto ? "+" : ""));


                            itemFilter.SubItems.Add(new ListViewItem.ListViewSubItem(itemFilter, advertList[i].PhonesAsString()));
                            itemFilter.SubItems.Add(new ListViewItem.ListViewSubItem(itemFilter, advertList[i].Content));
                            itemFilter.SubItems.Add(new ListViewItem.ListViewSubItem(itemFilter, advertList[i].Link));
                            itemFilter.Tag = advertList[i];

                            if (advertList[i].IsStar)
                                itemFilter.ImageIndex = (int)ImageMode.imStar;
                            itemFilter.StateImageIndex = advertList[i].ImageIndex;
                            lvStars.Items.Add(itemFilter);
                        }

                    }

                    if (cursorPossitionAll < lvAdverts.Items.Count && cursorPossitionAll >= 0)
                        lvAdverts.Items[cursorPossitionAll].Selected = true;
                    else
                    {
                        cursorPossitionAll--;
                        if (cursorPossitionAll < lvAdverts.Items.Count && cursorPossitionAll >= 0)
                            lvAdverts.Items[cursorPossitionAll].Selected = true;
                    }

                    if (cursorPossitionFiltered < lvStars.Items.Count && cursorPossitionFiltered >= 0)
                        lvStars.Items[cursorPossitionFiltered].Selected = true;
                    else
                    {
                        cursorPossitionFiltered--;
                        if (cursorPossitionFiltered < lvStars.Items.Count && cursorPossitionFiltered >= 0)
                            lvStars.Items[cursorPossitionFiltered].Selected = true;
                    }

                }
            }
            finally
            {

                lvAdverts.EndUpdate();
                lvStars.EndUpdate();
            }


        }

        /////////////////

        private void btnStart_Click(object sender, EventArgs e)
        {
            BeforeStart();
            bw.RunWorkerAsync();
        }

        private void menuStart_Click(object sender, EventArgs e)
        {
            BeforeStart();
            bw.RunWorkerAsync();
        }

        private void lvAdverts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv.SelectedItems.Count > 0)
            {
                Advert c = lv.SelectedItems[0].Tag as Advert;
                lblLink.Text = c.Link;
                tbContent.Text = c.Content;
                lbContacts.Items.Clear();
                foreach (string contact in c.Phones)
                    lbContacts.Items.Add(String.Format("{0:##-####-####}", contact));
            }
        }

        private void lblLink_Click(object sender, EventArgs e)
        {
            if (lblLink.Text.Length > 0)
                System.Diagnostics.Process.Start(lblLink.Text);
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnVisible(tree.SelectedNode);
        }

        private void btnAddBlackPhoneGrid_Click(object sender, EventArgs e)
        {
            Add2BlackList();
        }

        /// <summary>
        /// Удалить стоп слово
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteBlackWord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выражение будет удалено из базы. Продолжить", "Удаление...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Int32 blackId;
            if (Int32.TryParse(dataGridViewContactList.CurrentRow.Cells[0].Value.ToString(), out blackId))
            {
                string error = null;
                if (!BlackListManager.DeleteBlackWord(blackId, out error))
                    MessageBox.Show(error);
                else
                    FillBlackList();
            }

        }

        /// <summary>
        /// Удалить с избранного
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStarDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Объявление будет удалено из базы. Продолжить", "Удаление...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Int32 flatId;
            if (Int32.TryParse(grdFlats.CurrentRow.Cells[0].Value.ToString(), out flatId))
            {
                string error = null;
                if (!FlatManager.DeleteFlat(flatId, out error))
                    MessageBox.Show(error);
                else
                    FillFlatGridStar();
            }
        }

        /// <summary>
        /// Редактировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Int32 blackId;
            if (Int32.TryParse(dataGridViewContactList.CurrentRow.Cells[0].Value.ToString(), out blackId))
            {
                int ri = dataGridViewContactList.SelectedRows[0].Index;
                frmBlackItem frm = new frmBlackItem(blackId);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    FillBlackList();
                    dataGridViewContactList.Rows[ri].Selected = true;
                }
            }

        }

        private void dataGridViewContactList_DoubleClick(object sender, EventArgs e)
        {
            toolStripButton2_Click(null, null);
        }

        private void btnReloadBlackList_Click(object sender, EventArgs e)
        {
            FillBlackList();
        }



        private void onAddContacts2BlackList(Advert advert)
        {
            frmBlackItem frm = new frmBlackItem(advert.Phones);
            frm.ShowDialog();
            advert.IsBlocked = (frm.DialogResult == DialogResult.OK);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (lvAdverts.SelectedItems.Count > 0)
                onAddContacts2BlackList(lvAdverts.SelectedItems[0].Tag as Advert);
        }

        private void tspStop_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                btnStop.Enabled = false;
                bw.CancelAsync();
            }
        }

        private void cbSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSites.ComboBox.SelectedValue is int)
            {
                Properties.Settings.Default.cbSites = ((int)cbSites.ComboBox.SelectedValue);
                Properties.Settings.Default.Save();
            }
        }

        private void btnAddBlackPhone_Click(object sender, EventArgs e)
        {
            AddBlackPhone();
        }

        private void btnAddStar_Click(object sender, EventArgs e)
        {
            AddStar();
        }

        /// <summary>
        /// Пометить избранное 
        /// </summary>
        private void AddStar()
        {
            if (lvStars.SelectedItems.Count > 0)
            {
                Advert advert = lvStars.SelectedItems[0].Tag as Advert;
                if (advert != null)
                {
                    frmFlat frm = new frmFlat(advert, EditMode.emAddNew);
                    frm.ShowDialog();
                    advert.IsStar = (frm.DialogResult == DialogResult.OK);
                    if (advert.IsStar)
                        lvStars.SelectedItems[0].ImageIndex = (int)ImageMode.imStar;
                }
            }
        }

        /// <summary>
        /// Добавить контакты в блэк лист
        /// </summary>
        private void AddBlackPhone()
        {
            if (lvStars.SelectedItems.Count > 0)
            {
                Advert adv = lvStars.SelectedItems[0].Tag as Advert;
                if (adv != null)
                {
                    onAddContacts2BlackList(lvStars.SelectedItems[0].Tag as Advert);
                    if (adv.IsBlocked)
                        lvStars.SelectedItems[0].StateImageIndex = adv.ImageIndex + 1;
                }
            }
        }

        /// <summary>
        /// Добавить слово-телефон в черный список
        /// </summary>
        private void Add2BlackList()
        {
            frmBlackItem frm = new frmBlackItem();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                FillBlackList();
                dataGridViewContactList.Rows[dataGridViewContactList.Rows.Count - 2].Selected = true;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedCellCount = grdFlats.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                if (grdFlats.AreAllCellsSelected(true))
                {
                    MessageBox.Show("All cells are selected", "Selected Cells");
                }
                else
                {
                    var sb = new StringBuilder();

                    inputNAME.Text = grdFlats.SelectedRows[0].Cells["NAME"].Value.ToString();
                    inputCONTENT.Text = grdFlats.SelectedRows[0].Cells["CONTENT"].Value.ToString();
                    inputLINK.Text = grdFlats.SelectedRows[0].Cells["LINK"].Value.ToString();
                    intupROOM_COUNT.Text = grdFlats.SelectedRows[0].Cells["ROOM_COUNT"].Value.ToString();
                    intupADDRESS.Text = grdFlats.SelectedRows[0].Cells["ADDRESS"].Value.ToString();
                    intupFLOOR.Text = grdFlats.SelectedRows[0].Cells["FLOOR"].Value.ToString();
                    intupBATH_UNIT.Text = grdFlats.SelectedRows[0].Cells["BATH_UNIT"].Value.ToString();
                    intupBUILD.Text = grdFlats.SelectedRows[0].Cells["BUILD"].Value.ToString();
                    intupSTATE.Text = grdFlats.SelectedRows[0].Cells["STATE"].Value.ToString();
                    intupPRICE.Text = grdFlats.SelectedRows[0].Cells["PRICE"].Value.ToString();
                    inputPHONE.Items.Clear();
                    inputPHONE.Items.AddRange(grdFlats.SelectedRows[0].Cells["PHONE"].Value.ToString().Split(new Char[] { ';' }));

                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 flatId;
            try
            {
                flatId = Convert.ToInt32(grdFlats.CurrentRow.Cells[0].Value);
                var flat = new frmFlat(EditMode.emEdit, flatId);
                flat.ShowDialog();
                if (flat.DialogResult == DialogResult.OK)
                    FillFlatGridStar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении: " + ex.Message);
            }
        }


        private void inputLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (inputLINK.Text.Trim().Length > 0)
                System.Diagnostics.Process.Start(inputLINK.Text);
        }


        private void btnStarEdit_Click(object sender, EventArgs e)
        {
            dataGridView1_CellDoubleClick(null, null);
        }

        private void btnStarReload_Click(object sender, EventArgs e)
        {
            FillFlatGridStar();
        }

        /// <summary>
        /// Красим строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdFlats.Rows[e.RowIndex];
                string lessor = row.Cells["LESSOR"].Value.ToString().Trim();
                if (lessor.Length > 0 && !lessor.Contains("неизвестно"))
                    row.DefaultCellStyle.BackColor = Color.Crimson;
            }
            catch (Exception) { }
        }

        public void OnLoadLoginLogin(string username, string password, object sender)
        {
            try
            {
                frmLogin loginForm = null;
                if (sender != null && sender is frmLogin)
                    loginForm = (frmLogin)sender;

                if (!(username == "pangasius" && password == "amira23121978"))
                {
                    if (loginForm == null)
                        new frmLogin(this).ShowDialog();
                    else
                    {
                        loginForm.BringToFront();
                        loginForm.ButtonLoginEnabled = true;
                    }
                }
                else if (loginForm != null)
                    loginForm.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Detailed Error Message:\r\n" + Ex + "\r\n\r\nClick \"OK\" to exit the application", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStarAdd_Click(object sender, EventArgs e)
        {
            frmFlat frm = new frmFlat(null, EditMode.emAddNew);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int fid = frm.FlatId;
                FillFlatGridStar();
                ((CurrencyManager)this.BindingContext[grdFlats.DataSource]).Position = 0;


            }
        }

        private void lvStars_DoubleClick(object sender, EventArgs e)
        {
            AddStar();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }
    }

}
