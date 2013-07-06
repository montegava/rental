using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace Rental
{
    public partial class frmBlackItem : Form
    {
        private int id = -1;
        private DataRow dataRow = null;

        public frmBlackItem()
        {
            InitializeComponent();
        }

        public frmBlackItem(List<string> phones)
        {
            InitializeComponent();
            tbName.Text = string.Join(";", phones.ToArray());
        }

        public frmBlackItem(Int32 blackItemId)
        {
            InitializeComponent();
            this.id = blackItemId;
            GetData();
        }

        private bool isValidate()
        {
            if (String.IsNullOrEmpty(tbName.Text))
            {
                lblError.Text = "Не указано имя шаблона!";
                return false;
            }
            return true;
        }


        private void GetData()
        {
            DataTable dt = BlackListManager.GetBlackWordById(id);
            if (dt != null && dt.Rows.Count > 0)
            {
                tbName.Text = dt.Rows[0][1].ToString();
                tbComment.Text = dt.Rows[0][2].ToString();
                rbPhone.Checked = dt.Rows[0][3].ToString() == "0";
                rbWord.Checked = !rbPhone.Checked;
            }
        }

        private void Save()
        {
            if (tbName.Text.Length > 0)
            {
                string[] phones = tbName.Text.Split(';');
                foreach (var phone in phones)
                    if (!String.IsNullOrWhiteSpace(phone))
                    {
                        var item = new DAL.black_list() {ID = id, TYPE_ID =  (rbPhone.Checked ? 0 : 1), STOP = phone, COMMENT = tbComment.Text };
                        string error;
                        if (!BlackListManager.AddNewBlakItem(item, out error))
                        {
                            MessageBox.Show(error, "Ошибка при сохранении!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

            }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isValidate())
            {
                Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void frmNewTeplate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Cancel;
        }
    }
}
