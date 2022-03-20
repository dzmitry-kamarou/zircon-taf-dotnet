using System.Threading.Tasks;
using Api.Service;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Model.Account;
using Xunit;

namespace Test.Api.User
{
    public class LoginTest : BaseApiTest
    {
        private readonly UserApiService _service;

        public LoginTest()
        {
            _service = ServiceProvider.GetService<UserApiService>();
        }

        [Fact, Trait("TestCase", "C15")]
        public async Task RegisteredUserRetrievesToken()
        {
            var account = AccountFactory.RegisteredUser();
            var reason = $"Token generated for '{account.Email}' account";
            var token = await _service.LoginAccount(account);
            const string jwtPattern = "^[A-Za-z0-9-_=]+\\.[A-Za-z0-9-_=]+\\.?[A-Za-z0-9-_.+/=]*$";
            token.Should().MatchRegex(jwtPattern, reason);
        }
    }
}