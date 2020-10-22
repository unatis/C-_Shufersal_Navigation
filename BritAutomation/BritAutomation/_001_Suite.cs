using NUnit.Framework;
using System.Threading;

namespace BritAutomation
{
    public class _001_Suite
    {
        cHeader header = new cHeader();
        cLoginPopUp LoginPopUp = new cLoginPopUp();
        cSearchResultGallery SearchResultGallery = new cSearchResultGallery();
        cCityEnterPopUp CityEnterPopUp = new cCityEnterPopUp();
        cBasketPopUp BasketPopUp = new cBasketPopUp();

        [SetUp]
        public void Setup()
        {
            cCommon.LaunchBrowser(BrowserNames.Chrome);
            cCommon.NavigateTo("https://www.shufersal.co.il");
        }

        [Test]
        public void _001_VerifyLogoImage()
        {
            header.Verify_Logo_Image(true);            
        }

        [Test]
        public void _002_VerifyLoggedInName()
        {
            header.Click_Login_Button();

            LoginPopUp.Verify_LoginPopUp_Opened(true);

            LoginPopUp.Set_UserName_Text("xxxxxxxxxx");

            LoginPopUp.Set_Password_Text("xxxxxxxxxxx");

            LoginPopUp.Click_Login_Button();

            header.Verify_LoggedInUsername_Text("יורי", true);
        }

        [Test]
        public void _003_SearchItem_AddToBasket_PrintTotalCost()
        {
            header.Set_Search_Text("חלב");

            header.Click_Search_Icon();

            SearchResultGallery.Verify_SearchResultGallery_Loaded(true);

            SearchResultGallery.Select_SortSearchResults_Combobox("נמוך עד גבוה");

            SearchResultGallery.Click_ItemAddToBasket_Button_ByIndex(1);

            CityEnterPopUp.Verify_CityEnterPopUp_Opened(true);

            CityEnterPopUp.Click_Close_Icon();

            cCommon.WaitTime(5);

            BasketPopUp.Verify_TotalCostSum_Text_NotEmpty(true);

            BasketPopUp.Verify_TotalCostSum_Text_IsCorrect(true);
        }

        [TearDown]
        public void CleanUp()
        {
            cCommon.CloseBrowser();
        }
    }
}