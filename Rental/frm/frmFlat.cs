﻿using System;
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
      
        public frmFlat(Advert advert, EditMode edtype)
        {
            InitializeComponent();
            EdtMode = edtype;
            if (advert != null)
            {
                if (advert.Content != null)
                    inputCONTENT.Text = advert.Content;

                if (advert.Link != null)
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
            LoadFlatImages();
            LoadFlatInfo();
        }

        private void LoadFlatInfo()
        {
            var flat = NameListCache.proxy.FlatById(FlatId);
            if (flat != null)
            {
                this.intupADDRESS.Text = flat.ADDRESS;
                this.intupBATH_UNIT.Text = flat.BATH_UNIT;
                this.intupBUILD.Text = flat.BUILD;
                this.intupCOMMENT.Text = flat.COMMENT;
                this.inputCONTENT.Text = flat.CONTENT;
                this.chCooler.Checked = flat.COOLER ?? false;
                this.Text = "Информация о квартите № " + flat.ID + " от " + flat.DATA.ToString();
                this.intupFLOOR.Text = flat.FLOOR.ToString();
                this.chFridge.Checked = flat.FRIDGE ?? false;
                this.intupFURNITURE.Text = flat.FURNITURE;
                this.inputLESSOR.Text = flat.LESSOR;
                this.inputLINK.Text = flat.LINK;
                this.intupMECHANIC.Text = flat.MECHANIC;
                this.inputNAME.Text = flat.NAME;
                this.inputPHONE.Items.AddRange(flat.PHONE.Split(new Char[] { ';' }));
                this.intupPRICE.Text = flat.PRICE;
                this.inputREGION.Text = flat.REGION;
                this.inputRENT_FROM.Text = flat.RENT_FROM.ToString();
                this.inputRENT_TO.Text = flat.RENT_TO.ToString();
                this.intupROOM_COUNT.Text = flat.ROOM_COUNT.ToString();
                this.intupSTATE.Text = flat.STATE;
                this.inputTERM.Text = flat.TERM;
                this.chTV.Checked = flat.TV ?? false;
                this.inputEmail.Text = flat.EMAIL;
                this.inputCategory.Text = flat.CATEGORY;
                this.inputType.Text = flat.TYPE;
            }
        }



        private void frmFlat_Load(object sender, EventArgs e)
        {
            if (EdtMode == EditMode.emAddNew)
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

            }
        }

        private void Save()
        {
            flat_info flat = Form2Flat();
            if (EdtMode == EditMode.emAddNew)
            {
                NameListCache.proxy.FlatAdd(flat);
                this.FlatId = flat.ID;
            }
            else if (EdtMode == EditMode.emEdit)
                NameListCache.proxy.FlatUpdate(flat);

            foreach (ListViewItem item in lvImagList.Items)
            {
                var flatImage = ((DAL.images)item.Tag);
                var imgPath = flatImage.IMAGE_PATH;

                FlatImageManager.AddFlatImage(flat.ID, imgPath);
            }
        }

        /// <summary>
        /// Convert form to Flat type
        /// </summary>
        /// <returns></returns>
        private DAL.flat_info Form2Flat()
        {
            var result = new DAL.flat_info();

            result.ID = FlatId;
            result.DATA = DateTime.Now;

            int roomCount;
            result.ROOM_COUNT = int.TryParse(intupROOM_COUNT.Text, out roomCount)? (int?)roomCount : null;

            result.ADDRESS = intupADDRESS.Text;

            int floor;
            result.FLOOR = int.TryParse(intupFLOOR.Text, out floor) ? (int?)floor : null;

            result.BATH_UNIT = intupBATH_UNIT.Text;
            result.BUILD = intupBUILD.Text;

            result.FURNITURE = intupFURNITURE.Text;
            result.STATE = intupSTATE.Text;
            result.MECHANIC = intupMECHANIC.Text;
            result.NAME = inputNAME.Text;
            result.PRICE = intupPRICE.Text;

            result.PHONE = inputPHONE == null || inputPHONE.Items.Count == 0 ? String.Empty : String.Join(";", inputPHONE.Items.Cast<string>().ToArray());
            result.CONTENT = inputCONTENT.Text;
            result.COMMENT = intupCOMMENT.Text;
            result.LINK = inputLINK.Text;
            result.TERM = inputTERM.Text;
            result.RENT_FROM = inputRENT_FROM.Value.Date;
            result.RENT_TO = inputRENT_TO.Value.Date;
            result.LESSOR = inputLESSOR.Text;


            result.FRIDGE = chFridge.Checked;
            result.TV = chTV.Checked;
            result.WASHER = chWasher.Checked;
            result.COOLER = chCooler.Checked;

            result.REGION = inputREGION.Text;


            return result;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Save();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void inputLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (inputLINK.Text.Trim().Length > 0)
                System.Diagnostics.Process.Start(inputLINK.Text);
        }

        private void contextAdd_Click(object sender, EventArgs e)
        {
            string phone = "";
            if (Extension.InputBox("New phone", "Enter new phone:", ref phone) == DialogResult.OK)
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
                var images = FlatImageManager.GetFlatImagesByFlatId(FlatId);
                if (images.Count > 0)
                {
                    foreach (var flat in images)
                    {
                        var strings = flat.IMAGE_PATH.Split('\\');
                        flat.IMAGE_PATH = strings[strings.Length - 1].Replace("DatabaseImageStore", "");

                        var item = new ListViewItem()
                        {
                            Tag = flat,
                            Text = Path.GetFileNameWithoutExtension(flat.IMAGE_PATH)
                        };
                        lvImagList.Items.Add(item).Selected = true;
                    }

                    string imageFileName = GetFilePath(((DAL.images)lvImagList.Items[0].Tag).IMAGE_PATH);
                    if (File.Exists(imageFileName))
                        pbImage.Image = Image.FromFile(imageFileName);
                    else
                        pbImage.Image = null;
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
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var compressedImage = Common.SaveJpeg(Image.FromFile(dialog.FileName), 50);

                var proxy = new RentalCore.RentalCoreClient();
                var imgPath = proxy.Upload(new FileStream(compressedImage, FileMode.Open));
                proxy.Close();

                var fi = new DAL.images() { ID = -1, FLAT_ID = FlatId, IMAGE_PATH = imgPath };


                var item = new ListViewItem();
                item.Tag = fi;
                item.Text = Path.GetFileNameWithoutExtension(compressedImage);

                var selItem = lvImagList.Items.Add(item);

                //pbImage.Image = Image.FromFile(compressedImage);

                //if (EdtMode == EditMode.emEdit)
                //    FlatImageManager.AddFlatImage(FlatId, destImagePath);

            }
        }

        private void lvImagList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvImagList.SelectedItems.Count > 0)
            {
                string fileName = ((DAL.images)lvImagList.SelectedItems[0].Tag).IMAGE_PATH;
                if (File.Exists(fileName))
                {

                    pbImage.Image = Image.FromFile(fileName);
                }
                else
                {
                    DownloadFile(fileName);
                    if (File.Exists(fileName))
                        pbImage.Image = Image.FromFile(fileName);
                    else
                        pbImage.Image = null;
                }
            }
        }

        private void DownloadFile(string filePath)
        {
            var proxy = new RentalCore.RentalCoreClient();


            var data = proxy.DownloadFile(filePath);

            FileStream fs = null;
            try
            {
                fs = File.Create(filePath);
                byte[] buffer = new byte[1024];
                int read = 0;
                while ((read = data.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fs.Write(buffer, 0, read);
                }
            }
            catch { }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

                if (data != null)
                {
                    data.Close();
                    data.Dispose();
                }
            }

            proxy.Close();
        }

        private void pbImage_DoubleClick(object sender, EventArgs e)
        {
            if (lvImagList.SelectedItems.Count > 0)
            {
                string fileName = ConfigurationManager.AppSettings["ServerDir"] + "\\" + ((DAL.images)lvImagList.SelectedItems[0].Tag).IMAGE_PATH;
                if (File.Exists(fileName))
                    System.Diagnostics.Process.Start(fileName);
            }

        }

        private void frmFlat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
