using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OT.Assessment.BLL.IServices;
using OT.Assessment.Infrastructure.DTO;
using OT.Assessment.Infrastructure.IRepository;
using OT.Assessment.Infrastructure.RabbitMQ;
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

        public async Task<PlayerWagersDTO> GetPlayerWagers(string playerId, int pageSize, int page)
        {
            var skip = (page - 1) * pageSize;
            var wages = await _unitOfWork.Wagers
                .GetSet()
                .Where(x => x.AccountId.Equals(playerId))
                .Skip(skip)
                .Take(pageSize)
                .Include(x => x.Game)
                .ThenInclude(g => g.Provider)
                .Select(x => new WagerDTO
                {
                    WagerId = x.Id,
                    Amount = x.Amount,
                    CreatedDate = x.CreatedDateTime,
                    Game = x.Game.Name,
                    Provider = x.Game.Provider.Name
                })
                .ToListAsync();
            
            var totalCount = _unitOfWork.Wagers.GetSet().Count();
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

        public Task<List<SpenderDTO>> TopSpenders(int count)
        {
            var topSpenders = _unitOfWork.Wagers.GetSet()
                .Include(x => x.Player)
                .GroupBy(x => x.AccountId)
                .Select(g => new SpenderDTO
                {
                    AccountId = g.Key,
                    Username = g.Select(x => x.Player.Username).FirstOrDefault(),
                    TotalAmountSpend = g.Sum(x => x.Amount),
                })
                .OrderByDescending(x => x.TotalAmountSpend)
                .Take(count)
                .ToListAsync();

            return topSpenders;
        }
           
    }
}
