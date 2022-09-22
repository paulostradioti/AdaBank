using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaBank.Domain.Exceptions
{
    public class NotEnoughBalanceException : Exception
    {
        public NotEnoughBalanceException()
        {   
        }

        public NotEnoughBalanceException(string message) 
            : base(message)
        {
            
        }
        public NotEnoughBalanceException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}
