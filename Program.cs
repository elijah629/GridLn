#pragma warning disable CA1416
using System.Drawing;
using System.Drawing.Imaging;

namespace GridLn
{
    internal class Program
    {
        public static Point cursorPos = new(0, 0);
        public static Color cursorColor = Color.Black;
        public static Bitmap? image;
        public static void Main(string[] args)
        {
            Lexer(File.ReadAllText(args[0]));
        }

        public static void Lexer(string file)
        {
            string outputFileName = "";
            image = new Bitmap(1, 1);
            string[] lines = file.Split('\n');
            for (int line = 0; line < lines.Length; line++)
            {
                string currLine = lines[line].Trim();
                Console.WriteLine(currLine);
                if (currLine.StartsWith('!'))
                {
                    string[] header = currLine.Split(' ');
                    switch (header[0][1..])
                    {
                        case "BGC":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.Clear(Color.FromArgb(int.Parse(header[1]), int.Parse(header[2]), int.Parse(header[3])));
                            }

                            break;
                        case "WID":
                            image = new Bitmap(image, int.Parse(header[1]) + 1, image.Height);
                            break;
                        case "HEI":
                            image = new Bitmap(image, image.Width, int.Parse(header[1]) + 1);
                            break;
                        case "OUT":
                            outputFileName = header[1];
                            break;
                        default:
                            Console.WriteLine("Error Parsing Headers");
                            Environment.Exit(-1);
                            break;
                    }
                }
                else
                {
                    string[] command = currLine.Split(' ');
                    switch (command[0])
                    {
                        case "MOV":
                            cursorPos.X = Convert.ToInt32(command[1]);
                            cursorPos.Y = Convert.ToInt32(command[2]);
                            break;
                        case "LIN":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.DrawLine(new Pen(cursorColor), cursorPos.X, cursorPos.Y, int.Parse(command[1]), int.Parse(command[2]));
                            }
                            break;
                        case "LMV":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.DrawLine(new Pen(cursorColor), cursorPos.X, cursorPos.Y, int.Parse(command[1]), int.Parse(command[2]));
                            }
                            cursorPos.X = Convert.ToInt32(command[1]);
                            cursorPos.Y = Convert.ToInt32(command[2]);
                            break;
                        case "CLR":
                            cursorColor = Color.FromArgb(int.Parse(command[1]), int.Parse(command[2]), int.Parse(command[3]));
                            break;
                        case "ARC":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.DrawArc(new Pen(cursorColor), cursorPos.X, cursorPos.Y, int.Parse(command[1]), int.Parse(command[2]), int.Parse(command[3]), int.Parse(command[4]));
                            }
                            break;
                        case "BEZ":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.DrawBezier(new Pen(cursorColor), int.Parse(command[1]), int.Parse(command[2]), int.Parse(command[3]), int.Parse(command[4]), int.Parse(command[5]), int.Parse(command[6]), int.Parse(command[7]), int.Parse(command[8]));
                            }
                            break;
                        case "CCU":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                List<Point> points = new((command.Length - 1) / 2);
                                for (int i = 1; i < command.Length; i += 2)
                                {
                                    points.Add(new Point(int.Parse(command[i]), int.Parse(command[i + 1])));
                                }
                                g.DrawClosedCurve(new Pen(cursorColor), points.ToArray());
                            }
                            break;
                        case "CUR":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                List<Point> points = new((command.Length - 1) / 2);
                                for (int i = 1; i < command.Length; i += 2)
                                {
                                    points.Add(new Point(int.Parse(command[i]), int.Parse(command[i + 1])));
                                }
                                g.DrawCurve(new Pen(cursorColor), points.ToArray());
                            }
                            break;
                        case "ELL":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.DrawEllipse(new Pen(cursorColor), cursorPos.X, cursorPos.Y, int.Parse(command[1]), int.Parse(command[2]));
                            }
                            break;
                        case "PIE":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.DrawPie(new Pen(cursorColor), cursorPos.X, cursorPos.Y, int.Parse(command[1]), int.Parse(command[2]), int.Parse(command[3]), int.Parse(command[4]));
                            }
                            break;
                        case "POL":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                List<Point> points = new((command.Length - 1) / 2);
                                for (int i = 1; i < command.Length; i += 2)
                                {
                                    points.Add(new Point(int.Parse(command[i]), int.Parse(command[i + 1])));
                                }
                                g.DrawPolygon(new Pen(cursorColor), points.ToArray());
                            }
                            break;
                        case "REC":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.DrawRectangle(new Pen(cursorColor), cursorPos.X, cursorPos.Y, int.Parse(command[1]), int.Parse(command[2]));
                            }
                            break;
                        case "TXT":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.DrawString(string.Join(' ', (string?[])command, 1, command.Length), SystemFonts.DefaultFont, new SolidBrush(cursorColor), cursorPos.X, cursorPos.Y);
                            }
                            break;
                        case "FCC":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                List<Point> points = new((command.Length - 1) / 2);
                                for (int i = 1; i < command.Length; i += 2)
                                {
                                    points.Add(new Point(int.Parse(command[i]), int.Parse(command[i + 1])));
                                }
                                g.FillClosedCurve(new SolidBrush(cursorColor), points.ToArray());
                            }
                            break;
                        case "FEL":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.FillEllipse(new SolidBrush(cursorColor), cursorPos.X, cursorPos.Y, int.Parse(command[1]), int.Parse(command[2]));
                            }
                            break;
                        case "FPI":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.FillPie(new SolidBrush(cursorColor), int.Parse(command[1]), int.Parse(command[2]), int.Parse(command[3]), int.Parse(command[4]), int.Parse(command[5]), int.Parse(command[7]));
                            }
                            break;
                        case "FPO":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                List<Point> points = new((command.Length - 1) / 2);
                                for (int i = 1; i < command.Length; i += 2)
                                {
                                    points.Add(new Point(int.Parse(command[i]), int.Parse(command[i + 1])));
                                }
                                g.FillPolygon(new SolidBrush(cursorColor), points.ToArray());
                            }
                            break;
                        case "FRE":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.FillRectangle(new SolidBrush(cursorColor), cursorPos.X, cursorPos.Y, int.Parse(command[1]), int.Parse(command[2]));
                            }
                            break;
                        case "PIX":
                            using (Graphics g = Graphics.FromImage(image))
                            {
                                g.FillRectangle(new SolidBrush(cursorColor), cursorPos.X, cursorPos.Y, 1, 1);
                            }
                            break;
                        case "EOF":
                            image.Save(outputFileName, ImageFormat.Png);
                            break;

                        case "~": break;
                        default:
                            Console.WriteLine("Error Parsing Functions");
                            Environment.Exit(-1);
                            break;
                    }
                }
            }
        }
    }
}
#pragma warning restore CA1416