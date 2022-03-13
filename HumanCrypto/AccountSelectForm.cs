using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanCrypto {
    public partial class AccountSelectForm : Form {
        public AccountSelectForm() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Hide();
            Wallet wallet = new Wallet(0);
            Form1 form = new Form1(wallet);

            form.FormClosed += (a, b) => Close();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            Hide();
            Wallet wallet = new Wallet(1);
            Form1 form = new Form1(wallet);

            form.FormClosed += (a, b) => Close();
            form.Show();
        }
    }
}
