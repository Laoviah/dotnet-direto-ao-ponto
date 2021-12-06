using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly DevGameContext _context;
        private readonly IMapper _mapper;

        public BoardsController(DevGameContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Boards);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var board = _context.Boards.FirstOrDefault(x => x.Id == id);

            if (board == null)
                return NotFound();

            return Ok(board);
        }

        [HttpPost]
        public IActionResult Post(AddBoardInputModel model)
        {
            var board = _mapper.Map<Board>(model);

            _context.Boards.Add(board);

            _context.SaveChanges();

            //location api/[controller]/id
            return CreatedAtAction("GetById", new { id = board.Id }, model) ;
        }

        [HttpPut]
        public IActionResult Put(int id, UpdateBoardInputModel model)
        {
            var board = _context.Boards.FirstOrDefault(x => x.Id == id);

            if (board == null)
                return NotFound();

            board.Update(model.Description, model.Rules);
           
            _context.SaveChanges();

            return NoContent();
        }

        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }


    }
}
