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

        public PlayerController(IPlayersService playerService)
        {
            _playerService = playerService;
        }

        //POST api/player/casinowager
        [HttpPost("casinowager")]
        public async Task<IActionResult> AddCasinoWager(AddCasionWagerDTO addCasionWagerDTO)
        {
          _playerService.AddCasinoWager(addCasionWagerDTO);
          return Ok();
        }

        //GET api/player/{playerId}/wagers
        [HttpGet("{playerId}/wagers")]
        public async Task<IActionResult> AddCasinoWager(string playerId,int pageSize,int page)
        {
            if(page < 1)
                return BadRequest("page should be >= 1");

            if(pageSize < 1)
                return BadRequest("pageSize should be >= 1");

            return Ok(await _playerService.GetPlayerWagers(playerId, pageSize, page));
        }

        //GET api/player/topSpenders?count=10
        [HttpGet("topSpenders")]
        public async Task<IActionResult> TopSpenders(int count)
        {
            if (count < 1)
                return BadRequest("count should be >= 1");

            return Ok(await _playerService.TopSpenders(count));
        }
        
    }
}
