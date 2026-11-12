using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MusicRecordUITest
{
    [TestClass]
    public sealed class LoginFrontendTest
    {
        [TestMethod]
        public void Login_ValidCredentials_SuccessfulLogin()
        {
            // Arrange
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            IWebDriver driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");

            IWebElement usernameInput = driver.FindElement(By.Id("usernameInput"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("loginBtn"));

            // Act
            usernameInput.SendKeys("user");
            passwordInput.SendKeys("user");
            loginButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("logoutBtn")).Displayed);

            // Assert
            IWebElement logoutBtn = driver.FindElement(By.Id("logoutBtn"));
            Assert.IsNotNull(logoutBtn);
            driver.Quit();
        }
    }
}
