using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Ninject;


namespace rec
{
    public partial class Form1 : Form
    {
        public RentalApi.IRentalApi coreClient { get { return new RentalApi.RentalApiClient(); } }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ImageProcessing.Log.Append("init");
                var dd = ImageProcessing.ImageRecognition.RecognizeImage(Image.FromFile("download.png"), true);
                listBox1.Items.Add(dd);
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
                ImageProcessing.Log.Append(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int activePage1 = 1;
            int activePage;
            int totalRowsNumber;
            int pageCount;
            try
            {
                var dd = coreClient.GetFlatList(-1, false, ref activePage1, out pageCount, out totalRowsNumber, 100);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
        }
    }
}
