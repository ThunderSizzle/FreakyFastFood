using Owin;

namespace FFF
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseSignInCookies();

            // Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
			    clientId: "0000000040105C6D",
			    clientSecret: "oWTp0B9uZsmz2JXeLBgzLeCkmgCGuhqq");

            app.UseTwitterAuthentication(
			   consumerKey: "ufILPAEGVqBMzTDjjk2WUA",
			   consumerSecret: "FynEIIZ2Yyk8WBTP11gd9Vh4dogxoF9JSwmDr65bsEw" );

            app.UseFacebookAuthentication(
			   appId: "578923398838715",
			   appSecret: "ea9e0826873f376dc242f0d187d4461b");

            app.UseGoogleAuthentication();
        }
    }
}