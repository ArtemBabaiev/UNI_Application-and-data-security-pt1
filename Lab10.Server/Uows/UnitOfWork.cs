using Lab10.Server.Repositories;

namespace Lab10.Server.Uows
{

    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ITimeoutRepository TimeoutRepository { get; }
        Task SaveChangesAsync();
    }

    public class UnitOfWork: IUnitOfWork
    {
        protected readonly Lab10Context databaseContext;

        public IUserRepository UserRepository { get; }
        public ITimeoutRepository TimeoutRepository { get; }

        public UnitOfWork(Lab10Context databaseContext, IUserRepository userRepository, ITimeoutRepository timeoutRepository)
        {
            this.databaseContext = databaseContext;
            UserRepository = userRepository;
            TimeoutRepository = timeoutRepository;
        }

        public async Task SaveChangesAsync()
        {
            await databaseContext.SaveChangesAsync();
        }
    }
}
