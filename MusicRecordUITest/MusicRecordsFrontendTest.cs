using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MusicRecordUITest
{
    [TestClass]
    public sealed class MusicRecordsFrontendTest
    {
        [TestMethod]
        public void GetAllRecords_ButtonClick_ShowAllRecords()
        {
            // Arrange
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            IWebDriver driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

            IWebElement getAllButton = driver.FindElement(By.Id("get-records-btn"));


            // Act
            getAllButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => getAllButton.Displayed && getAllButton.Enabled);

            // Assert
            IWebElement recordsTable = driver.FindElement(By.Id("recordsTable"));
            Assert.IsNotNull(recordsTable);
            driver.Quit();

        }

        [TestMethod]
        public void GetAllRecords_FilteredSearch_ShowFilteredRecords()
        {
            // Arrange
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            IWebDriver driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

            IWebElement titleInput = driver.FindElement(By.Id("search-title"));

            IWebElement getAllButton = driver.FindElement(By.Id("get-records-btn"));

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Act
            titleInput.SendKeys("Back in Black");
            getAllButton.Click();
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("recordsTable")).Displayed);

            // Assert
            IWebElement recordsTable = driver.FindElement(By.Id("recordsTable"));
            Assert.IsNotNull(recordsTable);
            driver.Quit();
        }
    }
}
