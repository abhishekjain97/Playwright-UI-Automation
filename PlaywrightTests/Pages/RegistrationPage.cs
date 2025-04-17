using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    public class RegistrationPage
    {
        private readonly IPage _page;

        public RegistrationPage(IPage page) => _page = page;

        // Locators
        private ILocator HomeLogo => _page.Locator("img[alt='Website for automation practice']");
        private ILocator SignupLogin => _page.Locator("a[href='/login']");
        private ILocator NewUserSignup => _page.Locator("h2:has-text('New User Signup!')");
        private ILocator NameInput => _page.Locator("input[data-qa='signup-name']");
        private ILocator EmailInput => _page.Locator("input[data-qa='signup-email']");
        private ILocator SignupButton => _page.Locator("button[data-qa='signup-button']");
        private ILocator EnterAccountInfo => _page.Locator("b:has-text('Enter Account Information')");
        private ILocator TitleMr => _page.Locator("#id_gender1");
        private ILocator PasswordInput => _page.Locator("#password");
        private ILocator DaysDropdown => _page.Locator("#days");
        private ILocator MonthsDropdown => _page.Locator("#months");
        private ILocator YearsDropdown => _page.Locator("#years");
        private ILocator NewsletterCheckbox => _page.Locator("#newsletter");
        private ILocator OffersCheckbox => _page.Locator("#optin");
        private ILocator FirstName => _page.Locator("#first_name");
        private ILocator LastName => _page.Locator("#last_name");
        private ILocator Company => _page.Locator("#company");
        private ILocator Address => _page.Locator("#address1");
        private ILocator Address2 => _page.Locator("#address2");
        private ILocator Country => _page.Locator("#country");
        private ILocator State => _page.Locator("#state");
        private ILocator City => _page.Locator("#city");
        private ILocator Zip => _page.Locator("#zipcode");
        private ILocator MobileNumber => _page.Locator("#mobile_number");
        private ILocator CreateAccount => _page.Locator("button[data-qa='create-account']");
        private ILocator AccountCreatedMessage => _page.Locator("b:has-text('Account Created!')");
        private ILocator ContinueButton => _page.Locator("a[data-qa='continue-button']");
        private ILocator LoggedInAs => _page.Locator("a:has-text('Logged in as')");
        private ILocator DeleteAccount => _page.Locator("a[href='/delete_account']");
        private ILocator AccountDeletedMessage => _page.Locator("b:has-text('Account Deleted!')");
        
        public async Task<bool> IsHomePageVisibleAsync() => await HomeLogo.IsVisibleAsync();

        public async Task ClickSignupLoginAsync() => await SignupLogin.ClickAsync();

        public async Task<bool> IsNewUserSignupVisibleAsync() => await NewUserSignup.IsVisibleAsync();

        public async Task EnterNameAndEmailAsync(string name, string email)
        {
            await NameInput.FillAsync(name);
            await EmailInput.FillAsync(email);
        }

        public async Task ClickSignupButtonAsync() => await SignupButton.ClickAsync();

        public async Task<bool> IsEnterAccountInfoVisibleAsync() => await EnterAccountInfo.IsVisibleAsync();

        public async Task FillRegistrationForm(string password)
        {
            await TitleMr.ClickAsync();
            await PasswordInput.FillAsync(password);
            await DaysDropdown.SelectOptionAsync("10");
            await MonthsDropdown.SelectOptionAsync("5");
            await YearsDropdown.SelectOptionAsync("1990");
            await NewsletterCheckbox.CheckAsync();
            await OffersCheckbox.CheckAsync();
            await FirstName.FillAsync("John");
            await LastName.FillAsync("Doe");
            await Company.FillAsync("Omnify");
            await Address.FillAsync("123 Automation Street");
            await Address2.FillAsync("Apt 4B");
            await Country.SelectOptionAsync("Canada");
            await State.FillAsync("Ontario");
            await City.FillAsync("Toronto");
            await Zip.FillAsync("M5J2N8");
            await MobileNumber.FillAsync("1234567890");
        }

        public async Task ClickCreateAccountAsync() => await CreateAccount.ClickAsync();

        public async Task<bool> IsAccountCreatedAsync() => await AccountCreatedMessage.IsVisibleAsync();

        public async Task ClickContinueAsync() => await ContinueButton.ClickAsync();

        public async Task<bool> IsLoggedInAsAsync(string name) => await LoggedInAs.Filter(new() { HasText = name }).IsVisibleAsync();

        public async Task ClickDeleteAccountAsync() => await DeleteAccount.ClickAsync();

        public async Task<bool> IsAccountDeletedAsync() => await AccountDeletedMessage.IsVisibleAsync();
    }
}
