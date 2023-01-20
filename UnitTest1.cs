namespace storm103;

// Generated by Selenium IDE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using Xunit;
using System.Drawing.Imaging;

public class SuiteTests : IDisposable
{
    public IWebDriver driver { get; private set; }
    public IDictionary<String, Object> vars { get; private set; }
    public IJavaScriptExecutor js { get; private set; }

    private string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"\imagesresult";

    public SuiteTests()
    {
        driver = new ChromeDriver();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<String, Object>();
        if (Directory.Exists(imagePath))
        {
            Directory.Delete(imagePath, true);            
        }
        Directory.CreateDirectory(imagePath);
    }
    public void Dispose()
    {
        driver.Quit();
    }
    [Fact]
    public void Test01()
    {
        driver.Navigate().GoToUrl("https://demo.identityserver.com/grants");
        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(@"/Account/Login"));
        SaveImage(imagePath + @"\screen01.png");

        driver.FindElement(By.Id("Username")).SendKeys(Environment.GetEnvironmentVariable("username"));
        driver.FindElement(By.Id("Password")).SendKeys(Environment.GetEnvironmentVariable("password"));
        SaveImage(imagePath + @"\screen02.png");
        driver.FindElement(By.Name("button")).Click();   

        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("/grants"));
        SaveImage(imagePath + @"\screen03.png");
    }

    private void SaveImage(string filepath)
    {
        ITakesScreenshot? ssdriver = driver as ITakesScreenshot;
        if(ssdriver != null)
        {
            Screenshot screenshot = ssdriver.GetScreenshot();
            Screenshot tempImage = screenshot;
            tempImage.SaveAsFile(filepath, ScreenshotImageFormat.Png);
        }        
    }
}
