using AdaBank.Domain.Exceptions;
using AdaBank.Domain.Repository;

namespace AdaBank.Domain
{
    public class BankAccount
    {
        private readonly IBankAccountRepository _repository;
        public decimal Balance => balance;
        private decimal balance;

        public BankAccount(IBankAccountRepository repository)
        {
            _repository = repository;
            balance = Decimal.Zero;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountArgumentException("O Valor informado não é válido.", nameof(amount));

            balance += amount;
            _repository.UpdateAccount(this);
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountArgumentException("O Valor informado não é válido.", nameof(amount));

            if (amount > balance)
                throw new NotEnoughBalanceException("Saldo Insuficiente.");

            balance -= amount;
            _repository.UpdateAccount(this);
        }
    }
}