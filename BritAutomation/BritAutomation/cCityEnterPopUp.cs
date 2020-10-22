using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BritAutomation
{
    public class cCityEnterPopUp
    {
        private By CloseIcon = By.XPath("//div[@id='assortmentModal']//button[@class='btnClose']");
        private By PopUp = By.Id("assortmentModal");

        public void Click_Close_Icon()
        {
            try
            {
                cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(CloseIcon)).Click();
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }

        public void Verify_CityEnterPopUp_Opened(bool Expected)
        {
            bool flgFound = false;

            try
            {
                flgFound = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(PopUp)).Displayed;
                
                if (Expected)
                {
                    if (flgFound)
                    {
                        cReporter.ReportMessage("City Enter PopUp loaded");
                    }
                    else
                    {
                        cReporter.ReportError("City Enter PopUp not loaded");
                    }
                }
                else
                {
                    cReporter.ReportError("City Enter PopUp loaded");
                }
            }
            catch (Exception e)
            {
                if (e is WebDriverTimeoutException)
                {
                    if (Expected)
                    {
                        cReporter.ReportError("City Enter PopUp not loaded");
                    }
                    else
                    {
                        cReporter.ReportMessage("City Enter PopUp not loaded");
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
