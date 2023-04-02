using Lab10.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Server.Repositories
{
    public interface IUserRepository{
        Task<User> GetByUsernameAsync(String username);
        Task InsertAsync(User user);
    }
    public class UserRepository: IUserRepository
    {
        protected readonly Lab10Context databaseContext;

        protected readonly DbSet<User> table;

        public UserRepository(Lab10Context databaseContext)
        {
            this.databaseContext = databaseContext;
            table = this.databaseContext.Set<User>();
        }

        public async Task<User> GetByUsernameAsync(String username)
        {
            return await table.SingleOrDefaultAsync(user => user.Username == username);
        }

        public async Task InsertAsync(User user)
        {
            await table.AddAsync(user);
        }
    }
}
