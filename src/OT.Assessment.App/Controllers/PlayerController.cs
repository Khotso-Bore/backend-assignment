using Microsoft.AspNetCore.Mvc;
using OT.Assessment.BLL.IServices;
using OT.Assessment.Infrastructure.DTO;
namespace OT.Assessment.App.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayersService _playerService;
        private int batchSize = 500;
        private Object thisLock = new Object();
        private static Queue<AddCasionWagerDTO> _casionWagerDTOs = new Queue<AddCasionWagerDTO> ();
        //private readonly Lock _balanceLock = new();

        public PlayerController(IPlayersService playerService)
        {
            _playerService = playerService;
        }

        //POST api/player/casinowager
        [HttpPost("CasinoWager")]
        public async Task<IActionResult> AddCasinoWager(AddCasionWagerDTO addCasionWagerDTO)
        {

            _casionWagerDTOs.Enqueue(addCasionWagerDTO);

            lock (thisLock)
            {
                if (_casionWagerDTOs.Count >= batchSize)
                {

                    List<AddCasionWagerDTO> addCasionWagerDTOs = new List<AddCasionWagerDTO>();
                    for (int i = 0; i < batchSize; i++)
                    {
                        addCasionWagerDTOs.Add(_casionWagerDTOs.Dequeue());

                    }

                    _playerService.AddBulk(addCasionWagerDTOs);
                }
            }


            return Ok();
        }



        //GET api/player/{playerId}/wagers
        [HttpGet("{playerId}/Wagers")]
        public async Task<IActionResult> AddCasinoWager(string playerId,int pageSize,int page)
        {
            if(page < 1)
                return BadRequest("page should be >= 1");

            if(pageSize < 1)
                return BadRequest("pageSize should be >= 1");

            return Ok(await _playerService.GetPlayerWagers(playerId, pageSize, page));
        }

        //GET api/player/topSpenders?count=10
        [HttpGet("TopSpenders")]
        public async Task<IActionResult> TopSpenders(int count)
        {
            if (count < 1)
                return BadRequest("count should be >= 1");

            return Ok(await _playerService.TopSpenders(count));
        }
        
    }
}
