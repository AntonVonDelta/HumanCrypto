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

        struct PartInfo {
            public string partName;
            public Point position;
            public PartInfo(string partName, Point position) {
                this.partName = partName;
                this.position = position;
            }
        }


        public Form1() {
            InitializeComponent();

            web3 = new Web3("https://kovan.infura.io/v3/9aa3d95b3bc440fa88ea12eaa4456161");
            InitWalletAccount();

            // Init genome
            genomeProcessing = new GenomeProcessing(new byte[] { 4, 4, 4, 4, 4, 4, 4, 4 });

            // Add event to all settings-bound controls
            List<Control> settingsBoundedControls = new List<Control>() { apiKeyTxt, privateKeyTxt, networkChainTxt };
            foreach (Control control in settingsBoundedControls) {
                control.TextChanged += genericControl_TextChanged;
            }
        }

        private async void Form1_Load(object sender, EventArgs e) {
            //var latestBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            //Console.WriteLine($"Latest Block Number is: {latestBlockNumber}");
        }


        private void InitWalletAccount() {
            web3Account = new Account(Properties.Secret.Default.PrivateKey, Properties.Secret.Default.ChainId);
        }


        private void panel1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            XDocument doc = XDocument.Load("HumanParts\\data.xml");
            Queue<PartInfo> attachmentPoints = new Queue<PartInfo>();

            genomeProcessing.Reset();


            // Process face parts in order
            foreach (XElement el in doc.Root.Element("order").Elements()) {
                attachmentPoints.Enqueue(new PartInfo(el.Name.ToString(), new Point((int)el.Attribute("x"), (int)el.Attribute("y"))));

                while (attachmentPoints.Count != 0) {
                    PartInfo nextAttachPart = attachmentPoints.Dequeue();
                    int partId = genomeProcessing.GetNextPartId() % doc.Root.Elements("parts").Elements(nextAttachPart.partName).Count();

                    // Skip body part because it was not defined in xml
                    if (doc.Root.Elements("parts").Elements(nextAttachPart.partName).Count() == 0) {
                        throw new Exception($"Referenced body part not found {nextAttachPart.partName}");
                    }

                    // Get part of the body corresponding to current element and id
                    XElement partType = (from partEl in doc.Root.Elements("parts").Elements(nextAttachPart.partName)
                                         where (int)partEl.Attribute("id") == partId
                                         select partEl).FirstOrDefault();


                    Size figureCenterPoint = new Size((int)partType.Attribute("centerx"), (int)partType.Attribute("centery"));
                    Size offsetPoint = (Size)(nextAttachPart.position - figureCenterPoint);

                    foreach (XElement svgEl in partType.Elements("svg")) {
                        GraphicsPath path = GetSvgPath((string)svgEl.Attribute("data"), offsetPoint);
                        Color partColor = genomeProcessing.GetColor();

                        g.FillPath(new SolidBrush(partColor), path);
                        g.DrawPath(Pens.Black, path);
                    }


                    foreach (XElement partEl in partType.Elements("components").Elements()) {
                        // Here the next attachment point is actually "bounded" to the center of the figure
                        // So it should move as the center moves
                        Point newAttachPoint = new Point((int)partEl.Attribute("x"), (int)partEl.Attribute("y"));
                        attachmentPoints.Enqueue(new PartInfo(partEl.Name.ToString() ,newAttachPoint + offsetPoint));
                    }

                    genomeProcessing.AdvanceGene();
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
            GraphicsPath result = null;
            List<Regex> matchCommands = new List<Regex>();

            matchCommands.Add(new Regex("\\G([mMlL]{1})(-*\\d+)(?:\\.\\d+)*,(-*\\d+)(?:\\.\\d+)*", RegexOptions.ECMAScript));
            matchCommands.Add(new Regex("\\G([cC]{1})(-*\\d+)(?:\\.\\d+)*,(-*\\d+)(?:\\.\\d+)*\\s+(-*\\d+)(?:\\.\\d+)*,(-*\\d+)(?:\\.\\d+)*\\s+(-*\\d+)(?:\\.\\d+)*,(-*\\d+)(?:\\.\\d+)*", RegexOptions.ECMAScript));

            Point prevPoint = new Point(0, 0);
            int startingMatchPosition = 0;
            int i = 0;
            while (i < matchCommands.Count) {
                Match m = matchCommands[i].Match(svg, startingMatchPosition);

                // Reset the index of the command-regex to be tried
                if (!m.Success) {
                    i++;
                    continue;
                }

                Point lastPoint = new Point(0, 0);
                string drawCommand = m.Groups[1].Value.ToLower();

                if ("ml".Contains(drawCommand)) {
                    lastPoint = new Point(Convert.ToInt32(m.Groups[2].Value), Convert.ToInt32(m.Groups[3].Value));

                    if (result == null) {
                        result = new GraphicsPath();
                    } else {
                        if (Char.IsLower(m.Groups[1].Value[0])) {
                            lastPoint.X += prevPoint.X;
                            lastPoint.Y += prevPoint.Y;
                        }

                        result.AddLine(prevPoint + offset, lastPoint + offset);
                    }
                } else if ("c".Contains(drawCommand)) {
                    Point controlPoint1 = new Point(Convert.ToInt32(m.Groups[2].Value), Convert.ToInt32(m.Groups[3].Value));
                    Point controlPoint2 = new Point(Convert.ToInt32(m.Groups[4].Value), Convert.ToInt32(m.Groups[5].Value));
                    lastPoint = new Point(Convert.ToInt32(m.Groups[6].Value), Convert.ToInt32(m.Groups[7].Value));

                    if (Char.IsLower(m.Groups[1].Value[0])) {
                        controlPoint1.X += prevPoint.X;
                        controlPoint1.Y += prevPoint.Y;

                        controlPoint2.X += prevPoint.X;
                        controlPoint2.Y += prevPoint.Y;

                        lastPoint.X += prevPoint.X;
                        lastPoint.Y += prevPoint.Y;
                    }

                    result.AddBezier(prevPoint + offset, controlPoint1 + offset, controlPoint2 + offset, lastPoint + offset);
                } else {
                    throw new Exception("Unrecognized draw command");
                }

                // Store the previous point
                prevPoint = lastPoint;

                startingMatchPosition = m.Index + m.Length;
            }

            result.CloseFigure();
            return result;
        }


        private void button1_Click(object sender, EventArgs e) {
            genomeProcessing.Randomize();
            pictureBox1.Invalidate();
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
