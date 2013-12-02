using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using System.IO;
using System.Linq.Expressions;
using System.Configuration;
using log4net;
using Rental.src;

namespace Rental
{
    public partial class frmFlat : Form
    {
        private static ILog errorLog = LogManager.GetLogger("ErrorLogger");
        private EditMode EdtMode;
        public int FlatId = -1;
        public string RepositoryDirectory = @"Media";

        public frmFlat(Advert advert, EditMode edtype)
        {
            InitializeComponent();
            EdtMode = edtype;
            frmFlat_Load();
            if (advert != null)
            {
                inputCONTENT.Text = advert.Content;
                inputLINK.Text = advert.Link;
                if (advert.Phones != null)
                    foreach (string contact in advert.Phones)
                        inputPHONE.Items.Add(String.Format("{0:##-####-####}", contact));
            }
        }

        public frmFlat(EditMode edtype, int flatId)
        {
            InitializeComponent();
            EdtMode = edtype;
            this.FlatId = flatId;
            frmFlat_Load();
            LoadFlatInfo();
            LoadFlatImages();
           
        }

        private void LoadFlatInfo()
        {
            var flat = NameListCache.proxy.FlatById(FlatId);
            if (flat != null)
            {
                this.intupADDRESS.Text = flat.ADDRESS;
                this.intupBATH_UNIT.SelectedValue = flat.bathunit_type_id;
                this.intupBUILD.SelectedValue = flat.buld_type_id;
                this.intupCOMMENT.Text = flat.COMMENT;
                this.inputCONTENT.Text = flat.CONTENT;
                this.chCooler.Checked = flat.COOLER;
                this.Text = "Информация о квартите № " + flat.ID + " от " + flat.DATA.ToString();
                this.intupFLOOR.Text = flat.FLOOR.ToString();
                this.chFridge.Checked = flat.FRIDGE;
                this.intupFURNITURE.Text = flat.FURNITURE;
                this.inputLESSOR.SelectedValue = flat.lessor_type_id;
                this.inputLINK.Text = flat.LINK;
                this.inputNAME.Text = flat.NAME;
                this.inputPHONE.Items.AddRange(flat.PHONE.Split(new Char[] { ';' }));
                this.intupPRICE.Text = flat.PRICE;
                this.inputREGION.SelectedValue = flat.region_type_id;
                this.inputRENT_FROM.Text = flat.RENT_FROM.ToString();
                this.inputRENT_TO.Text = flat.RENT_TO.ToString();
                this.intupROOM_COUNT.Text = flat.ROOM_COUNT.ToString();
                this.intupSTATE.SelectedValue = flat.state_type_id;
                this.inputTERM.SelectedValue = flat.term_type_id;
                this.chTV.Checked = flat.TV;
                this.inputEmail.Text = flat.EMAIL;

                this.inputCategory.SelectedValue = flat.category_type_id;

                this.inputType.SelectedValue = flat.rent_type_id;

                this.inputPayment.SelectedValue = flat.payment_type_id;


            }
        }

        private void frmFlat_Load()
        {
            intupBUILD.SelectedIndex = 0;
            intupFLOOR.SelectedIndex = 0;

            intupROOM_COUNT.SelectedIndex = 0;

            intupBATH_UNIT.SelectedIndex = 0;

            intupSTATE.SelectedIndex = 0;
            intupFURNITURE.SelectedIndex = 0;
            intupMECHANIC.SelectedIndex = 0;

            inputTERM.SelectedIndex = 0;
            inputLESSOR.SelectedIndex = 0;

            inputREGION.SelectedIndex = 0;

            inputType.SelectedIndex = 0;

            inputCategory.SelectedIndex = 0;

            inputPayment.SelectedIndex = 0;

            if (EdtMode == EditMode.emAddNew)
            {


                btnOk.Text = "Добавить";

                this.Text = "ДОБАВИТЬ В ИЗБРАННОЕ";

            }
            else
            {
                this.Text = "РЕДАКТИРОВАТЬ";
                btnOk.Text = "Сохранить";
            }
        }

        private void Save()
        {
            flat_info flat = Form2Flat();

            if (EdtMode == EditMode.emAddNew)
                this.FlatId = NameListCache.proxy.FlatAdd(flat);
            else if (EdtMode == EditMode.emEdit)
                NameListCache.proxy.FlatUpdate(flat);

            var imageList = new List<image_list>();
            foreach (ListViewItem item in lvImagList.Items)
            {
                var image = (image_list)item.Tag;
                if (image.ID <= 0)
                    NameListCache.proxy.FileUpload(image.IMAGE_PATH, System.IO.File.ReadAllBytes(RepositoryDirectory + "//" + image.IMAGE_PATH));
                image.FLAT_ID = this.FlatId;
                imageList.Add(image);
            }
            NameListCache.proxy.ImageUpdate(imageList.ToArray(), this.FlatId);
        }



        /// <summary>
        /// Convert form to Flat type
        /// </summary>
        /// <returns></returns>
        private flat_info Form2Flat()
        {
            var result = new DAL.flat_info();


            result.ID = FlatId;
            result.DATA = DateTime.Now;

            int roomCount;
            result.ROOM_COUNT = int.TryParse(intupROOM_COUNT.Text, out roomCount) ? (int?)roomCount : null;

            result.ADDRESS = intupADDRESS.Text;

            int floor;
            result.FLOOR = int.TryParse(intupFLOOR.Text, out floor) ? (int?)floor : null;

            result.bathunit_type_id = (int?)intupBATH_UNIT.SelectedValue;
            result.buld_type_id = (int?)intupBUILD.SelectedValue;

            result.FURNITURE = intupFURNITURE.Text;
            result.state_type_id = (int?)intupSTATE.SelectedValue;
            result.NAME = inputNAME.Text;
            result.PRICE = intupPRICE.Text;

            result.PHONE = String.Join(";", inputPHONE.Items.Cast<string>().ToArray());
            result.CONTENT = inputCONTENT.Text;
            result.COMMENT = intupCOMMENT.Text;
            result.LINK = inputLINK.Text;
            result.term_type_id = (int?)inputTERM.SelectedValue;
            result.RENT_FROM = inputRENT_FROM.Value.Date;
            result.RENT_TO = inputRENT_TO.Value.Date;
            result.lessor_type_id = (int?)inputLESSOR.SelectedValue;

            result.FRIDGE = chFridge.Checked;
            result.TV = chTV.Checked;
            result.WASHER = chWasher.Checked;
            result.COOLER = chCooler.Checked;

            result.region_type_id = (int?)inputREGION.SelectedValue;

            result.EMAIL = inputEmail.Text;
            result.category_type_id = (int?)inputCategory.SelectedValue;
            result.rent_type_id = (int?)inputType.SelectedValue;
            result.payment_type_id = (int?)inputPayment.SelectedValue;


            return result;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Save();
            DialogResult = DialogResult.OK;
        }

        private void inputLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (inputLINK.Text.Trim().Length > 0)
                System.Diagnostics.Process.Start(inputLINK.Text);
        }

        private void contextAdd_Click(object sender, EventArgs e)
        {
            string phone = "";
            if (Extension.InputBox("Новый телефон", "Введите новый телефон:", ref phone) == DialogResult.OK)
                if (!String.IsNullOrWhiteSpace(phone))
                    inputPHONE.Items.Add(phone);
        }

        private void contextDel_Click(object sender, EventArgs e)
        {
            if (inputPHONE.SelectedItem != null)
                inputPHONE.Items.Remove(inputPHONE.SelectedItem);
        }

        private void intupMECHANIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            chFridge.Enabled = chTV.Enabled = chWasher.Enabled = chCooler.Enabled = intupMECHANIC.SelectedIndex == 2;
        }

        private void LoadFlatImages()
        {
            if (FlatId > 0)
            {
                var images = NameListCache.proxy.ImagesByFlatId(this.FlatId);
                if (images.Any())
                {
                    foreach (image_list image in images)
                        lvImagList.Items.Add(new ListViewItem()
                                                {
                                                    Tag = image,
                                                    Text = string.Format("image №{0}", lvImagList.Items.Count.ToString()),
                                                });
                }
            }
        }

        private string GetFilePath(string path)
        {
            string destFolder = ConfigurationManager.AppSettings["ServerDir"];
            return destFolder + "\\" + Path.GetFileName(path);
        }

        private void btnAddNewImage_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var compressedImagePath = Common.SaveJpeg(Image.FromFile(dialog.FileName), 50);
                var selItem = lvImagList.Items.Add(new ListViewItem()
                                                    {
                                                        Text = string.Format("image №{0}", lvImagList.Items.Count.ToString()),
                                                        Tag = new image_list()
                                                        {
                                                            ID = -1,
                                                            IMAGE_PATH = compressedImagePath,
                                                            FLAT_ID = this.FlatId
                                                        }
                                                    });
            }
        }

        private void lvImagList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvImagList.SelectedItems.Count > 0)
            {
                var fileName = ((image_list)lvImagList.SelectedItems[0].Tag).IMAGE_PATH;

                var filePath = RepositoryDirectory + "//" + fileName;
                if (File.Exists(filePath))
                {
                    using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pbImage.Image = System.Drawing.Image.FromStream(fs);
                    }
                }
                else
                {
                    File.WriteAllBytes(filePath, NameListCache.proxy.FileDownload(((image_list)lvImagList.SelectedItems[0].Tag).IMAGE_PATH));

                    using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pbImage.Image = System.Drawing.Image.FromStream(fs);
                    }
                }
            }
        }

        private void pbImage_DoubleClick(object sender, EventArgs e)
        {
            if (lvImagList.SelectedItems.Count > 0)
            {
                string filePath = Path.GetFullPath(RepositoryDirectory + "//" + ((image_list)lvImagList.SelectedItems[0].Tag).IMAGE_PATH);
                if (File.Exists(filePath))
                    System.Diagnostics.Process.Start(filePath);
            }

        }

        private void frmFlat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnDeleteImg_Click(object sender, EventArgs e)
        {
            if (lvImagList.SelectedItems.Count > 0)
            {
                lvImagList.Items.Remove(lvImagList.SelectedItems[0]);
                lvImagList.Refresh();
            }
        }
    }
}
