using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using Rental.src;

namespace Rental
{
    public partial class frmBlackItem : Form
    {
        private int id = -1;

        public frmBlackItem()
        {
            InitializeComponent();
        }

        public frmBlackItem(List<string> phones)
            : this()
        {
            tbName.Text = string.Join(";", phones.ToArray());
        }

        public frmBlackItem(Int32 id)
            : this()
        {
            this.id = id;
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
            var bl = NameListCache.proxy.BlackListById(id);
            if (bl != null)
            {
                tbName.Text = bl.STOP;
                tbComment.Text = bl.COMMENT;
                rbPhone.Checked = bl.TYPE_ID == 0;
                rbWord.Checked = !rbPhone.Checked;
                this.Text = "Редактировать стоп-слово";
                btnOk.Text = "Сохранить";
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
                        var item = new DAL.black_list() { ID = id, TYPE_ID = (rbPhone.Checked ? 0 : 1), STOP = phone, COMMENT = tbComment.Text };
                        NameListCache.proxy.BlackListAdd(item);
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
