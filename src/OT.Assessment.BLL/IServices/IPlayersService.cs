using OT.Assessment.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.BLL.IServices
{
    public interface IPlayersService
    {
        Task AddCasinoWager(AddCasionWagerDTO addCasionWagerDTO);
        Task<PlayerWagersDTO> GetPlayerWagers(string playerId, int pageSize, int page);
        Task<List<SpenderDTO>> TopSpenders(int count);
    }
}
