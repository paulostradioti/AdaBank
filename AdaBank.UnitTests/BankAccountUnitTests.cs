using AdaBank.Domain;
using AdaBank.Domain.Exceptions;
using AdaBank.Domain.Repository;
using Moq;
using Range = Moq.Range;

namespace AdaBank.UnitTests
{
    public class BankAccountUnitTests
    {
        private BankAccount _sut;
        private readonly MockRepository _mockRepository;
        private readonly Mock<IBankAccountRepository> _bankAccountRepositoryMock;

        public BankAccountUnitTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            
            _bankAccountRepositoryMock = _mockRepository
                .Create<IBankAccountRepository>();
            _bankAccountRepositoryMock
                .Setup(x => x.UpdateAccount(It.IsAny<BankAccount>()))
                .Verifiable();

            _sut = new BankAccount(_bankAccountRepositoryMock.Object);
        }

        [Fact]
        public void Constructor_Initializes_ZeroBalance()
        {
            //Assert
            Assert.Equal(Decimal.Zero, _sut.Balance);
        }

        [Fact]
        public void Deposit_InvalidAmount_ShouldThrowInvalidAmountException()
        {
            Action depositar = () => _sut.Deposit(-1);

            Assert.Throws<InvalidAmountArgumentException>(depositar);
        }

        [Fact]
        public void Deposit_ValidAmount_ShouldUpdateBalance()
        {
            //Act
            _sut.Deposit(1000);

            //Assert
            Assert.Equal(1000, _sut.Balance);
            _bankAccountRepositoryMock
                .Verify(x => x.UpdateAccount(It.IsAny<BankAccount>()), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Withdraw_InvalidAmount_ShouldThrowInvalidException(decimal amount)
        {
            //Act && Assert
            Action saque = () => _sut.Withdraw(amount);

            Assert.Throws<InvalidAmountArgumentException>(saque);
        }

        [Fact]
        public void Withdraw_NotEnoughBalance_ShouldThrowNotEnoughBalanceException()
        {
            //Act && Assert
            Action saque = () => _sut.Withdraw(5);

            Assert.Throws<NotEnoughBalanceException>(saque);
        }


        [Fact]
        public void Withdraw_ValidAmountWithEnoughBalance_ShouldUpdateBalance()
        {
            // Arrange
            _sut.Deposit(100);

            //Act
            _sut.Withdraw(5);

            //Assert
            Assert.Equal(95, _sut.Balance);
        }


    }
}