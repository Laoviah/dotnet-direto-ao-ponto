using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{

    [ApiController]
    [Route("api/boards/{id}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly DevGameContext _context;

        public PostsController(DevGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(int id)
        {
            var board = _context.Boards.SingleOrDefault(b => b.Id == id);

            if(board == null)
                return NotFound();

            return Ok(board.Posts);
        }


        [HttpGet("{postId}")]
        public IActionResult GetById(int id, int postId)
        {
            var board = _context.Boards.SingleOrDefault(b => b.Id == id);

            if (board == null)
                return NotFound();

            var post = board.Posts.SingleOrDefault(p => p.Id == postId);
            if (post == null)
                return NoContent();

            return Ok(post);
        }


        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            var board = _context.Boards.SingleOrDefault(b => b.Id == id);
            if (board == null)
                return NotFound();

            var post = new Post(model.Id, model.Title, model.Description);

            board.Posts.Add(post);

            return CreatedAtAction(nameof(GetById), new {id = id, postId = post.Id}, post);
        }


        [HttpPut("{postId}/comments")]
        public IActionResult PostComment(int id, int postId, AddCommentInputModel model)
        {
            var board = _context.Boards.SingleOrDefault(b => b.Id == id);
            if (board == null)
                return NotFound();

            var post = board.Posts.SingleOrDefault(p => p.Id == postId);
            if (post == null)
                return NoContent();

            var comment = new Comment(model.Title, model.Description, model.User);

            post.AddComment(comment);

            return NoContent();
        }
    }
}
