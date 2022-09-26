using AdaBank.Domain.Exceptions;
using AdaBank.Domain.Repositories;

namespace AdaBank.Domain
{
    public class BankAccount
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        public decimal Balance => balance;
        private decimal balance;

        public BankAccount(IBankAccountRepository bankAccountRepository, decimal initialBalance = Decimal.Zero)
        {
            _bankAccountRepository = bankAccountRepository;
            balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountArgumentException("O Valor informado não é válido.", nameof(amount));

            balance += amount;
            _bankAccountRepository.Update(this);
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountArgumentException("O Valor informado não é válido.", nameof(amount));

            if (amount > balance)
                throw new NotEnoughBalanceException("Saldo Insuficiente.");

            balance -= amount;
            _bankAccountRepository.Update(this);
        }
    }
}