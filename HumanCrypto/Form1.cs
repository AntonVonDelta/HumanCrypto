using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HumanCrypto {
    public partial class Form1 : Form {
        Web3 web3;
        Account web3Account;
        GenomeProcessing genomeProcessing;

        public Form1() {
            InitializeComponent();

            web3 = new Web3("https://kovan.infura.io/v3/9aa3d95b3bc440fa88ea12eaa4456161");
            InitWalletAccount();

            // Init genome
            genomeProcessing = new GenomeProcessing(new byte[] {0,255,0,0 });

            // Add event to all settings-bound controls
            List<Control> settingsBoundedControls = new List<Control>() { apiKeyTxt, privateKeyTxt, networkChainTxt };
            foreach (Control control in settingsBoundedControls) {
                control.TextChanged += genericControl_TextChanged;
            }
        }

        private async void Form1_Load(object sender, EventArgs e) {
            var latestBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            Console.WriteLine($"Latest Block Number is: {latestBlockNumber}");


            
            
        }


        private void InitWalletAccount() {
            web3Account = new Account(Properties.Secret.Default.PrivateKey, Properties.Secret.Default.ChainId);
        }


        private void panel1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;

            var doc = XDocument.Load("HumanParts\\data.xml");

            foreach(XElement el in doc.Root.Element("order").Elements()) {
                int partId = genomeProcessing.GetNextPartId();

                var partType = (from partEl in doc.Root.Elements("parts").Elements(el.Name)
                                where (string)partEl.Attribute("id") == partId.ToString()
                                select partEl).First();
                Console.WriteLine(partType.Name);



            }


            var path = new GraphicsPath();
            path.AddLine(100, 100, 400, 100);
            path.AddLine(400, 100, 400, 400);
            path.AddLine(400, 400, 100, 400);
            path.CloseFigure();
            Region reg1 = new Region(path);

            var path2 = new GraphicsPath();
            int centerx = 0;
            int centery = 0;
            path2.AddLine(centerx+150, centery+150, centerx+350, centery+150);
            path2.AddLine(centerx+350, centery+150, centerx+350, centery+350);
            path2.AddLine(centerx+350, centery+350, centerx+150, centery+350);
            path2.CloseFigure();
            Region reg2 = new Region(path2);

            g.FillPath(Brushes.Blue, path);
            g.FillRegion(Brushes.Red, reg2);
        }

        private GraphicsPath[] GetBitmapContours(string bmpfilename) {
            Bitmap bmp = new Bitmap(bmpfilename);
            Queue<Point> unvisitedPoints=new Queue<Point>();
            List<Point> visitedPoints = new List<Point>();

            bool found = false;
            for(int i = 0; i < bmp.Height && !found; i++) {
                for(int j = 0; j < bmp.Width && !found; j++) {
                    Color pixelColor=bmp.GetPixel(j, i);
                    if (pixelColor.ToArgb() != 0) {
                        Console.WriteLine($"Pixel value {pixelColor.ToArgb()}");
                        found = true;
                    }
                }
            }

            return null;
        }


        #region SettingsTab
        // Variable which signals that controls are updated and so should not raise the Changed events or
        // ignore their effect
        private bool updatingControls = false;

        private void tabSettings_Enter(object sender, EventArgs e) {
            saveSettingsBtn.Enabled = false;


            updatingControls = true;
            apiKeyTxt.Text = Properties.Secret.Default.APIKey;
            privateKeyTxt.Text = Properties.Secret.Default.PrivateKey;
            networkChainTxt.Text = Properties.Secret.Default.ChainId.ToString();
            updatingControls = false;
        }

        private void genericControl_TextChanged(object sender, EventArgs e) {
            if (updatingControls) return;
            saveSettingsBtn.Enabled = true;
        }

        private void saveSettingsBtn_Click(object sender, EventArgs e) {
            Properties.Secret.Default.APIKey = apiKeyTxt.Text;
            Properties.Secret.Default.PrivateKey = privateKeyTxt.Text;
            Properties.Secret.Default.ChainId =Convert.ToInt32(networkChainTxt.Text);

            Properties.Secret.Default.Save();
            saveSettingsBtn.Enabled = false;
        }



        #endregion

    }

}
