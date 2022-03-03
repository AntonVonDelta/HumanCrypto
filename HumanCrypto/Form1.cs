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

            var doc = XDocument.Load("HumanParts\\data.xml");

            foreach (XElement el in doc.Root.Element("order").Elements()) {
                int partId = genomeProcessing.GetNextPartId();

                var partType = (from partEl in doc.Root.Elements("parts").Elements(el.Name)
                                where (string)partEl.Attribute("id") == partId.ToString()
                                select partEl).First();
                Console.WriteLine(partType.Name);

                foreach (XElement svgEl in partType.Elements("svg")) {
                    GraphicsPath path = GetSvgPath((string)svgEl.Attribute("data"));

                    g.FillPath(Brushes.Blue, path);

                }


            }
        }

        private GraphicsPath GetSvgPath(string svg) {
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

                points.Add(interprettedPoint);
                m = m.NextMatch();
            }

            GraphicsPath result = new GraphicsPath();
            result.AddPolygon(points.ToArray());
            return result;
        }

        private List<GraphicsPath> GetBitmapContours(string bmpfilename) {
            List<GraphicsPath> result = new List<GraphicsPath>();
            Bitmap bmp = new Bitmap(bmpfilename);
            Point startingPoint = new Point();
            Queue<Point> unvisitedPoints = new Queue<Point>();
            List<Point> visitedPoints = new List<Point>();

            bool found = false;
            for (int i = 0; i < bmp.Height && !found; i++) {
                for (int j = 0; j < bmp.Width && !found; j++) {
                    Color pixelColor = bmp.GetPixel(j, i);
                    if (pixelColor.ToArgb() == Color.Black.ToArgb()) {
                        startingPoint = new Point(j, i);
                        found = true;
                    }
                }
            }

            if (!found) {
                throw new Exception("No figure found");
            }

            // Only add to the queue one of the connected points
            List<Point> neighbourPoints = GetNeighbourPoints(bmp, startingPoint, Color.Black);
            if (neighbourPoints.Count != 2) {
                throw new Exception("Too many neighbours");
            }
            visitedPoints.Add(startingPoint);

            visitedPoints.Add(neighbourPoints[0]);
            unvisitedPoints.Enqueue(neighbourPoints[0]);
            result.Add(new GraphicsPath());

            while (unvisitedPoints.Count != 0) {
                Point currentPoint = unvisitedPoints.Dequeue();

                foreach (Point newPoint in GetNeighbourPoints(bmp, currentPoint, Color.Black)) {
                    if (visitedPoints.Contains(newPoint)) continue;

                    Color pixelColor = bmp.GetPixel(newPoint.X, newPoint.Y);

                    if (pixelColor.ToArgb() != Color.White.ToArgb()) {
                        unvisitedPoints.Enqueue(newPoint);
                        visitedPoints.Add(newPoint);
                    }
                }
            }

            result[0].AddPolygon(visitedPoints.ToArray());

            return result;
        }

        private List<Point> GetNeighbourPoints(Bitmap bmp, Point point, Color color) {
            List<Point> result = new List<Point>();

            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    Point newPoint = new Point(point.X + j, point.Y + i);

                    if (i == j && i == 0) continue;
                    if (newPoint.X < 0 || newPoint.X > bmp.Width) continue;
                    if (newPoint.Y < 0 || newPoint.Y > bmp.Height) continue;
                    if (bmp.GetPixel(newPoint.X, newPoint.Y).ToArgb() == color.ToArgb()) {
                        result.Add(newPoint);
                    }
                }
            }
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
