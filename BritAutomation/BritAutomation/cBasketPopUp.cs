using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BritAutomation
{
    public class cBasketPopUp
    {
        private By TotalCostElement = By.ClassName("currency");
        private By DeliveryCostElement = By.ClassName("infoSubText");
        private By BasketSingleItemPriceElement = By.XPath("//div[contains(@class,'miglog-cart-summary-prod-wrp')]");

        public void Verify_TotalCostSum_Text_NotEmpty(bool Expected)
        {            
            try
            {
                String FoundSum = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(TotalCostElement)).Text;
                String FoundSumNumber = Regex.Match(FoundSum, @"[0-9]+[.[0-9]+]?").Value;

                if (Expected)
                {
                    if (!FoundSumNumber.Trim().Equals("0.00"))
                    {
                        cReporter.ReportMessage("Total Cost : " + FoundSumNumber);
                    }
                    else
                    {
                        cReporter.ReportError("Total Cost : " + FoundSumNumber);
                    }
                }
                else
                {
                    if (FoundSumNumber.Trim().Equals("0.00"))
                    {
                        cReporter.ReportMessage("Total Cost : " + FoundSumNumber);
                    }
                    else
                    {
                        cReporter.ReportError("Total Cost : " + FoundSumNumber);
                    }
                }
            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }

        public void Verify_TotalCostSum_Text_IsCorrect(bool Expected)
        {
            try
            {
                Double TotalBasketItemsCost = 0.0;

                String FoundTotalSum = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(TotalCostElement)).Text;

                String FoundTotalSumNumber = Regex.Match(FoundTotalSum, @"[0-9]+[.[0-9]+]?").Value;

                String DeliverySum = cCommon.wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(DeliveryCostElement)).Text;

                String DeliverySumNumber = Regex.Match(DeliverySum, @"[0-9]+[.[0-9]+]?").Value;
                                
                IList<IWebElement> BasketItemsPrisez = cCommon.driver.FindElements(BasketSingleItemPriceElement);

                foreach (IWebElement SingleItemPrise in BasketItemsPrisez)
                {                    
                    IWebElement Price = SingleItemPrise.FindElement(By.XPath("//p[contains(@class,'miglog-prod-totalPrize')]/span"));

                    String PriceOnly = Regex.Match(Price.Text.Trim(), @"[0-9]+[.[0-9]+]?").Value;

                    TotalBasketItemsCost = TotalBasketItemsCost + Convert.ToDouble(PriceOnly);
                }

                Double ExpectedTotal = TotalBasketItemsCost + Convert.ToDouble(DeliverySumNumber);

                if (ExpectedTotal == Convert.ToDouble(FoundTotalSumNumber))
                {
                    cReporter.ReportMessage("Found total basket sum : " + FoundTotalSumNumber + " equals to expected : " + ExpectedTotal);
                }
                else
                {
                    cReporter.ReportError("Found total basket sum : " + FoundTotalSumNumber + " not equals to expected : " + ExpectedTotal);
                }

            }
            catch (Exception e)
            {
                cReporter.ReportError(e.Message);
            }
        }
    }
}
