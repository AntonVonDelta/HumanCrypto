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
using System.Text.RegularExpressions;
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
            genomeProcessing = new GenomeProcessing(new byte[] { 0, 255, 0, 0 });

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
            XDocument doc = XDocument.Load("HumanParts\\data.xml");
            Queue<Point> attachmentPoints = new Queue<Point>();

            attachmentPoints.Enqueue(new Point(250, 250));

            // Process face parts in order
            foreach (XElement el in doc.Root.Element("order").Elements()) {
                Point nextAttachPoint = attachmentPoints.Dequeue();
                int partId = genomeProcessing.GetNextPartId();

                XElement partType = (from partEl in doc.Root.Elements("parts").Elements(el.Name)
                                     where (int)partEl.Attribute("id") == partId
                                     select partEl).First();

                Size figureCenterPoint = new Size((int)partType.Attribute("centerx"), (int)partType.Attribute("centery"));
                Size offsetPoint = (Size)(nextAttachPoint - figureCenterPoint);

                foreach (XElement svgEl in partType.Elements("svg")) {
                    GraphicsPath path = GetSvgPath((string)svgEl.Attribute("data"), offsetPoint);

                    g.FillPath(Brushes.Blue, path);
                }


                foreach (XElement svgEl in partType.Elements("point")) {
                    // Here the next attachment point is actually "bounded" to the center of the figure
                    // So it should move as the center moves
                    Point newAttachPoint = new Point((int)partType.Attribute("c"), (int)partType.Attribute("y"));
                    attachmentPoints.Enqueue(newAttachPoint + offsetPoint);
                }
            }
        }

        /// <summary>
        /// Adds the offset point to all points in path
        /// </summary>
        /// <param name="svg"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private GraphicsPath GetSvgPath(string svg, Size offset) {
            List<Point> points = new List<Point>();
            Regex matchNumberPairs = new Regex("([a-zA-Z])(-*\\d+),(-*\\d+)", RegexOptions.ECMAScript);
            Match m = matchNumberPairs.Match(svg);

            Point absolutePosition = new Point(0, 0);
            while (m.Success) {
                Point interprettedPoint = new Point(Convert.ToInt32(m.Groups[2].Value), Convert.ToInt32(m.Groups[3].Value));
                if (Char.IsLower(m.Groups[1].Value[0])) {
                    interprettedPoint.X += absolutePosition.X;
                    interprettedPoint.Y += absolutePosition.Y;

                    absolutePosition = interprettedPoint;
                } else {
                    absolutePosition = interprettedPoint;
                }

                points.Add(interprettedPoint + offset);
                m = m.NextMatch();
            }

            GraphicsPath result = new GraphicsPath();
            result.AddPolygon(points.ToArray());
            return result;
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
            Properties.Secret.Default.ChainId = Convert.ToInt32(networkChainTxt.Text);

            Properties.Secret.Default.Save();
            saveSettingsBtn.Enabled = false;
        }



        #endregion

    }

}
