using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaBank.Domain.Repositories
{
    public interface IBankAccountRepository
    {
        void Add(BankAccount account);
        void Remove(BankAccount account);
        void Update(BankAccount account);
        List<BankAccount> GetAll();
    }
}
