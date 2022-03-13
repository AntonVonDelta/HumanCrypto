using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanCrypto {
    class PicassoConstruction : IDisposable {
        public struct PartInfo {
            public string partName;
            public Point position;
            public PartInfo(string partName, Point position) {
                this.partName = partName;
                this.position = position;
            }
        }

        private GenomeProcessing genomeProcessing;
        private Bitmap cachedBitmap = null;

        public PicassoConstruction(GenomeProcessing genomeProcessing) {
            this.genomeProcessing = genomeProcessing;
        }

        public Bitmap GetBitmap() {
            if (cachedBitmap != null) return cachedBitmap;

            Bitmap bmp = new Bitmap(550, 550);

            using (Graphics g = Graphics.FromImage(bmp)) {
                XDocument doc = XDocument.Load("HumanParts\\data.xml");
                Queue<PartInfo> attachmentPoints = new Queue<PartInfo>();

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
                            attachmentPoints.Enqueue(new PartInfo(partEl.Name.ToString(), newAttachPoint + offsetPoint));
                        }

                        genomeProcessing.AdvanceGene();
                    }
                }
            }

            return bmp;
        }


        /// <summary>
        /// Adds the offset point to all points in path
        /// </summary>
        /// <param name="svg"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static GraphicsPath GetSvgPath(string svg, Size offset) {
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

        public void Dispose() {
            if (cachedBitmap != null) {
                cachedBitmap.Dispose();
            }
        }
    }
}
