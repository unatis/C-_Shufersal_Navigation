using System;
using System.Collections.Generic;
using System.Text;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Net;
using System.IO;

namespace BritAutomation
{
    public enum BrowserNames
    {
        Chrome,
        FireFox,
        Edge
    }
    public class cCommon
    {
        public static IWebDriver driver;
        public static WebDriverWait wait;

        public static void LaunchBrowser(BrowserNames BrowserName)
        {
            try
            {
                switch (BrowserName)
                {
                    case BrowserNames.Chrome:
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--start-maximized");
                        options.AddArgument("--disable-infobars");
                        options.AddArguments("--incognito");

                        driver = new ChromeDriver("D:\\Dev\\BAutomation\\BAutomation\\Tools", options);
                        break;

                    case BrowserNames.FireFox:
                        /*FirefoxProfile profile = new FirefoxProfile();
                        profile.SetPreference("webdriver.gecko.driver", Environment.CurrentDirectory + "\\geckodriver.exe");
                        profile.SetPreference("marionette", true);
                        profile.SetPreference("plugin.state.flash", 2);

                        driver = new FirefoxDriver(profile);*/
                        break;

                    case BrowserNames.Edge:

                        break;

                    default:

                        break;

                }

                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);                
            }

        }

        public static void NavigateTo(String URL)
        {
            try
            {
                driver.Navigate().GoToUrl(URL);

                cReporter.ReportMessage("NavigateTo " + URL);
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }

        public static void CloseBrowser()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }

        }
        
        public static void WaitTime(Int32 TimeOUTSeconds)
        {
            try
            {
                Thread.Sleep(TimeOUTSeconds * 1000);
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }

        }

        public static bool SendHTTPRequest(String RequestType, String URI, String Body)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URI);
                httpWebRequest.ContentType = "application/json;text/html;";
                //httpWebRequest.ContentType = "application/json";


                httpWebRequest.Method = RequestType;

                if (!Body.Equals(String.Empty))
                {
                    using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = Body;

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                String StatusCode = httpResponse.StatusCode.ToString();
                String ResponceBody = "";

                if (!StatusCode.Equals("200"))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (WebException e)
            {
                cReporter.ReportError(e.Message);
                return false;
            }
        }

        private static string readResponseString(HttpWebResponse r)
        {
            StreamReader reader = new StreamReader(r.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            StringBuilder sb = new StringBuilder();
            Char[] read = new Char[256];
            int count = reader.Read(read, 0, 256);
            while (count > 0)
            {
                sb.Append(read, 0, count);
                count = reader.Read(read, 0, 256);
            }
            reader.Close();
            return sb.ToString();
        }
    }
}
