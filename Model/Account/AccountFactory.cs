using Bogus;
using Commons;

namespace Model.Account
{
    public static class AccountFactory
    {
        public static Account RegisteredUser()
        {
            return new Account
            {
                Id = BusinessConfig.RegisteredUserId,
                Email = BusinessConfig.RegisteredUserEmail,
                Password = BusinessConfig.RegisteredUserPassword
            };
        }

        public static Account RandomAccount()
        {
            return new Faker<Account>()
                .RuleFor(p => p.Email, f => f.Internet.Email())
                .RuleFor(p => p.Password, f => f.Internet.Password())
                .Generate();
        }
    }
}