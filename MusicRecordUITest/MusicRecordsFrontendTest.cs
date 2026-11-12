using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            options.AddArgument("--headless");
            IWebDriver driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

            IWebElement getAllButton = driver.FindElement(By.Id("get-records-btn"));

            // Act
            getAllButton.Click();

            Thread.Sleep(1000); // Wait for the table to be updated

            // Assert
            IWebElement recordsTable = driver.FindElement(By.Id("records-table"));
            Assert.IsNotNull(recordsTable);
            driver.Quit();

        }
    }
}
