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
        public async Task FullUserRegistrationAndDeletionFlow()
        {
            string name = DataGenerator.GenerateRandomName();
            string email = DataGenerator.GenerateRandomEmail();
            string password = DataGenerator.GenerateRandomPassword();

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

            // API Verification
            await VerifyRegistrationData_VerifyLoginAPI(email, password);
            
            // UI Deletion Steps
            await _registrationPage.ClickDeleteAccountAsync();
            Assert.IsTrue(await _registrationPage.IsAccountDeletedAsync(), "Account deletion failed.");
        }

        public async Task VerifyRegistrationData_VerifyLoginAPI(string email, string password)
        {
            var apiRequestContext = await _playwright.APIRequest.NewContextAsync();
            var response = await apiRequestContext.PostAsync("https://automationexercise.com/api/verifyLogin", new APIRequestContextOptions
            {
                DataObject = new
                {
                    email = email,
                    password = password
                }
            });
            Assert.That(response.Status, Is.EqualTo(200), $"Expected status code 200 but got a different value.");
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
