using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BritAutomation
{
    public class cHeader
    {
        private By LogoImageElement = By.XPath("//div[contains(@class,'banner__component')]/a/img");
        private By LoginButtonElement = By.ClassName("btnTrigger");
        private By LoggedInUserElement = By.XPath("//a[@class='info']/strong");
        private By SearchTextElement = By.Id("js-site-search-input");        
        private By SearchIconElement = By.XPath("//button[contains(@class,'js_search_button')]");
        
        public void Click_Login_Button()
        {
            try
            {
                cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(LoginButtonElement)).Click();
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }

        public void Click_Logo_Image()
        {
            try
            {
                cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(LogoImageElement)).Click();                
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }

        public void Set_Search_Text(String SearchText)
        {
            cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(SearchTextElement)).SendKeys(SearchText);
        }

        public void Click_Search_Icon()
        {
            try
            {
                cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(SearchIconElement)).Submit();
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }
        
        public void Verify_Logo_Image(bool Expected)
        {           
            try
            {               
                bool flgFound = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(LogoImageElement)).Displayed;

                if (Expected)
                {
                    if (flgFound)
                    {
                        bool flg200OK = cCommon.SendHTTPRequest("GET", "https://res.cloudinary.com/shufersal/image/upload/f_auto,q_auto/v1551800922/prod/cmscontent/hde/h73/9038553808926", "");

                        if (flg200OK)
                        {
                            cReporter.ReportMessage("Logo image found as expected");
                        }
                        else
                        {
                            cReporter.ReportError("Logo image not found on the server");
                        }
                        
                    }
                    else
                    {
                        cReporter.ReportError("Logo image not found");
                    }
                }
                else
                {                    
                    cReporter.ReportError("Logo image found");
                }               
            }
            catch (Exception e)
            {
                if (e is WebDriverTimeoutException)
                {
                    if (Expected)
                    {
                        cReporter.ReportError("Logo image not found");
                    }
                    else
                    {
                        cReporter.ReportMessage("Logo image not found");
                    }
                }
                else
                {
                    cReporter.ReportError(e.Message);
                }
                
            }
        }

        public void Verify_LoggedInUsername_Text(String ExpectedUserName, bool Expected)
        {
            try
            {
                String FoundName = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(LoggedInUserElement)).Text;

                if (Expected)
                {
                    if (FoundName.Trim().Contains(ExpectedUserName.Trim()))
                    {
                        cReporter.ReportMessage("Expected user name " + ExpectedUserName + " found on page");
                    }
                    else
                    {
                        cReporter.ReportError("Expected user name " + ExpectedUserName + " not found on page");
                    }
                }
                else
                {
                    cReporter.ReportError("Expected user name " + ExpectedUserName + " found on page");
                }
            }
            catch (Exception e)
            {
                if (e is WebDriverTimeoutException)
                {
                    if (Expected)
                    {
                        cReporter.ReportError("Expected user name " + ExpectedUserName + " not found on page");
                    }
                    else
                    {
                        cReporter.ReportMessage("Expected user name " + ExpectedUserName + " not found on page");
                    }
                }
                else
                {
                    cReporter.ReportError(e.Message);
                }
            }
        }
    }
}
