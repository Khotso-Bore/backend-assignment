using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OT.Assessment.BLL.IServices;
using OT.Assessment.Infrastructure.DTO;
using OT.Assessment.Infrastructure.IRepository;
using OT.Assessment.Infrastructure.RabbitMQ;
using OT.Assessment.Tester.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.BLL.Services
{
    public class PlayersService : IPlayersService
    {
        public IUnitOfWork _unitOfWork;
        public IRabbitMQProducer _rabbitMQProducer;

        public PlayersService(IUnitOfWork unitOfWork, IRabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public Task AddCasinoWager(AddCasionWagerDTO addCasionWagerDTO)
        {
            _rabbitMQProducer.Publish(addCasionWagerDTO);
            return Task.CompletedTask;
        }

        public Task AddBulk(List<AddCasionWagerDTO> addCasionWagerDTOs)
        {
            _rabbitMQProducer.Publish(addCasionWagerDTOs);
            return Task.CompletedTask;
        }

        public async Task<PlayerWagersDTO> GetPlayerWagers(string playerId, int pageSize, int page)
        {
            var skip = (page - 1) * pageSize;
            var wages = await _unitOfWork.Wagers
                .GetSet()
                .Where(x => x.AccountId.Equals(Guid.Parse(playerId)))
                .Skip(skip)
                .Take(pageSize)
                .Select(x => new WagerDTO
                {
                    WagerId = x.WagerId,
                    Amount = x.Amount,
                    CreatedDate = x.CreatedDateTime,
                    Game = x.GameName,
                    Provider = x.Provider
                })
                .ToListAsync();
            
            var totalCount = _unitOfWork.Wagers.GetSet().Where(x => x.AccountId.Equals(Guid.Parse(playerId))).Count();
            var totalPages = (int)Math.Ceiling(totalCount / (decimal)pageSize);

            return new PlayerWagersDTO
            {
                Data = wages,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Total = totalCount,
            };

        }

        public async Task<List<Spender>> TopSpenders(int count)
        {
            /*var topSpenders = await _unitOfWork.Wagers.GetSet()
                .GroupBy(x => x.AccountId)
                .Select(g => new SpenderDTO
                {
                    AccountId = g.Key,
                    Username = g.Select(x => x.Username).FirstOrDefault(),
                    TotalAmountSpend = g.Sum(x => x.Amount),
                })
                .OrderByDescending(x => x.TotalAmountSpend)
                .Take(count)
                .ToListAsync();*/

            var topSpenders = await _unitOfWork.GetContext()
                .Spenders
                .FromSqlRaw("EXEC TopSpenders @Count = {0}", count)
                .ToListAsync();

            return topSpenders;

        }
           
    }
}
