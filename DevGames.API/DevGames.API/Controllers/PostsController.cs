using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var posts = _context.Posts.Where(p =>p.BoardId == id);

            if(posts == null)
                return NotFound();

            return Ok(posts);
        }


        [HttpGet("{postId}")]
        public IActionResult GetById(int id, int postId)
        {
            var post = _context.Posts.
                                 Include(p => p.Comments).
                                 SingleOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }


        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            var post = new Post(model.Title, model.Description, id);

            _context.Posts.Add(post);

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = post.Id, postId = post.Id}, post);
        }


        [HttpPut("{postId}/comments")]
        public IActionResult PostComment(int id, int postId, AddCommentInputModel model)
        {
            var postExists = _context.Posts.Any(b => b.Id == postId);
            if (!postExists)
                return NotFound();

            var comment = new Comment(model.Title, model.Description, model.User, postId);
           
            _context.Comments.Add(comment);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
