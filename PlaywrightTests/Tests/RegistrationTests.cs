using Microsoft.Playwright;
using PlaywrightTests.Pages;
using PlaywrightTests.Utilities;
using PlaywrightTests.Api;

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
            var apiContext = await ApiHelper.CreateApiContextAsync(_playwright);
            var response = await ApiHelper.PostRegistrationVerificationAsync(apiContext, email, password);
            Assert.That(response.Status, Is.EqualTo(200), "Expected 200 OK from verifyLogin API.");
            
            // UI Deletion Steps
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
