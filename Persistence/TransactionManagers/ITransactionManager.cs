using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.TransactionManagers
{
    public interface ITransactionManager
    {
        Task ExecuteInTransactionAsync(Func<Task> action);

    }
}
