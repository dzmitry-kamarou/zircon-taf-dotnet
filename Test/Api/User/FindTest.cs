using FluentAssertions;
using System.Threading.Tasks;
using Api.Service;
using Model.Account;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test.Api.User;

public class FindTest : BaseApiTest
{
    private readonly UserApiService _service;

    public FindTest()
    {
        _service = ServiceProvider.GetService<UserApiService>();
    }

    [Fact, Trait("TestCase", "C13")]
    public async Task RegisteredUserCanBeFound()
    {
        var account = AccountFactory.RegisteredAccount();
        var found = await _service.FindAccount(account);
        var reason = $"Registered '{account.Email}' account was found";
        found.Email.Should().Be(account.Email, reason);
    }

    [Fact, Trait("TestCase", "C14")]
    public async Task NotRegisteredUserCantBeFound()
    {
        var account = AccountFactory.RandomAccount();
        var found = await _service.FindAccount(account);
        var reason = $"Not registered '{account.Email}' account was not found";
        found.Should().BeNull(reason);
    }
}