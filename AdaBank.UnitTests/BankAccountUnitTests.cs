using AdaBank.Domain;
using AdaBank.Domain.Exceptions;

namespace AdaBank.UnitTests
{
    public class BankAccountUnitTests
    {
        [Fact]
        public void Constructor_Initializes_ZeroBalance()
        {
            //Arrange  & Act
            var sut = new BankAccount(); // subject under test

            //Assert
            Assert.Equal(Decimal.Zero, sut.Balance);
        }

        [Fact]
        public void Deposit_InvalidAmount_ShouldThrowInvalidAmountException()
        {
            //Arrange
            var sut = new BankAccount();

            Action depositar = () => sut.Deposit(-1);

            Assert.Throws<InvalidAmountArgumentException>(depositar);
        }

        [Fact]
        public void Deposit_ValidAmount_ShouldUpdateBalance()
        {
            //Arrange
            var sut = new BankAccount();

            //Act
            sut.Deposit(1000);

            //Assert
            Assert.Equal(1000, sut.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Withdraw_InvalidAmount_ShouldThrowInvalidException(decimal amount)
        {
            // Arrange
            var sut = new BankAccount();

            //Act && Assert
            Action saque = () => sut.Withdraw(amount);

            Assert.Throws<InvalidAmountArgumentException>(saque);
        }

        [Fact]
        public void Withdraw_NotEnoughBalance_ShouldThrowNotEnoughBalanceException()
        {
            // Arrange
            var sut = new BankAccount();

            //Act && Assert
            Action saque = () => sut.Withdraw(5);

            Assert.Throws<NotEnoughBalanceException>(saque);
        }


        [Fact]
        public void Withdraw_ValidAmountWithEnoughBalance_ShouldUpdateBalance()
        {
            // Arrange
            var sut = new BankAccount();
            sut.Deposit(100);

            //Act
            sut.Withdraw(5);

            //Assert
            Assert.Equal(95, sut.Balance);
        }
    }
}