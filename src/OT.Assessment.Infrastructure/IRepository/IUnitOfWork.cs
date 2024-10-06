using OT.Assessment.Domain.Entities;
using OT.Assessment.Tester.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Player> Players { get; }
        IRepository<Provider> Providers { get; }
        IRepository<Game> Games { get; }
        IRepository<CasinoWager> Wagers { get; }

        void Commit();
        void Dispose();
    }
}
