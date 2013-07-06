using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using System.Windows.Forms;

namespace Rental
{
    static class Extension
    {

        public static void GetPhones(this Advert adv, string content)
        {
            Regex r = new Regex(@"<div[\s]class=""contact"">.*?>([\s\d-)(+]*?)</div>", RegexOptions.Singleline);
            if (adv.Phones != null)
                adv.Phones.Clear();
            else
            {
                adv.Phones = new List<string>();
            }


            Match m = r.Match(content);

            while (m.Success && m.Groups.Count > 1)
            {
                string phone = m.Groups[1].ToString().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim();
                if (phone.Length > 2)
                    adv.Phones.Add(phone);

                m = m.NextMatch();
            }

        }

        public static string ClearPhone(this string phone)
        {
            return phone.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "").Trim();
        }


        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
