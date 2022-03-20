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
    }
}