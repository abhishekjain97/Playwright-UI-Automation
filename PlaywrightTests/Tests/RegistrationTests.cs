using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Pages;
using PlaywrightTests.Utilities;
using System.Threading.Tasks;

namespace PlaywrightTests.Tests
{
    public class RegistrationTests : TestBase
    {
        private RegistrationPage _registrationPage;

        [SetUp]
        public void LaunchWebsite()
        {
            _registrationPage = new RegistrationPage(_page);
        }

        [Test]
        [TestCase("John Doe", "johndeo@example.com", "12345678")]
        [TestCase("Zoe Bender", "zoebender@example.com", "12345678")]
        [TestCase("Sarah Leach", "sarahleach@example.com", "12345678")]
        public async Task FullUserRegistrationAndDeletionFlow(string name, string email, string password)
        {
            // string name = DataGenerator.GenerateRandomName();
            // string email = DataGenerator.GenerateRandomEmail();
            // string password = DataGenerator.GenerateRandomPassword();

            Assert.IsTrue(await _registrationPage.IsHomePageVisibleAsync(), "Home page is not visible.");

            await _registrationPage.ClickSignupLoginAsync();
            Assert.IsTrue(await _registrationPage.IsNewUserSignupVisibleAsync(), "New User Signup is not visible.");

            await _registrationPage.EnterNameAndEmailAsync(name, email);
            await _registrationPage.ClickSignupButtonAsync();
            Assert.IsTrue(await _registrationPage.IsEnterAccountInfoVisibleAsync(), "'Enter Account Info' is not visible.");

            await _registrationPage.FillRegistrationForm(password);
            await _registrationPage.ClickCreateAccountAsync();
            Assert.IsTrue(await _registrationPage.IsAccountCreatedAsync(), "Account creation failed.");

            await _registrationPage.ClickContinueAsync();
            Assert.IsTrue(await _registrationPage.IsLoggedInAsAsync(name), $"'Logged in as {name}' is not visible.");

            await _registrationPage.ClickDeleteAccountAsync();
            Assert.IsTrue(await _registrationPage.IsAccountDeletedAsync(), "Account deletion failed.");
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
