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

            // Login first
            IWebElement usernameInput = driver.FindElement(By.Id("usernameInput"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("loginBtn"));
            usernameInput.SendKeys("user");
            passwordInput.SendKeys("user");
            loginButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("logoutBtn")).Displayed);
            // Wait for the actual element you need, and until it's clickable
            IWebElement getAllButton = wait.Until(d =>
            {
                var el = d.FindElement(By.Id("getRecordsBtn"));
                return (el.Displayed && el.Enabled) ? el : null;
            });

            // Act
            getAllButton.Click();

            // Assert: wait for the table to render after the async axios call
            IWebElement recordsTable = wait.Until(d =>
            {
                var el = d.FindElement(By.Id("recordsTable"));
                return el.Displayed ? el : null;
            });
            Assert.IsNotNull(recordsTable);

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

            // Login first
            IWebElement usernameInput = driver.FindElement(By.Id("usernameInput"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("loginBtn"));
            usernameInput.SendKeys("user");
            passwordInput.SendKeys("user");
            loginButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("logoutBtn")).Displayed);
            // Wait for the actual element you need, and until it's clickable
            IWebElement getAllButton = wait.Until(d =>
            {
                var el = d.FindElement(By.Id("getRecordsBtn"));
                return (el.Displayed && el.Enabled) ? el : null;
            });

            IWebElement titleInput = driver.FindElement(By.Id("search-title"));

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

        [TestMethod]
        public void GetAllRecords_GetAllRecords_NoLogin()
        {
            // Arrange
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Wait for the actual element you need, and until it's clickable
            IWebElement getAllButton = wait.Until(d =>
            {
                var el = d.FindElement(By.Id("getRecordsBtn"));
                return (el.Displayed && el.Enabled) ? el : null;
            });

            // Act
            getAllButton.Click();

            // Assert
            Assert.ThrowsExactly<NoSuchElementException>(() => driver.FindElement(By.Id("recordsTable")));
        }
    }
}