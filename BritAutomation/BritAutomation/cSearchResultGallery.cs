using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BritAutomation
{   
    public class cSearchResultGallery
    {
        private By SearchGalleryElement = By.ClassName("tileSection3");
        private By SortComboElement_Arrow = By.ClassName("caret");
        private By SortComboElement_ElementsList = By.XPath("//div[contains(@class,'dropdown-menu')]/ul[contains(@class,'dropdown-menu')]");
        
        public void Verify_SearchResultGallery_Loaded(bool Expected)
        {
            try
            {
                bool flgFound = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(SearchGalleryElement)).Displayed;

                if (Expected)
                {
                    if (flgFound)
                    {
                        cReporter.ReportMessage("Search Gallery loaded");
                    }
                    else
                    {
                        cReporter.ReportError("Search Gallery not loaded");
                    }
                }
                else
                {
                    cReporter.ReportError("Search Gallery loaded");
                }
            }
            catch (Exception e)
            {
                if (e is WebDriverTimeoutException)
                {
                    if (Expected)
                    {
                        cReporter.ReportError("Search Gallery not loaded");
                    }
                    else
                    {
                        cReporter.ReportMessage("Search Gallery not loaded");
                    }
                }
                else
                {
                    cReporter.ReportError(e.Message);
                }
            }
        }

        public void Select_SortSearchResults_Combobox(String SortRule)
        {
            try
            {
                cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(SortComboElement_Arrow)).Click();

                cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(SortComboElement_ElementsList));

                cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'dropdown-menu')]/ul[contains(@class,'dropdown-menu')]/li/a/span[contains(text(),'" + SortRule.Trim()+"')]"))).Click();
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }

        public void Click_ItemAddToBasket_Button_ByIndex(int index)
        {
            try
            {
                Thread.Sleep(2000);

                IWebElement item = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//ul[@id='mainProductGrid']/li[" + index + "]")));
                                
                Actions action = new Actions(cCommon.driver);
                action.MoveToElement(item).Perform();
                                               
                cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//ul[@id='mainProductGrid']/li["+index+ "]//button[contains(@class,'js-add-to-cart')]"))).Click();
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }
    }
}
