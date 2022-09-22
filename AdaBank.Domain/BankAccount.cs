using AdaBank.Domain.Exceptions;

namespace AdaBank.Domain
{
    public class BankAccount
    {
        public decimal Balance => balance;
        private decimal balance;

        public BankAccount()
        {
            balance = Decimal.Zero;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountArgumentException("O Valor informado não é válido.", nameof(amount));

            balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountArgumentException("O Valor informado não é válido.", nameof(amount));

            if (amount > balance)
                throw new NotEnoughBalanceException("Saldo Insuficiente.");

            balance -= amount;
        }
    }
}