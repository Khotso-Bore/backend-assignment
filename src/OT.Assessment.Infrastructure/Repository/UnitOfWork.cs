using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Data;
using OT.Assessment.Domain.Entities;
using OT.Assessment.Infrastructure.IRepository;
using OT.Assessment.Tester.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OTDbContext _context;

        public IRepository<Player> Players { get; private set; }
        public IRepository<Provider> Providers { get; private set; }
        public IRepository<CasinoWager> Wagers{ get; private set; }
        public IRepository<Game> Games { get; private set; }

        public UnitOfWork(OTDbContext context)
        {
            _context = context;
            Players = new Repository<Player>(context);
            Providers = new Repository<Provider>(context);
            Wagers = new Repository<CasinoWager>(context);
            Games = new Repository<Game>(context);
        }

        public OTDbContext GetContext()
        {
            return _context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }




    }
}
