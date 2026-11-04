using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    public class Utility
    {
        public static string CreateXML(Object Object)
        {
            XmlDocument XmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer XmlSerializer = new XmlSerializer(Object.GetType());
            // Creates a stream whose backing store is memory. 
            try
            {
                using (MemoryStream XmlStream = new MemoryStream())
                {
                    XmlSerializer.Serialize(XmlStream, Object);
                    XmlStream.Position = 0;
                    //Loads the XML document from the specified string.
                    XmlDoc.Load(XmlStream);
                    return XmlDoc.InnerXml;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "Zero";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 10000000) > 0)
            {
                words += ConvertNumbertoWords(number / 10000000) + " Corer ";
                number %= 10000000;
            }
            if ((number / 100000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " Lakh ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " Hundred ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "and ";
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thitry", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public static string ConvertCurrancytoInWords(double? numbers, Boolean paisaconversion = false, int paisa = 0)
        {
            var pointindex = numbers.ToString().IndexOf(".");

            var paisaamt = 0;
            if (pointindex > 0)
            {
                var pointindexLength = numbers.ToString().Substring(numbers.ToString().IndexOf(".") + 1).Length;
                if (pointindexLength > 1)
                {
                    paisaamt = Convert.ToInt32(numbers.ToString().Substring(pointindex + 1, 2));
                }
                else
                {
                    paisaamt = Convert.ToInt32(numbers.ToString().Substring(pointindex + 1, 1) + "0");
                }

            }

            int number = Convert.ToInt32(numbers);

            if (number == 0) return "Zero";

            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0)
                {
                    sb.Append(words0[h] + "Hundred ");
                }
                if (u > 0 || t > 0)
                {
                    //if (h > 0 || (paisa > 0  && i == 0))
                    //{
                    //    sb.Append("and ");
                    //}
                    if (paisa > 0 && i == 0)
                    {
                        sb.Append("and ");
                    }

                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }

            if (paisaamt == 0 && paisaconversion == false)
            {
                //sb.Append("ruppes only");
                sb.Append("only");
            }
            else if (paisaamt > 0)
            {
                var paisatext = ConvertCurrancytoInWords(paisaamt, true, 1);
                //sb.AppendFormat("rupees {0} paise only", paisatext);
                sb.AppendFormat("{0} paise only", paisatext);
            }
            return sb.ToString().TrimEnd();
        }
        public static string DisplayIndianCurrency(string _amt)
        {
            string text = "";
            if (!string.IsNullOrEmpty(_amt))
            {
                decimal parsed = decimal.Parse(_amt, CultureInfo.InvariantCulture);
                CultureInfo en = new CultureInfo("en-IN");
                text = string.Format(en, "{0:N2}", parsed);
            }
            return text;
        }

        public static string DisplayTwoDecimal(string _amt)
        {
            string text = "";
            if (!string.IsNullOrEmpty(_amt))
            {
                decimal parsed = decimal.Parse(_amt, CultureInfo.InvariantCulture);
                CultureInfo en = new CultureInfo("en-IN");
                text = string.Format(en, "{0:.00}", parsed);
            }
            return text;
        }

        public static string DisplayThreeDecimal(string _amt)
        {
            string text = "";
            if (!string.IsNullOrEmpty(_amt))
            {
                decimal parsed = decimal.Parse(_amt, CultureInfo.InvariantCulture);
                CultureInfo en = new CultureInfo("en-IN");
                text = string.Format(en, "{0:.00}", parsed);
            }
            return text;
        }
        public static string DisplayIndianCurrency3(string _amt)
        {
            string text = "";
            if (!string.IsNullOrEmpty(_amt))
            {
                decimal parsed = decimal.Parse(_amt, CultureInfo.InvariantCulture);
                CultureInfo en = new CultureInfo("en-IN");
                text = string.Format(en, "{0:N3}", parsed);
            }
            return text;
        }
        public static string DisplayIndianCurrency4(string _amt)
        {
            string text = "";
            if (!string.IsNullOrEmpty(_amt))
            {
                decimal parsed = decimal.Parse(_amt, CultureInfo.InvariantCulture);
                CultureInfo en = new CultureInfo("en-IN");
                text = string.Format(en, "{0:N4}", parsed);
            }
            return text;
        }
        public static string DisplayIndianCurrencyINR(string _amt)
        {
            string text = "";
            if (!string.IsNullOrEmpty(_amt))
            {
                decimal parsed = decimal.Parse(_amt, CultureInfo.InvariantCulture);
                CultureInfo en = new CultureInfo("en-IN");
                text = string.Format(en, "{0:c}", parsed).Replace(" ", "");
            }
            return text;
        }
        public static string DisplayIndianCurrencyINT(string _amt)
        {
            string text = "";
            if (!string.IsNullOrEmpty(_amt))
            {
                decimal parsed = decimal.Parse(_amt, CultureInfo.InvariantCulture);
                CultureInfo en = new CultureInfo("en-IN");
                text = string.Format(en, "{0:N0}", parsed);
            }
            return text;
        }
        public static void CleanOldFiles(string FilePath)
        {
            try
            {
                string[] _directory = Directory.GetDirectories(FilePath);
                if (_directory.Length > 0)
                {
                    foreach (var itemD in _directory)
                    {
                        var _path = itemD;
                        string[] _subDirectory = Directory.GetDirectories(_path);

                        if (_subDirectory.Length > 0)
                        {
                            foreach (var itemSD in _subDirectory)
                            {
                                string[] sfiles = Directory.GetFiles(itemSD);
                                if (sfiles.Length > 0)
                                {
                                    foreach (string file in sfiles)
                                    {
                                        FileInfo fi = new FileInfo(file);
                                        if (fi.CreationTime < DateTime.Now.AddDays(-2))
                                            fi.Delete();
                                    }
                                }
                            }
                        }
                        else
                        {
                            string[] files = Directory.GetFiles(_path);
                            if (files.Length > 0)
                            {
                                foreach (string file in files)
                                {
                                    FileInfo fi = new FileInfo(file);
                                    if (fi.CreationTime < DateTime.Now.AddDays(-2))
                                        fi.Delete();
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
        public static string changeNumericToWords(double numb)
        {
            string num = numb.ToString(); return changeToWords(num, false);
        }
        private static string changeToWords(string numb, bool isCurrency)
        {


            string val = "", wholeNo = "", points = "", andStr = "", pointStr = "";
            string endStr = (isCurrency) ? ("Only.") : ("");

            bool isnegative = false;
            if (numb.Substring(0, 1) == "-")
            {
                isnegative = true;
                numb = Convert.ToString(System.Math.Abs(Convert.ToDecimal(numb)));
                wholeNo = numb;
            }
            else
            {
                wholeNo = numb;
            }


            try
            {
                int decimalPlace = numb.IndexOf("."); if (decimalPlace > 0)
                {


                    wholeNo = numb.Substring(0, decimalPlace);


                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {


                        andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/cents all
                        endStr = (isCurrency) ? ("Paisa " + endStr) : ("");


                        pointStr = translateCents(points);


                    }

                }
                val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);


            }
            catch {; }

            if (isnegative == true)
            {
                return "Minus " + val;
            }
            else
            {
                return val;
            }



        }
        private static string translateWholeNumber(String number)
        {


            string word = "";


            try
            {


                bool beginsZero = false;//tests for 0XX


                bool isDone = false;//test if already translated
                double dblAmt = (Convert.ToDouble(number));


                //if ((dblAmt > 0) && number.StartsWith("0"))
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric
                    beginsZero = number.StartsWith("0");


                    int numDigits = number.Length;


                    int pos = 0;//store digit grouping


                    String place = "";//digit grouping name:hundres,thousand,etc...
                    switch (numDigits)
                    {


                        case 1://ones' range


                            word = ones(number);
                            isDone = true;


                            break;


                        case 2://tens' range


                            word = tens(number);
                            isDone = true;


                            break;


                        case 3://hundreds' range


                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";


                            break;


                        case 4://thousands' range
                        case 5:


                            pos = (numDigits % 4) + 1;


                            place = " Thousand ";
                            break;


                        case 6://Lakhs' range
                        case 7:


                            pos = (numDigits % 6) + 1;


                            place = " Lakh ";
                            break;


                        case 8://Crores' range
                        case 9:


                            pos = (numDigits % 8) + 1;


                            place = " Crore ";
                            break;


                        case 10://Arabs range
                        case 11:


                            //pos = (numDigits % 10) + 1;
                            //place = " Arab ";
                            pos = (numDigits % 8) + 1;
                            place = " Crore ";


                            break;




                        //add extra case options for anything above Billion...
                        default:


                            isDone = true;
                            break;


                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)


                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));


                        //check for trailing zeros
                        if (beginsZero) word = " and " + word.Trim();


                    }


                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim())) word = "";


                }


                String Result = word.Trim();
                Result = Result.Replace("and Hundred", "");
                Result = Result.Replace("and Thousand", "");
                Result = Result.Replace("and Lakh", "");
                Result = Result.Replace("and Crore", "");
                Result = Result.Replace(" and ", " ");


                word = Result;
            }
            catch {; }
            return word.Trim();


        }
        private static string translateCents(String cents)
        {
            String cts = "", digit = "", engOne = "";
            for (int i = 0; i < cents.Length; i++)
            {
                digit = cents[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cts += " " + engOne;
                if (i == 1)
                {
                    if (Convert.ToInt32(cents) > 9 && Convert.ToInt32(cents) < 21)
                    {
                        cts = " " + tens(cents);
                    }
                    else
                    {
                        digit = cents[0].ToString();
                        cts = " " + tens(digit + "0");
                        digit = cents[1].ToString();
                        cts += " " + ones(digit);
                    }
                }
            }
            return cts;
        }
        private static string ones(String digit)
        {
            int digt = Convert.ToInt32(digit);


            String name = "";
            switch (digt)
            {
                case 1:


                    name = "One";
                    break;


                case 2:
                    name = "Two";


                    break;
                case 3:


                    name = "Three";
                    break;


                case 4:
                    name = "Four";


                    break;
                case 5:


                    name = "Five";
                    break;


                case 6:
                    name = "Six";


                    break;
                case 7:


                    name = "Seven";
                    break;


                case 8:
                    name = "Eight";


                    break;
                case 9:


                    name = "Nine";
                    break;


            }
            return name;


        }
        private static string tens(String digit)
        {


            int digt = Convert.ToInt32(digit);
            String name = null; switch (digt)
            {


                case 10:
                    name = "Ten";


                    break;
                case 11:


                    name = "Eleven";
                    break;


                case 12:
                    name = "Twelve";


                    break;
                case 13:


                    name = "Thirteen";
                    break;


                case 14:
                    name = "Fourteen";


                    break;
                case 15:


                    name = "Fifteen";
                    break;


                case 16:
                    name = "Sixteen";


                    break;
                case 17:


                    name = "Seventeen";
                    break;


                case 18:
                    name = "Eighteen";


                    break;
                case 19:


                    name = "Nineteen";
                    break;


                case 20:
                    name = "Twenty";


                    break;
                case 30:


                    name = "Thirty";
                    break;


                case 40:
                    name = "Fourty";


                    break;
                case 50:


                    name = "Fifty";
                    break;


                case 60:
                    name = "Sixty";


                    break;
                case 70:


                    name = "Seventy";
                    break;


                case 80:
                    name = "Eighty";


                    break;
                case 90:


                    name = "Ninety";
                    break;


                default:
                    if (digt > 0)
                    {
                        name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));


                    }
                    break;


            }
            return name;


        }

        public static List<String> WrapperString(String message)
        {
            int maxLength = 60;
            List<String> Line = new List<String>();
            String[] words;

            message = message.Trim();
            words = message.Split(" ".ToCharArray());

            StringBuilder sentence = new StringBuilder();
            foreach (String word in words)
            {
                if ((sentence.Length + word.Length) <= maxLength)
                {
                    sentence.Append(word + " ");

                }
                else
                {
                    Line.Add(sentence.ToString().Trim());
                    sentence = new StringBuilder(word + " ");
                }
            }

            if (sentence.Length > 0)
                Line.Add(sentence.ToString().Trim());

            return Line;
        }

        public static string SaveExcel(XLWorkbook wb, string DocFolder, string DocName, string DocText)
        {
            var FileName = "";
            var FinalPath = "";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Docs\\" + DocFolder + "\\");
            FileName = DocName + "_" + DocText + ".xlsx";
            FinalPath = path + FileName;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            if (System.IO.File.Exists(FinalPath))
            {
                System.IO.File.Delete(FinalPath);
            }

            wb.SaveAs(FinalPath);

            return FinalPath;
        }

        public static string LoadImage(string img, string docpath)
		{

			if (img != "")
			{

				string strm = img;

				//this is a simple white background image
				var myfilename = string.Format(@"{0}", Guid.NewGuid());

				//Generate unique filename
				//string filepath = Path.Combine(this._Environment.WebRootPath, "Docs\\QRCode" + myfilename + ".jpeg");
				string filepath = Path.Combine(docpath) + myfilename + ".jpeg";
				var bytess = Convert.FromBase64String(strm);
				using (var imageFile = new FileStream(filepath, FileMode.Create))
				{
					imageFile.Write(bytess, 0, bytess.Length);
					imageFile.Flush();
				}

				string targetpath = Path.Combine(docpath) + myfilename + "crop" + ".jpeg";
				String newfilepath = Utility.ResizeImage(filepath, targetpath);
				return newfilepath;
			}
			else
			{
				return "";

			}
		}

		public static string ResizeImage(String Source, String Target)
		{
			Bitmap source = new Bitmap(Source);
			Bitmap t = CropWhiteSpace(source);
			t.Save(Target);

			return (Target);
		}

		public static Bitmap CropWhiteSpace(Bitmap bmp)
		{
			int w = bmp.Width;
			int h = bmp.Height;
			int white = 0xffffff;

			Func<int, bool> allWhiteRow = r =>
			{
				for (int i = 0; i < w; ++i)
					if ((bmp.GetPixel(i, r).ToArgb() & white) != white)
						return false;
				return true;
			};

			Func<int, bool> allWhiteColumn = c =>
			{
				for (int i = 0; i < h; ++i)
					if ((bmp.GetPixel(c, i).ToArgb() & white) != white)
						return false;
				return true;
			};

			int topmost = 0;
			for (int row = 0; row < h; ++row)
			{
				if (!allWhiteRow(row))
					break;
				topmost = row;
			}

			int bottommost = 0;
			for (int row = h - 1; row >= 0; --row)
			{
				if (!allWhiteRow(row))
					break;
				bottommost = row;
			}

			int leftmost = 0, rightmost = 0;
			for (int col = 0; col < w; ++col)
			{
				if (!allWhiteColumn(col))
					break;
				leftmost = col;
			}

			for (int col = w - 1; col >= 0; --col)
			{
				if (!allWhiteColumn(col))
					break;
				rightmost = col;
			}

			if (rightmost == 0) rightmost = w; // As reached left
			if (bottommost == 0) bottommost = h; // As reached top.

			int croppedWidth = rightmost - leftmost;
			int croppedHeight = bottommost - topmost;

			if (croppedWidth == 0) // No border on left or right
			{
				leftmost = 0;
				croppedWidth = w;
			}

			if (croppedHeight == 0) // No border on top or bottom
			{
				topmost = 0;
				croppedHeight = h;
			}

			try
			{
				var target = new Bitmap(croppedWidth, croppedHeight);
				using (Graphics g = Graphics.FromImage(target))
				{
					g.DrawImage(bmp,
					  new RectangleF(0, 0, croppedWidth, croppedHeight),
					  new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
					  GraphicsUnit.Pixel);
				}
				return target;
			}
			catch (Exception ex)
			{
				throw new Exception(
				  string.Format("Values are topmost={0} btm={1} left={2} right={3} croppedWidth={4} croppedHeight={5}", topmost, bottommost, leftmost, rightmost, croppedWidth, croppedHeight),
				  ex);
			}
		}


		public static byte[] ConvertPdfToBytes(string pdfFilePath)
		{
			byte[] pdfBytes = null;

			try
			{
				// Create a FileStream to read the PDF file
				using (FileStream fs = new FileStream(pdfFilePath, FileMode.Open, FileAccess.Read))
				{
					// Create a BinaryReader to read the binary content
					using (BinaryReader br = new BinaryReader(fs))
					{
						// Read the entire file content into a byte array
						pdfBytes = br.ReadBytes((int)fs.Length);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}

			return pdfBytes;
		}
	}
}
