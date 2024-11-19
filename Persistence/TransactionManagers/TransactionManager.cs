using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.TransactionManagers
{
    public class TransactionManager<TContext> : ITransactionManager where TContext :DbContext 
    {
        protected TContext _context;

        public TransactionManager(TContext context)
        {
            _context = context;
        }
        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Gönderilen işlemi çalıştırıyoruz
                    await action();

                    // Eğer her şey başarılı olursa, transaction'ı commit ediyoruz
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Eğer hata oluşursa rollback yapıyoruz
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Transaction başarısız oldu: {ex.Message}");
                    throw;  // Hata tekrar fırlatılıyor
                }
            }
        }
    }
}
