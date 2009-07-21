using System.Collections.Generic;
using Moneyero.Models;

namespace Moneyero.Services.Mock
{
    /// <summary>
    /// Provides a mock implementation of the <see cref="IAccountService"/> interface.
    /// </summary>
    public class AccountServiceMock : IAccountService
    {
        private readonly List<Account> _accounts;

        /// <summary>
        /// Creates a new <see cref="AccountServiceMock"/> instance.
        /// </summary>
        public AccountServiceMock()
        {
            _accounts = CreateAccounts();
        }

        /// <summary>
        /// Retrieves a list of all the available accounts.
        /// </summary>
        /// 
        /// <returns>A list of all the available accounts.</returns>
        public IList<Account> RetrieveAccounts()
        {
            return _accounts;
        }

        /// <summary>
        /// Creates a list of mock accounts.
        /// </summary>
        /// 
        /// <returns>A list of mock accounts</returns>
        private static List<Account> CreateAccounts()
        {
            return new List<Account>
            {
                new Account
                {
                    AccountType = AccountType.CheckingAccount,
                    BankName = "Skandiabanken",
                    Name = "Brukskonto",
                },
                new Account
                {
                    AccountType = AccountType.CheckingAccount,
                    BankName = "Skandiabanken",
                    Name = "Husleiekonto",
                },
                new Account
                {
                    AccountType = AccountType.CheckingAccount,
                    BankName = "Skandiabanken",
                    Name = "Studielånskonto",
                },
                new Account
                {
                    AccountType = AccountType.SavingsAccount,
                    BankName = "Skandiabanken",
                    Name = "Høyrentekonto",
                },
                new Account
                {
                    AccountType = AccountType.SavingsAccount,
                    BankName = "Sparebank 1",
                    Name = "BSU",
                },
                new Account
                {
                    AccountType = AccountType.CreditCardAccount,
                    BankName = "EuroCard",
                    Name = "Kredittkort - jobb",
                },
                new Account
                {
                    AccountType = AccountType.CreditCardAccount,
                    BankName = "EuroCard",
                    Name = "Kredittkort - personlig",
                }
            };
        }
    }
}