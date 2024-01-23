using BackendIotvos.Data.Context;
using BackendIotvos.Repository.Interfaces;

namespace BackendIotvos.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IoTvosContext _context;

        public UnitOfWork(IoTvosContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Rollback()
        {

        }
    }
}
