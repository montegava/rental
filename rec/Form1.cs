using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace rec
{
    public partial class Form1 : Form
    {
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
    }
}
