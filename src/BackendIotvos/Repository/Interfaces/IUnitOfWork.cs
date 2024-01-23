namespace BackendIotvos.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();

        void Rollback();
    }
}
