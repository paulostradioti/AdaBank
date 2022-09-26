using AdaBank.Domain;
using AdaBank.Domain.Exceptions;
using AdaBank.Domain.Repositories;
using Moq;

namespace AdaBank.UnitTests
{
    public class BankAccountUnitTests
    {
        private BankAccount sut;
        private MockRepository mockRepository;
        private Mock<IBankAccountRepository> bankAccountRepositoryMock;

        public BankAccountUnitTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            
            bankAccountRepositoryMock = mockRepository.Create<IBankAccountRepository>();
            bankAccountRepositoryMock.Setup(x => x.Update(It.IsAny<BankAccount>())).Verifiable();

            sut = new BankAccount(bankAccountRepositoryMock.Object);
        }

        [Fact]
        public void Constructor_Initializes_ZeroBalance()
        {
            //Assert
            Assert.Equal(Decimal.Zero, sut.Balance);
        }

        [Fact]
        public void Deposit_InvalidAmount_ShouldThrowInvalidAmountException()
        {
            Action depositar = () => sut.Deposit(-1);

            Assert.Throws<InvalidAmountArgumentException>(depositar);
        }

        [Fact]
        public void Deposit_ValidAmount_ShouldUpdateBalance()
        {
            //Act
            sut.Deposit(1000);

            //Assert
            Assert.Equal(1000, sut.Balance);
            bankAccountRepositoryMock.Verify(x => x.Update(It.IsAny<BankAccount>()), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Withdraw_InvalidAmount_ShouldThrowInvalidException(decimal amount)
        {
            //Act && Assert
            Action saque = () => sut.Withdraw(amount);

            Assert.Throws<InvalidAmountArgumentException>(saque);
        }

        [Fact]
        public void Withdraw_NotEnoughBalance_ShouldThrowNotEnoughBalanceException()
        {
            //Act && Assert
            Action saque = () => sut.Withdraw(5);

            Assert.Throws<NotEnoughBalanceException>(saque);
        }


        [Fact]
        public void Withdraw_ValidAmountWithEnoughBalance_ShouldUpdateBalance()
        {
            sut = new BankAccount(bankAccountRepositoryMock.Object, 100);

            //Act
            sut.Withdraw(5);

            //Assert
            Assert.Equal(95, sut.Balance);
            bankAccountRepositoryMock.Verify(x => x.Update(It.IsAny<BankAccount>()), Times.Once);
        }
    }
}