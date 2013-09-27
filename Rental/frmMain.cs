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
using Rental.src;
using RentalCommon;

namespace Rental
{
    public delegate void SetUIProgress();
    public delegate void UIupdate();
    public delegate void SetPageCountForLoad(int count);
    public delegate void SetPageCountLoaded(int count);
    public delegate bool CheckCansel();

    public partial class frmMain : Form
    {
        public static readonly ILog Log = LogManager.GetLogger("TestApplication");
        private BackgroundWorker Worker = new BackgroundWorker();

        // List of tab 
        private Dictionary<string, TabPage> pages = new Dictionary<string, TabPage>();
        private volatile int page_count_for_load = 0;
        private volatile int page_count_loaded = 0;


        private List<Advert> AdvertList = new List<Advert>();

        private List<black_list> BlackList = new List<black_list>();

        NameListCache Cache = null;
        const int PAGE_SIZE = 5000;


        #region LoadSaveClose
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Log.Debug("Application started.");

            Worker.DoWork += this.DoWork;
            Worker.RunWorkerCompleted += this.RunWorkerCompleted;
            Worker.WorkerSupportsCancellation = true;
            //Login
            frmLogin frm = new frmLogin(this);
            // frm.ShowDialog();


            LoadFormSize();
            tabControlAdvList.SelectedIndex = 1;
            FillTree();
            OnVisible(tree.Nodes[0]);

            cbSites.Items.Clear();
            cbSites.ComboBox.DataSource = Convetor.GetParcingModeDataSource();
            cbSites.ComboBox.DisplayMember = "Value";
            cbSites.ComboBox.ValueMember = "Key";


            this.Cache = new NameListCache(PAGE_SIZE);


            int displayIndex = 0;
            Common.SetColumlOption(grdFlats, Fields.ID, 45, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.DATA, 102, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.ROOM_COUNT, 107, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.ADDRESS, 200, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.PHONE, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.FURNITURE, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.PRICE, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.LESSOR, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.FLOOR, 45, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.BATH_UNIT, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.BUILD, 85, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.STATE, 85, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.MECHANIC, 85, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.NAME, 100, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.TERM, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.RENT_FROM, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.RENT_TO, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.REGION, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.CONTENT, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.LINK, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.EMAIL, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.TYPE, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.CATEGORY, 80, ref displayIndex);
            Common.SetColumlOption(grdFlats, Fields.PAYMENT, 80, ref displayIndex);

            grdFlats.CellValueNeeded += new DataGridViewCellValueEventHandler(dataGridView1_CellValueNeeded);
            grdFlats.RowCount = Cache.TotalRowsNumber;

            grdFlats.VirtualMode = true;



            //cbSites.ComboBox.Selectedva = Properties.Settings.Default.cbSites != null ? Properties.Settings.Default.cbSites : -1;

        }


        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            Cache.LoadPage(e.RowIndex);
            int rowIndex = e.RowIndex % Cache.PageSize;
            e.Value = Cache.CachedData[rowIndex][grdFlats.Columns[e.ColumnIndex].Name];
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
                        BlackListRefresh();
                        return;
                    case "tabStar":
                        //FillFlatGridStar();
                        return;
                }
            }
        }

        /// <summary>
        /// Load windows size
        /// </summary>
        private void LoadFormSize()
        {
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
            this.Width = Properties.Settings.Default.Width;
            this.Height = Properties.Settings.Default.Length;
        }



        #endregion

        #region Worker

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            menuStart.Enabled = true;
            btnStart.Enabled = true;
            Convetor.AdvertToListView(AdvertList, lvAdverts, lvStars);

            cbCountAll.Text = lvAdverts.Items.Count.ToString();
            cbFilteredCount.Text = lvStars.Items.Count.ToString();
            ProgressBar.Value = 0;
            tabControlAdvList.SelectedIndex = 1;
            MessageBox.Show("Загрузка завершена");
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Log.Info("Begin download");

            page_count_for_load = 0;
            page_count_loaded = 0;
            BlackList.Clear();
            AdvertList.Clear();

            BlackList.AddRange(NameListCache.proxy.BlackListAll());

            switch ((ParcingMode)Properties.Settings.Default.cbSites)
            {
                case ParcingMode.All:

                    getMoyaReklama(1);
                    getMoyaReklama(2);
                    getMoyaReklama(3);
                    GetCamelot(true);
                    GetCamelot(false);
                    GetIRR();
                    GetAvito(true);
                    GetAvito(false);
                    GetSlando(true);
                    GetSlando(false);
                    break;
                case ParcingMode.CamelotFlat:
                    GetCamelot(true);
                    break;
                case ParcingMode.CamelotHouse:
                    GetCamelot(false);
                    break;
                case ParcingMode.MoyaReklamaFlat:
                    getMoyaReklama(1);
                    break;
                case ParcingMode.MoyaReklamaRoom:
                    getMoyaReklama(2);
                    break;
                case ParcingMode.MoyaReklamaHouse:
                    getMoyaReklama(3);
                    break;
                case ParcingMode.IRR:
                    GetIRR();
                    break;
                case ParcingMode.AvitoFlat:
                    GetAvito(true);
                    break;
                case ParcingMode.pmAvitoHouse:
                    GetAvito(false);
                    break;
                case ParcingMode.SlandoFlat:
                    GetSlando(true);
                    break;
                case ParcingMode.SlandoHouse:
                    GetSlando(false);
                    break;
            }
        }

        private void getMoyaReklama(int type)
        {
            #region Load 1st page
            Log.Debug("MoyaReklama 1 ");

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
                Log.Debug("\tERROR on load page " + url + "  " + error);
            else
            {

                MoyaReklama mr = new MoyaReklama(BlackList);
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
                Log.Debug("\tPage count: " + page_count);
                #endregion

                AdvertList.AddRange(mr.GetAdvertList(page));
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
                        Log.Debug(error);
                    else
                        AdvertList.AddRange(mr.GetAdvertList(page));
                }


            }
        }

        private void GetCamelot(bool forFlat = true)
        {
            #region Load 1st page
            Log.Debug("CAMELOT " + (forFlat ? "flat" : "house"));
            string url = forFlat ? Const.CNT_CAMELOT_FLAT_URL : Const.CNT_CAMELOT_BUILD_URL;
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("windows-1251"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel())
                return;
            #endregion

            if (!String.IsNullOrEmpty(error))
                Log.Debug("\tERROR on load page " + url + "  " + error);
            else
            {
                DateTime posterDate = Camelot.GetPosterDate(page);
                Log.Debug("\t Current poster date: " + posterDate.ToShortDateString());

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
                            Log.Debug("\tPage count: " + page_count.ToString());
                        }
                        catch (Exception ex)
                        {
                            Log.Debug("\tERROR Camelot convert count: " + ex.Message);
                        }
                    }
                    else
                        Log.Debug("\tERROR on Camelot: Can't find max page count");
                }
                else
                    Log.Debug("\tERROR on Camelot: Can't find max page count");
                #endregion

                Camelot camelot = new Camelot(BlackList);
                camelot.onSetUIProgress += onSetUIProgress;
                camelot.onSetPageCountForLoad += onSetPageCountForLoad;
                camelot.onSetPageCountLoaded += onSetPageCountLoaded;
                camelot.onCheckCansel += onCheckCansel;

                if (page_count > 1)
                    onSetPageCountForLoad(page_count - 1);
                onSetPageCountLoaded(1);
                onSetUIProgress();

                AdvertList.AddRange(camelot.GetAdvertList(page, posterDate));

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
                        Log.Debug(error);
                    else
                        AdvertList.AddRange(camelot.GetAdvertList(page, posterDate));
                }
            }
        }

        private void GetIRR()
        {
            #region Load 1st page
            Log.Debug("IRR");
            string error;
            string page = WebPage.LoadPage(Const.CNT_IRR_URL, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel())
                return;
            #endregion

            if (!String.IsNullOrEmpty(error))
                Log.Debug("\tERROR on load page " + Const.CNT_IRR_URL + "  " + error);
            else
            {
                #region Get page count
                int page_count = 0;
                var m = Regex.Match(page, @"<span>...</span>[\s]*</li>[\s]*<li>[\s]*<a[\s]href="".*?"">(.*?)</a>", RegexOptions.Singleline);
                if (!m.Success || (m.Groups.Count < 2) || !(Int32.TryParse(m.Groups[1].ToString(), out page_count)))
                {
                    Log.Debug("\tERROR on Camelot: Can't find max page count");
                    return;
                }
                if (page_count > 5)
                    page_count = 5;
                Log.Debug("\tCamelot tPage count: " + page_count);
                #endregion

                IRR irr = new IRR(BlackList);
                irr.onSetUIProgress += onSetUIProgress;
                irr.onSetPageCountForLoad += onSetPageCountForLoad;
                irr.onSetPageCountLoaded += onSetPageCountLoaded;
                irr.onCheckCansel += onCheckCansel;

                if (page_count > 1)
                    onSetPageCountForLoad(page_count - 1);
                onSetPageCountLoaded(1);
                onSetUIProgress();

                AdvertList.AddRange(irr.GetAdvertList(page));

                //3. Get All adv
                for (int i = 2; i <= page_count; i++)
                {
                    if (onCheckCansel())
                        return;
                    page = WebPage.LoadPage(Const.CNT_IRR_URL + "page" + i + "/", Encoding.GetEncoding("windows-1251"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (!String.IsNullOrEmpty(error))
                        Log.Debug(error);
                    else
                        AdvertList.AddRange(irr.GetAdvertList(page));
                }
            }
        }

        private void GetAvito(bool forFlat = true)
        {
            #region Load 1st page
            Log.Debug("AVITO " + (forFlat ? "flat" : "house"));
            string url = forFlat ? Const.CNT_AVITO_FLAT_URL : Const.CNT_AVITO_BUILD_URL;
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel())
                return;
            #endregion

            if (!String.IsNullOrEmpty(error))
                Log.Debug("\tERROR on load page " + url + "  " + error);
            else
            {
                //2. Get page count
                int page_count = forFlat ? 3 : 1;
                Log.Debug("\tPage count: " + page_count.ToString());

                if (Worker.CancellationPending)
                    return;
                Avito avito = new Avito(BlackList);
                avito.onSetUIProgress += onSetUIProgress;
                avito.onSetPageCountForLoad += onSetPageCountForLoad;
                avito.onSetPageCountLoaded += onSetPageCountLoaded;
                avito.onCheckCansel += onCheckCansel;

                if (page_count > 1)
                    onSetPageCountForLoad(page_count - 1);
                onSetPageCountLoaded(1);
                onSetUIProgress();

                AdvertList.AddRange(avito.GetAdvertList(page));

                //3. Get All adv
                for (int i = 2; i <= page_count; i++)
                {
                    if (onCheckCansel())
                        return;
                    page = WebPage.LoadPage(url + "/page" + i, Encoding.GetEncoding("windows-1251"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (!String.IsNullOrEmpty(error))
                        Log.Debug(error);
                    else
                        AdvertList.AddRange(avito.GetAdvertList(page));
                }
            }
        }

        private void GetSlando(bool forFlat = true)
        {
            Log.Debug("Slando");
            string url = forFlat ? Const.CNT_SLANDO_FLAT_URL : Const.CNT_SLANDO_BUILD_URL;
            Log.Debug("-----------------------Page number: 1");
            Log.Debug("\tLoading....: " + url);
            string error;
            string page = WebPage.LoadPage(url, Encoding.GetEncoding("utf-8"), out error);
            onSetPageCountForLoad(1);
            onSetUIProgress();
            if (onCheckCansel()) return;
            if (!String.IsNullOrEmpty(error))
                Log.Debug("\tERROR on Avito load page " + url + "  " + error);
            else
            {
                Log.Debug("\tPage size: " + page.Length);
                int page_count = forFlat ? 3 : 1;
                if (Worker.CancellationPending) return;
                Slando slando = new Slando(BlackList);
                slando.onSetUIProgress += onSetUIProgress;
                slando.onSetPageCountForLoad += onSetPageCountForLoad;
                slando.onSetPageCountLoaded += onSetPageCountLoaded;
                slando.onCheckCansel += onCheckCansel;

                if (page_count > 1)
                    onSetPageCountForLoad(page_count - 1);
                onSetPageCountLoaded(1);
                onSetUIProgress();

                //Get 1st page
                if (AdvertList == null) AdvertList = new List<Advert>();
                Log.Debug("\tCollect links");
                AdvertList.AddRange(slando.GetAdvertList(page));
                //3. Get All adv
                for (int i = 2; i <= page_count; i++)
                {
                    Log.Debug("-----------------------Page number: " + i);
                    if (onCheckCansel()) return;
                    Log.Debug("\tLoading....: " + url + "?page=" + i);
                    page = WebPage.LoadPage(url + "?page=" + i, Encoding.GetEncoding("windows-1251"), out error);
                    onSetPageCountLoaded(1);
                    onSetUIProgress();
                    if (!String.IsNullOrEmpty(error))
                        Log.Debug("\tERROR on Avito load page " + url + "/page" + i + "  " + error);
                    else
                    {
                        Log.Debug("\tPage size: " + page.Length);
                        Log.Debug("\tCollect links");
                        AdvertList.AddRange(slando.GetAdvertList(page));
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
            return Worker.CancellationPending;
        }
        #endregion

        /// <summary>
        /// Начать загрузку
        /// </summary>
        private void Start()
        {
            menuStart.Enabled = false;
            btnStart.Enabled = false;
            lvAdverts.Items.Clear();
            lvStars.Items.Clear();
            Worker.RunWorkerAsync();
        }

        /// <summary>
        /// Завершить загрузку
        /// </summary>
        private void Stop()
        {
            if (Worker.IsBusy)
            {
                btnStop.Enabled = false;
                Worker.CancelAsync();
            }
        }

        /// <summary>
        /// Delete flat
        /// </summary>
        private void FlatDelete()
        {
            if (MessageBox.Show("Объявление будет удалено из базы. Продолжить", "Удаление...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                NameListCache.proxy.FlatDelete(Convert.ToInt32(grdFlats.CurrentRow.Cells[0].Value));
                this.FlatRefresh();
            }
        }

        /// <summary>
        /// Edit flat
        /// </summary>
        private void FlatEdit()
        {
            if (new frmFlat(EditMode.emEdit, Convert.ToInt32(grdFlats.CurrentRow.Cells[0].Value)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                FlatRefresh();
        }

        /// <summary>
        /// Refresh
        /// </summary>
        private void FlatRefresh()
        {
            this.Cache.CachedData.RemoveAll();
            grdFlats.Refresh();
        }

        /// <summary>
        /// Flat add
        /// </summary>
        private void FlatAdd()
        {
            if (new frmFlat(null, EditMode.emAddNew).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.FlatRefresh();
        }

        /// <summary>
        /// Помеcтить в избранное 
        /// </summary>
        private void StarAdd()
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
        private void BlackListAddPhone()
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
        private void BlackListAdd()
        {
            frmBlackItem frm = new frmBlackItem();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                BlackListRefresh();
                dataGridViewContactList.Rows[dataGridViewContactList.Rows.Count - 2].Selected = true;
            }
        }

        /// <summary>
        /// Редактировать блэк-лист
        /// </summary>
        private void BlackListEdit()
        {
            Int32 blackId;
            if (Int32.TryParse(dataGridViewContactList.CurrentRow.Cells[0].Value.ToString(), out blackId))
            {
                int ri = dataGridViewContactList.SelectedRows[0].Index;
                frmBlackItem frm = new frmBlackItem(blackId);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    BlackListRefresh();
                    dataGridViewContactList.Rows[ri].Selected = true;
                }
            }
        }

        /// <summary>
        /// Get black list from db and fill table
        /// </summary>
        private void BlackListRefresh()
        {
            dataGridViewContactList.DataSource = NameListCache.proxy.BlackListAll();
            dataGridViewContactList.Columns["ID"].Visible = dataGridViewContactList.Columns["TYPE_ID"].Visible = false;
            dataGridViewContactList.Columns["STOP"].Width = dataGridViewContactList.Columns["COMMENT"].Width = 200;
        }

        /// <summary>
        /// Начать загрузку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        /// <summary>
        /// Начать загрузку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        /// <summary>
        /// Завершить загрузку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// Завершить загрузку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
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

        private void BlackListAddClick(object sender, EventArgs e)
        {
            BlackListAdd();
        }

        /// <summary>
        /// Удалить стоп слово
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBlackListClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выражение будет удалено из базы. Продолжить", "Удаление...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Int32 blackId;
            if (Int32.TryParse(dataGridViewContactList.CurrentRow.Cells[0].Value.ToString(), out blackId))
            {
                NameListCache.proxy.BlackListDelete(blackId);
                BlackListRefresh();
            }

        }

        /// <summary>
        /// Удалить с избранного
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlatDeleteClick(object sender, EventArgs e)
        {
            FlatDelete();
        }

        /// <summary>
        /// Delete flat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlatAddClick(object sender, EventArgs e)
        {
            FlatAdd();
        }

        /// <summary>
        /// Редактировать блэк лист
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlackListEditClick(object sender, EventArgs e)
        {
            BlackListEdit();
        }

        private void btnReloadBlackList_Click(object sender, EventArgs e)
        {
            BlackListRefresh();
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

        private void cbSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSites.ComboBox.SelectedValue is int)
            {
                Properties.Settings.Default.cbSites = ((int)cbSites.ComboBox.SelectedValue);
                Properties.Settings.Default.Save();
            }
        }

        private void BlackListAddPhoneClick(object sender, EventArgs e)
        {
            BlackListAddPhone();
        }

        private void StarAddClick(object sender, EventArgs e)
        {
            StarAdd();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (grdFlats.CurrentRow != null)
            {
                var row = grdFlats.CurrentRow;
                inputNAME.Text = row.Cells[Fields.NAME.ToString()].Value.ToString();
                inputCONTENT.Text = row.Cells[Fields.CONTENT.ToString()].Value.ToString();
                var link = row.Cells[Fields.LINK.ToString()].Value.ToString();
                inputLINK.Text = link;
                intupROOM_COUNT.Text = row.Cells[Fields.ROOM_COUNT.ToString()].Value == null ? string.Empty : row.Cells[Fields.ROOM_COUNT.ToString()].Value.ToString();
                intupFLOOR.Text = row.Cells[Fields.FLOOR.ToString()].Value == null ? string.Empty : row.Cells[Fields.FLOOR.ToString()].Value.ToString();
                intupADDRESS.Text = row.Cells[Fields.ADDRESS.ToString()].Value.ToString();
                intupBATH_UNIT.Text = row.Cells[Fields.BATH_UNIT.ToString()].Value.ToString();
                intupBUILD.Text = row.Cells[Fields.BUILD.ToString()].Value.ToString();
                intupSTATE.Text = row.Cells[Fields.STATE.ToString()].Value.ToString();
                intupPRICE.Text = row.Cells[Fields.PRICE.ToString()].Value.ToString();
                inputPHONE.Items.Clear();
                inputPHONE.Items.AddRange(row.Cells[Fields.PHONE.ToString()].Value.ToString().Split(new Char[] { ';' }));

                if (string.IsNullOrEmpty(link))
                    pbIcon.Image = imglistSites.Images[11];
                else if (link.Contains("slando"))
                    pbIcon.Image = imglistSites.Images[9];

                else if (link.Contains("avito"))
                    pbIcon.Image = imglistSites.Images[7];

                else if (link.Contains("irr"))
                    pbIcon.Image = imglistSites.Images[5];

                else if (link.Contains("reklama"))
                    pbIcon.Image = imglistSites.Images[3];

                else if (link.Contains("cmlt"))
                    pbIcon.Image = imglistSites.Images[1];
            }

        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FlatEdit();
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditFlat_Click(object sender, EventArgs e)
        {
            FlatEdit();
        }


        private void inputLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (inputLINK.Text.Trim().Length > 0)
                System.Diagnostics.Process.Start(inputLINK.Text);
        }

        private void FlatRefreshClick(object sender, EventArgs e)
        {
            FlatRefresh();
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

        /// <summary>
        /// Сохранить размеры экрана перед закрытием
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.cbSites = ((int)cbSites.ComboBox.SelectedValue);
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Length = this.Height;
            Properties.Settings.Default.Top = this.Top;
            Properties.Settings.Default.Left = this.Left;
            Properties.Settings.Default.Save();
        }

        private void grdFlats_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                FlatDelete();
        }

    }

}
