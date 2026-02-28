using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace TechMAssignment
{
    public class ParaBankTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();

            // Disable save password popup
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            // Disable autofill (address save popup)
            options.AddUserProfilePreference("autofill.profile_enabled", false);
            options.AddUserProfilePreference("autofill.credit_card_enabled", false);
            // Optional stability settings
            options.AddArgument("--disable-notifications");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://parabank.parasoft.com/ ");
        }

        [Test]
        public void EndToEnd_Banking_Workflow_Test()
        {
            // Click Register
            driver.FindElement(By.XPath("//a[text()='Register']")).Click();

            // Fill Registration Form
            driver.FindElement(By.Name("customer.firstName")).SendKeys("Daniel");
            driver.FindElement(By.Name("customer.lastName")).SendKeys("Carter");
            driver.FindElement(By.Name("customer.address.street")).SendKeys("245 Lake View St");
            driver.FindElement(By.Name("customer.address.city")).SendKeys("Austin");
            driver.FindElement(By.Name("customer.address.state")).SendKeys("Texas");
            driver.FindElement(By.Name("customer.address.zipCode")).SendKeys("73301");
            driver.FindElement(By.Name("customer.phoneNumber")).SendKeys("8899776655");
            driver.FindElement(By.Name("customer.ssn")).SendKeys("556677");
            driver.FindElement(By.Name("customer.username")).SendKeys("carter" + DateTime.Now.ToString("mmss"));
            driver.FindElement(By.Name("customer.password")).SendKeys("Bank@1234");
            driver.FindElement(By.Name("repeatedPassword")).SendKeys("Bank@1234");
            driver.FindElement(By.XPath("//input[@value='Register']")).Click();
            Thread.Sleep(50000);

            // Assert Navigation to Account Services
            Assert.IsTrue(driver.PageSource.Contains("Your account was created successfully. You are now logged in."));

            // Open New Savings Account
            driver.FindElement(By.LinkText("Open New Account")).Click();
            SelectElement accountType = new SelectElement(driver.FindElement(By.Id("type")));
            accountType.SelectByText("SAVINGS");

            driver.FindElement(By.XPath("//input[@value='Open New Account']")).Click();

            Assert.IsTrue(driver.PageSource.Contains("Account Opened"));

            // Request Loan - Rejection Scenario
            driver.FindElement(By.LinkText("Request Loan")).Click();
            driver.FindElement(By.Id("amount")).Clear();
            driver.FindElement(By.Id("amount")).SendKeys("10000");
            driver.FindElement(By.Id("downPayment")).Clear();
            driver.FindElement(By.Id("downPayment")).SendKeys("15");
            driver.FindElement(By.XPath("//input[@value='Apply Now']")).Click();

            Assert.IsTrue(driver.PageSource.Contains("Denied"));

            // Request Loan - Approval Scenario
            driver.FindElement(By.LinkText("Request Loan")).Click();
            driver.FindElement(By.Id("amount")).Clear();
            driver.FindElement(By.Id("amount")).SendKeys("1000");
            driver.FindElement(By.Id("downPayment")).Clear();
            driver.FindElement(By.Id("downPayment")).SendKeys("15");
            driver.FindElement(By.XPath("//input[@value='Apply Now']")).Click();

            Assert.IsTrue(driver.PageSource.Contains("Approved"));

            // Navigate to Accounts Overview
            driver.FindElement(By.LinkText("Accounts Overview")).Click();

            string balanceText = driver.FindElement(By.XPath("//*[text()='Total']//parent::td//following-sibling::td/b")).Text;
            string cleanedBalance = balanceText.Replace("$", "").Replace(",", "");
            double balance = Convert.ToDouble(cleanedBalance);

            Assert.Greater(balance, 1000);

            // Logout
            driver.FindElement(By.LinkText("Log Out")).Click();

            Assert.IsTrue(driver.PageSource.Contains("Customer Login"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }

}
