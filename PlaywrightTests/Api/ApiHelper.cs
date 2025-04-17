using Microsoft.Playwright;

namespace PlaywrightTests.Api
{
    public static class ApiHelper
    {
        public static async Task<IAPIRequestContext> CreateApiContextAsync(IPlaywright playwright)
        {
            return await playwright.APIRequest.NewContextAsync();
        }

        public static async Task<IAPIResponse> PostRegistrationVerificationAsync(IAPIRequestContext context, string email, string password)
        {
            var response = await context.PostAsync("https://automationexercise.com/api/verifyLogin", new APIRequestContextOptions
            {
                DataObject = new
                {
                    email = email,
                    password = password
                }
            });

            return response;
        }
    }
}
