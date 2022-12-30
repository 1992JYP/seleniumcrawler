using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace seleniumcrawler
{
    public partial class Form1 : Form
    {
        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;
        public Form1()
        {
            InitializeComponent();

            try
            {
                _driverService = ChromeDriverService.CreateDefaultService();
                _driverService.HideCommandPromptWindow = true;

                _options = new ChromeOptions();
                _options.AddArgument("disable-gpu");

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            _driver = new ChromeDriver(_driverService,_options);
            _driver.Navigate().GoToUrl("https://www.kofiabond.or.kr/index.html");

            Thread.Sleep(3000);

            if (_driver.WindowHandles.Count > 1)
            {
                _driver.SwitchTo().Window(_driver.WindowHandles[1]);
                _driver.Close(); 
                _driver.SwitchTo().Window(_driver.WindowHandles[0]);

            }
            Thread.Sleep(3000);

            var mainFrame = _driver.FindElement(By.XPath("/html/frameset/frame[2]"));
            _driver.SwitchTo().Frame(mainFrame);
            var a = _driver.FindElement(By.XPath("//*[@id=\"group33\"]"));
            if (a != null) { a.Click(); }

            var pagesource = _driver.PageSource;

            Thread.Sleep(5000);
            _driver.SwitchTo().DefaultContent();

            //var newmainFrame = _driver.FindElement(By.XPath("/html/frameset/frame[2]"));
            
            _driver.SwitchTo().Frame("fraAMAKMain");
            _driver.SwitchTo().Frame("maincontent");
            _driver.SwitchTo().Frame("tabContents1_contents_tabs1_body");

            var b = _driver.FindElement(By.XPath("//*[@id=\"group96\"]"));
            b.Click();

            label1.Text = b.Text;

        }

    }
}