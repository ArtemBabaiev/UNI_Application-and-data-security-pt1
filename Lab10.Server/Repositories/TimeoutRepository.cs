using Lab10.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Server.Repositories
{
    public interface ITimeoutRepository
    {
        Task<LoginTimeout> GetByUsernameAsync(String username);
        Task UpdateAsync(LoginTimeout timeout);
        Task InsertAsync(LoginTimeout timeout);

    }
    public class TimeoutRepository : ITimeoutRepository
    {

        protected readonly Lab10Context databaseContext;

        protected readonly DbSet<LoginTimeout> table;

        public TimeoutRepository(Lab10Context databaseContext)
        {
            this.databaseContext = databaseContext;
            table = this.databaseContext.Set<LoginTimeout>();
        }

        public async Task<LoginTimeout> GetByUsernameAsync(string username)
        {
            return await table.SingleOrDefaultAsync(timeout => timeout.UserUsername == username);
        }

        public async Task UpdateAsync(LoginTimeout timeout)
        {
            await Task.Run(() => table.Update(timeout));
        }

        public async Task InsertAsync(LoginTimeout timeout)
        {
            await table.AddAsync(timeout);
        }
    }
}
