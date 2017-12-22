using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace WDtest
{
    [TestFixture]
    public class TestClass
    {
        IWebDriver Browser;
        [Test]
        public void OpenBrowser()
        {
            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("http://www.i.ua/");
        }
        [Test]
        public void VerifyLogin()
        {
            WebDriverWait wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));
            IWebElement elLogin = Browser.FindElement(By.Name("login"));
            elLogin.SendKeys("TestSeleniumWD");
            IWebElement elPass = Browser.FindElement(By.Name("pass"));
            elPass.SendKeys("TestSeleniumWD123");
            IWebElement elEnter = Browser.FindElement(By.CssSelector(".content.clear>form>p>input"));
            elEnter.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//*[@id='header_overall']/div[1]/ul[3]/li[1]/a/span")));
            IWebElement elClassName = Browser.FindElement(By.XPath(".//*[@id='header_overall']/div[1]/ul[3]/li[1]/a/span"));
            Assert.AreEqual("TestSeleniumWD",elClassName.Text);
            

        }
        [Test]
        public void VerifyMail()
        {
            WebDriverWait wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("html/body/div[1]/div[5]/div[1]/div[1]/p/a")));
            IWebElement elMessage = Browser.FindElement(By.XPath("html/body/div[1]/div[5]/div[1]/div[1]/p/a"));
            elMessage.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("to")));
            IWebElement elTo = Browser.FindElement(By.Id("to"));
            elTo.SendKeys("TestSeleniumWD@i.ua");
            IWebElement elSubject = Browser.FindElement(By.XPath("//div[1]/div/form/div[5]/div[2]/span/input[1]"));
            elSubject.SendKeys("TestSubject");
            IWebElement elText = Browser.FindElement(By.Id("text"));
            elText.SendKeys("TestTetx12345");
            IWebElement elSend = Browser.FindElement(By.Name("send"));
            elSend.Click();
            //Thread.Sleep(10000);//задержка для гарантированной отправки
            //wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("new")));
            IWebElement elCurrent = Browser.FindElement(By.ClassName("new"));
            elCurrent.Click();
            Thread.Sleep(2000);
            IWebElement elSelect = Browser.FindElement(By.XPath(".//*[@id='mesgList']/form/div[1]/a/span[2]"));
            elSelect.Click();
            IWebElement elMsgSubject = Browser.FindElement(By.XPath("//div[2]/ul/li/div[1]/div/div[2]/h3"));
            IWebElement elAutor = Browser.FindElement(By.ClassName("black"));
            IWebElement elBody = Browser.FindElement(By.ClassName("message_body"));
            Assert.AreEqual("TestSubject", elMsgSubject.Text);
            Assert.AreEqual("TestSeleniumWD@i.ua", elAutor.Text);
            Assert.AreEqual("TestTetx12345", elBody.Text);
            Browser.Navigate().GoToUrl("http://www.i.ua/");
            IWebElement elLogout = Browser.FindElement(By.LinkText("Выход"));
            elLogout.Click();

        }
        [Test]
        public void VerifyNotLogin()
        {
            WebDriverWait wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));
            IWebElement elLogin = Browser.FindElement(By.Name("login"));
            elLogin.SendKeys("TestSeleniumWD");
            IWebElement elPass = Browser.FindElement(By.Name("pass"));
            elPass.SendKeys("TestSelenium");
            IWebElement elEnter = Browser.FindElement(By.CssSelector(".content.clear>form>p>input"));
            elEnter.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//*[@id='lform_errCtrl']/div[1]")));
            IWebElement elNotValid = Browser.FindElement(By.XPath(".//*[@id='lform_errCtrl']/div[1]"));
            Assert.AreEqual("Неверный логин или пароль",elNotValid.Text);

        }
    }
}
