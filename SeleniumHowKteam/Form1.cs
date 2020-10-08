using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using Patagames.Ocr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace SeleniumHowKteam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string GetCaptChaCode()
        {
            string captchaCode = "";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri("http://tracuuhoadon.gdt.gov.vn/Captcha.jpg"), "Captcha.jpg");
                //var autoOcr = new AutoOcr();
                //Image img = new Bitmap(Application.StartupPath + "\\Captcha.jpg");
                //captchaCode = autoOcr.Read(img).ToString();
                using (var objOcr = OcrApi.Create())
                {
                    objOcr.Init(Patagames.Ocr.Enums.Languages.English);

                    captchaCode = objOcr.GetTextFromImage(Application.StartupPath + "\\Captcha.jpg");
                }
                return captchaCode = Regex.Replace(captchaCode, @"\r\n?|\n", "");
            }
        }
        public string GetCaptChaCode(string path)
        {
            string captchaCode = "";
            using (var objOcr = OcrApi.Create())
            {
                objOcr.Init(Patagames.Ocr.Enums.Languages.English);

                captchaCode = objOcr.GetTextFromImage(path);
            }
            return captchaCode = Regex.Replace(captchaCode, @"\r\n?|\n", "");
        }
        public void ScreenshotOfElement()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "http://ankpro.com/";
            TakesScreenshot(driver, driver.FindElement(By.ClassName("jumbotron")));
            driver.Quit();
        }

        public string TakesScreenshot(IWebDriver driver, IWebElement element)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";
            Byte[] byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            Bitmap screenshot = new Bitmap(new System.IO.MemoryStream(byteArray));
            Rectangle croppedImage = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
            screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
            screenshot.Save(String.Format(fileName, ImageFormat.Jpeg));
            return fileName;
        }
        private void btnFirefox_Click(object sender, EventArgs e)
        {
            //string captchaCode = GetCaptChaCode();

            FirefoxDriver firefoxDriver = new FirefoxDriver();
            firefoxDriver.Url = "http://tracuuhoadon.gdt.gov.vn/tbphtc.html";
            firefoxDriver.Navigate();

            string path =  TakesScreenshot(firefoxDriver, firefoxDriver.FindElementByXPath("/html/body/div[1]/table[2]/tbody/tr/td/div/table/tbody/tr[2]/td/form/div[2]/table/tbody/tr[3]/td[2]/table/tbody/tr/td[1]/div"));

            string captchaCode = GetCaptChaCode(Application.StartupPath + "\\" + path);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            var mst = firefoxDriver.FindElementById("tin");
            mst.SendKeys("0401381539");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            var dateFrom = firefoxDriver.FindElementById("ngayTu");
            dateFrom.SendKeys("01/01/2016");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            var dateTo = firefoxDriver.FindElementById("ngayDen");
            dateTo.SendKeys("01/01/2020");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            var captchaCodeVerify = firefoxDriver.FindElementById("captchaCodeVerify");
            captchaCodeVerify.SendKeys(captchaCode);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            var searchBtn = firefoxDriver.FindElementById("searchBtn");
            searchBtn.Click();

            string html = firefoxDriver.PageSource;

            string validateCaptcha = Regex.Match(html, "<label id=\"lbLoiCode\"><font color=\"red\"> Sai mã xác thực!</font></label>",RegexOptions.Singleline).ToString();
            if (String.IsNullOrWhiteSpace(validateCaptcha))
            {
                var code = firefoxDriver.FindElementById("reTin");
                var status = firefoxDriver.FindElementById("reTtHoatdong");
                var address = firefoxDriver.FindElementById("reTinAddr");
                var manager = firefoxDriver.FindElementById("reCqtQl");

                if (!String.IsNullOrWhiteSpace(code.Text))
                {
                    var list = Regex.Matches(html, "(?=<tr role=\"row\" id=\")(.*?)(?=</tr>)", RegexOptions.Singleline);
                    foreach (var item in list)
                    {
                        string stt = Regex.Match(item.ToString(), "aria-describedby=\"grid_ketqua_rn(.*?)(</td)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_rn\">", "").Replace("</td", "");
                        string id = Regex.Match(item.ToString(), "aria-describedby=\"grid_ketqua_id\">(.*?)(</td)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_id\">", "").Replace("</td", "");
                        string tin = Regex.Match(item.ToString(), "aria-describedby=\"grid_ketqua_dtnt_tin\">(.*?)(</td)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_dtnt_tin\">", "").Replace("</td", "");
                        string loaiTBPhathanh = Regex.Match(item.ToString(), "aria-describedby=\"grid_ketqua_loaitb_phanh\">(.*?)(</td)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_loaitb_phanh\">", "").Replace("</td", "");
                        string lanThaydoi = Regex.Match(item.ToString(), "aria-describedby=\"grid_ketqua_lan_thaydoi\">(.*?)(</td)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_lan_thaydoi\">", "").Replace("</td", "");
                        string ngayPhatHanh = Regex.Match(item.ToString(), "a href=\"#\" (.*?)(/a>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_lan_thaydoi\">", "").Replace("</td", "");
                        string soThongBao = Regex.Match(item.ToString(), "aria-describedby=\"grid_ketqua_so_thong_bao\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_so_thong_bao\">", "").Replace("</td", "").Replace(">","");
                        string chiCucQL = Regex.Match(item.ToString(), "aria-describedby=\"grid_ketqua_cqt_ten\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_cqt_ten\">", "").Replace("</td", "");
                        string ghiChu = HttpUtility.HtmlDecode(Regex.Match(item.ToString(), "aria-describedby=\"grid_ketqua_ghi_chu\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_ghi_chu\">", "").Replace("</td", "").Replace(">",""));
                    }
                }
                var link1 = firefoxDriver.FindElementByXPath("/html/body/div[1]/table[2]/tbody/tr/td/div/table/tbody/tr[2]/td/form/div[5]/div[3]/div[3]/div/table/tbody/tr[2]/td[6]/a");
                link1.Click();
                html = firefoxDriver.PageSource;

                string sTT = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_rn\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_rn\">", "").Replace("</td", "").Replace(">","");
                string tenLoaiHD = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_ach_ten\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_ach_ten\">", "").Replace("</td", "").Replace(">", "");
                string mauSo = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_ach_ma\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_ach_ma\">", "").Replace("</td", "").Replace(">", "");
                string kyHieuHD = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_kyhieu\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_kyhieu\">", "").Replace("</td", "").Replace(">", "");
                string soLuong = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_soluong\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_soluong\">", "").Replace("</td", "").Replace(">", "");
                string tuSo = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_tu_so\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_tu_so\">", "").Replace("</td", "").Replace(">", "");
                string denSo = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_den_so\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_den_so\">", "").Replace("</td", "").Replace(">", "");
                string ngayBDSD = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_ngay_sdung\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_ngay_sdung\">", "").Replace("</td", "").Replace(">", "");
                string tenDNIn = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_nin_ten\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_nin_ten\">", "").Replace("</td", "").Replace(">", "");
                string mstDNIn = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_nin_tin\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_nin_tin\">", "").Replace("</td", "").Replace(">", "");
                string soHDIn = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_so_hopdong\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_so_hopdong\">", "").Replace("</td", "").Replace(">", "");
                string ngayHDIn = Regex.Match(html, "aria-describedby=\"grid_ketqua_viewtbph_ngay_hopdong\">(.*?)(</td>)", RegexOptions.Singleline).ToString().Replace("aria-describedby=\"grid_ketqua_viewtbph_ngay_hopdong\">", "").Replace("</td", "").Replace(">", "");

                Thread.Sleep(TimeSpan.FromSeconds(5));
                var button = firefoxDriver.FindElementByXPath("/html/body/div[6]/div[11]/div/button/span");
                button.Click();

                var link2 = firefoxDriver.FindElementByXPath("/html/body/div[1]/table[2]/tbody/tr/td/div/table/tbody/tr[2]/td/form/div[5]/div[3]/div[3]/div/table/tbody/tr[3]/td[6]/a");
                link2.Click();

                Thread.Sleep(TimeSpan.FromSeconds(5));
                var button2 = firefoxDriver.FindElementByXPath("/html/body/div[6]/div[11]/div/button/span");
                button2.Click();
            }
            else
            {
                firefoxDriver.Quit();
                btnFirefox.PerformClick();
            }            
        }

        private static void RemoveLines(MagickImage original, MagickImage image, string geometryValue)
        {
            image.Scale(new MagickGeometry(geometryValue));
            MagickGeometry geometry = new MagickGeometry(original.Width, original.Height);
            geometry.IgnoreAspectRatio = true;
            image.Scale(geometry);

            image.AutoLevel();
            //image.Shave(20, 20);
            image.Despeckle();
            image.ReduceNoise();

            image.Morphology(MorphologyMethod.Erode, Kernel.Diamond, 2);
            using (MagickImage clone = (MagickImage)image.Clone())
            {
                image.Negate();
                image.Composite(original, Gravity.Center);
            }
        }

        public static void RemoveLines(string filePath)
        {
            using (MagickImage image = new MagickImage(filePath)) // image001.tif
            {
                image.RePage(); // +repage
                image.RemoveArtifact(filePath);
                using (MagickImage original = (MagickImage)image.Clone())
                {
                    using (MagickImage firstClone = (MagickImage)original.Clone()) // -clone 0
                    {
                        RemoveLines(original, firstClone, "x1!"); // -scale x1!
                        using (MagickImage secondClone = (MagickImage)firstClone.Clone()) // -clone 0
                        {
                            RemoveLines(secondClone, firstClone, "1x!"); // -scale x1!
                            firstClone.Write(Application.StartupPath + Path.GetFileName(filePath));
                        }
                    }
                }
            }
        }
        public string imageLocation = "";
        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var dialogResult =  openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Image img = new Bitmap(openFileDialog.FileName);
                picImageCaptcha.Image = img;
                picImageCaptcha.ImageLocation = openFileDialog.FileName;
                imageLocation = openFileDialog.FileName;
            }
        }

        public string ProcessImage(string imagePath)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";

            MagickImage img = new MagickImage(imagePath);
            img = (MagickImage)img.Clone();
            var settings = new MorphologySettings();
            settings.Channels = Channels.Alpha;
            settings.Method = MorphologyMethod.Distance;
            settings.Kernel = Kernel.Euclidean;
            settings.KernelArguments = "1,10!";

            img.Alpha(AlphaOption.Set);
            img.VirtualPixelMethod = VirtualPixelMethod.Transparent;
            img.Morphology(settings);
            img.Write(fileName);

            return fileName;
        }
        private void btnProcessImage_Click(object sender, EventArgs e)
        {
            MagickImage img = new MagickImage(picImageCaptcha.ImageLocation);
            img = (MagickImage)img.Clone();
            var settings = new MorphologySettings();
            settings.Channels = Channels.Alpha;
            settings.Method = MorphologyMethod.Distance;
            settings.Kernel = Kernel.Euclidean;
            settings.KernelArguments = "1,10!";

            img.Alpha(AlphaOption.Set);
            img.VirtualPixelMethod = VirtualPixelMethod.Transparent;
            img.Morphology(settings);
            img.Write(Application.StartupPath + Path.GetFileName(picImageCaptcha.ImageLocation));

            Image image = new Bitmap(Application.StartupPath + Path.GetFileName(picImageCaptcha.ImageLocation));
            picImageCaptcha.Image = image;
            picImageCaptcha.ImageLocation = Application.StartupPath + Path.GetFileName(picImageCaptcha.ImageLocation);

            //using (var image = new MagickImage(picImageCaptcha.ImageLocation))
            //{
            //    // -alpha off ) ^
            //    image.Alpha(AlphaOption.Off);

            //    using (var images = new MagickImageCollection())
            //    {
            //        // ( -clone 0
            //        var tmp1 = image.Clone();

            //        // -morphology close rectangle:1x50
            //        tmp1.Morphology(MorphologyMethod.Close, "rectangle:1x50");

            //        // -negate
            //        tmp1.Negate();

            //        // +write tmp1.png ) ^
            //        tmp1.Write(Application.StartupPath + "\\tmp1.png");

            //        images.Add(tmp1);

            //        // ( -clone 0
            //        var tmp2 = image.Clone();

            //        // -morphology close rectangle:50x1
            //        tmp2.Morphology(MorphologyMethod.Close, "rectangle:50x1");

            //        // -negate
            //        tmp2.Negate();

            //        // +write tmp2.png ) ^
            //        tmp2.Write(Application.StartupPath + "\\tmp2.png");

            //        images.Add(tmp2);

            //        // -evaluate-sequence add
            //        using (var tmp3 = images.Evaluate(EvaluateOperator.Add))
            //        {
            //            // +write tmp3.png ) ^
            //            tmp3.Write(Application.StartupPath + "\\tmp3.png");

            //            // -compose plus -composite ^
            //            image.Composite(tmp3, CompositeOperator.Plus);

            //            // result.png
            //            image.Write(Application.StartupPath + Path.GetFileName(picImageCaptcha.ImageLocation));
            //        }
            //    }
            //}
        }

        private void btnGetCaptChaCode_Click(object sender, EventArgs e)
        {
           txtCaptchaCode.Text =  GetCaptChaCode(picImageCaptcha.ImageLocation);
        }

        public string DowLoadCaptChaCode(string url)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url), fileName);
                return fileName;
            }
        }
        private void btnSearchNguoiNopThue_Click(object sender, EventArgs e)
        {
            FirefoxDriver firefoxDriver = new FirefoxDriver();
            firefoxDriver.Url = "http://tracuunnt.gdt.gov.vn/tcnnt/mstdn.jsp";
            firefoxDriver.Navigate();


            IWebElement image = firefoxDriver.FindElementByXPath("/html/body/div/div[1]/div[4]/div[2]/div[2]/div/div/div/form/table/tbody/tr[6]/td[2]/table/tbody/tr/td[2]/div/img");
            //Rihgt click on Image using contextClick() method.
            //Actions action = new Actions(firefoxDriver);
            //action.ContextClick(image).Build().Perform();

            //action.SendKeys(OpenQA.Selenium.Keys.Control).Build().Perform();

            Actions act = new Actions(firefoxDriver);
            act.ContextClick(image).SendKeys(OpenQA.Selenium.Keys.ArrowDown).SendKeys(OpenQA.Selenium.Keys.ArrowDown).SendKeys(OpenQA.Selenium.Keys.Return).Build().Perform();

            //var divImage = firefoxDriver.FindElementByXPath("/html/body/div/div[1]/div[4]/div[2]/div[2]/div/div/div/form/table/tbody/tr[6]/td[2]/table/tbody/tr/td[2]/div/img");

            //string urlImage = divImage.GetAttribute("src").ToString();

            //string imageDowLoad = DowLoadCaptChaCode(urlImage);

            string path = TakesScreenshot(firefoxDriver, firefoxDriver.FindElementByXPath("/html/body/div/div[1]/div[4]/div[2]/div[2]/div/div/div/form/table/tbody/tr[6]/td[2]/table/tbody/tr/td[2]/div/img"));
            string imgScreenshot = Application.StartupPath + "\\" + path;
            string fileName = ProcessImage(imgScreenshot);
            string captchaCode = GetCaptChaCode(Application.StartupPath + "\\" + fileName);


            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            var mst = firefoxDriver.FindElementByName("mst");
            mst.SendKeys("0401381539");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var captChaCode = firefoxDriver.FindElementById("captcha");
            captChaCode.SendKeys(captchaCode);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var btnSubmit = firefoxDriver.FindElementByClassName("subBtn");
            btnSubmit.Click();

        }
    }
}
