using System.Threading.Tasks;
using Api.Service;
using FluentAssertions;
using Model.Account;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Api.User;

public class AuthTest : BaseApiTest
{
    private readonly UserApiService _service;

    public AuthTest()
    {
        _service = ServiceProvider.GetService<UserApiService>();
    }

    [Fact, Trait("TestCase", "C11")]
    public async Task LoggedInUserRetrievesToken()
    {
        var account = AccountFactory.RegisteredAccount();
        await _service.LoginAccount(account);
        var token = await _service.AuthAccount(account);
        var reason = $"Token generated for logged in '{account.Email}' account";
        const string jwtPattern = "^[A-Za-z0-9-_=]+\\.[A-Za-z0-9-_=]+\\.?[A-Za-z0-9-_.+/=]*$";
        token.Should().MatchRegex(jwtPattern, reason);
    }

    [Fact, Trait("TestCase", "C12")]
    public async Task NotLoggedInUserNotRetrievesToken()
    {
        var account = AccountFactory.RegisteredAccount();
        var message = await _service.AuthAccount(account);
        var reason = $"Token not generated for not logged in '{account.Email}' account";
        message.Should().Be("Not authorized", reason);
    }
}