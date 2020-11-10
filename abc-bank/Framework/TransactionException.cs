using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Framework
{
    public class TransactionException : Exception
    {
        public TransactionException(string message)
            : base(message)
        {
        }

        public TransactionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }


    public class AccountException : Exception
    {
        public AccountException(string message)
            : base(message)
        {
        }

        public AccountException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
