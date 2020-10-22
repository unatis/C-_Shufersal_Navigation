using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BritAutomation
{
    public class cLoginPopUp
    {
        private By LoginPopUp = By.Id("loginDropdown");
        private By UserNameText = By.Id("j_username");
        private By PasswordText = By.Id("j_password");
        private By LoginButton = By.XPath("//div[@class='bottomSide']/button");

        public void Verify_LoginPopUp_Opened(bool Expected)
        {
            try
            {
                bool flgFound = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(LoginPopUp)).Displayed;

                if (Expected)
                {
                    if (flgFound)
                    {
                        cReporter.ReportMessage("Login PopUp found as expected");
                    }
                    else
                    {
                        cReporter.ReportError("Login PopUp not found");
                    }
                }
                else
                {
                    cReporter.ReportError("Login PopUp found");
                }
            }
            catch (Exception e)
            {
                if (e is WebDriverTimeoutException)
                {
                    if (Expected)
                    {
                        cReporter.ReportError("Login PopUp not found");
                    }
                    else
                    {
                        cReporter.ReportMessage("Login PopUp not found");
                    }
                }
                else
                {
                    cReporter.ReportError(e.Message);
                }
            }
        }

        public void Set_UserName_Text(String UserName)
        {
            cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(UserNameText)).SendKeys(UserName);
        }

        public void Set_Password_Text(String Password)
        {
            cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(PasswordText)).SendKeys(Password);
        }
        
        public void Click_Login_Button()
        {
            cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(LoginButton)).Click();
        }

    }
}
