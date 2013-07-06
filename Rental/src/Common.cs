using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Rental
{
    class Common
    {
        public static double DoubleCut(double val)
        {
            double valr = Math.Round(val, 2);
            if (valr > val)
                valr += -0.01;
            return valr;
        }

        public static string GetMd5Hash(string input)
        {
            if (input != null)
            {

                // Create a new instance of the MD5CryptoServiceProvider object.
                MD5 md5Hasher = MD5.Create();

                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
            return null;
        }

        public static void DoubleAllow(object sender, KeyPressEventArgs e)
        {
            double res;
            if (char.IsLetter(e.KeyChar) ||
     char.IsSymbol(e.KeyChar) ||
     char.IsWhiteSpace(e.KeyChar) || (e.KeyChar != '\b' &&
               !Double.TryParse((sender as TextBox).Text + e.KeyChar, out res)))
                e.Handled = true;
        }

        public static void DigitalAllow(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
       char.IsSymbol(e.KeyChar) ||
       char.IsWhiteSpace(e.KeyChar) ||
       char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        public static void SetAutoComplete(ComboBox combo)
        {
            if (combo != null)
            {
                combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combo.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }

        public static void SetAutoComplete(TextBox combo)
        {
            if (combo != null)
            {
                combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        public static void FillComboBox(ComboBox combo, IList dataSource, string displayMember, string valueMember, bool addEmty = false)
        {
            if (combo != null)
            {
                if (dataSource != null && dataSource.Count > 0)
                {


                    if (addEmty)
                    {
                        Type t = dataSource[0].GetType();
                        var obj = Activator.CreateInstance(t);
                        dataSource.Insert(0, obj);

                    }
                    combo.DataSource = dataSource;
                    combo.DisplayMember = displayMember;
                    combo.ValueMember = valueMember;
                }
            }
        }

   
        public static void SetGridStyle(DataGridView grid)
        {
            if (grid != null)
            {
                grid.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold, GraphicsUnit.Point);
                grid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
                grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.DefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Point);
                grid.DefaultCellStyle.BackColor = Color.Empty;
                grid.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.ControlLight;
                grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                grid.GridColor = SystemColors.ControlDarkDark;
            }
        }

        public static void SetColumlOption(DataGridView grid, string columnName, string headerText, int width, ref int displayIndex, bool visible = true)
        {
            if (grid != null && grid.Columns[columnName] != null)
            {
                grid.Columns[columnName].Visible = visible;
                grid.Columns[columnName].DisplayIndex = displayIndex;
                grid.Columns[columnName].Width = width;
                grid.Columns[columnName].HeaderText = headerText;
                displayIndex++;
            }
        }
    }
}
