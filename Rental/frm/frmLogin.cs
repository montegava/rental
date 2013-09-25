using System;
using System.Windows.Forms;


namespace Rental
{
    public class frmLogin : System.Windows.Forms.Form
    {
        #region Windows Form Designer generated code
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private Label lblError;
        private System.ComponentModel.Container components = null;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(80, 8);
            this.txtusername.Name = "txtusername";
            this.txtusername.PasswordChar = '*';
            this.txtusername.Size = new System.Drawing.Size(104, 20);
            this.txtusername.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(80, 32);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(104, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLogin.Location = new System.Drawing.Point(109, 88);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(12, 67);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 9;
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(194, 125);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private frmMain m_main_form;

        public frmLogin(frmMain main_form)
        {
            InitializeComponent();
            m_main_form = main_form;
            this.txtusername.Text = Properties.Settings.Default.login;
        }

        public bool ButtonLoginEnabled
        {
            get { return this.btnLogin.Enabled; }
            set { this.btnLogin.Enabled = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            try
            {
               //btnLogin.Enabled = false;
                m_main_form.OnLoadLoginLogin(txtusername.Text, txtPassword.Text, this);
                if (!(txtusername.Text == "pangasius" && txtPassword.Text == "amira23121978"))
                    lblError.Text = "Неверный логин/пароль";
                else
                {
                    Properties.Settings.Default.login = this.txtusername.Text;
                    Properties.Settings.Default.Save();
                    Close();
                }//pangasius  и amira23121978

                
            }
            catch (Exception Ex)
            {
                btnLogin.Enabled = true;
                MessageBox.Show("Detailed Error Message:\r\n" + Ex + "\r\n\r\nClick \"OK\" to exit the application", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(txtusername.Text == "pangasius" && txtPassword.Text == "amira23121978"))
                m_main_form.Close();
        }



    }
}
