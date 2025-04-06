using Art_WebAPI.Models;
using Art_WebAPI.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Art_WebAPI.Controllers
{

    [Authorize] // Apply authorization globally to the controller
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ArtController : ControllerBase
    {
        private readonly IArtRepo _mock_repo;

        public ArtController(IArtRepo repo)
        {
            _mock_repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var articles = _mock_repo.GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var article = _mock_repo.GetById(id);
            if (article == null)
            {
                return NotFound(new { Message = "Article not found" });
            }
            return Ok(article);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Art article)
        {
            // Validate the model 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var a = new Art();
            article.CloneTo(a);
            _mock_repo.Add(a);
            return CreatedAtAction(nameof(GetById), new { id = a.Id }, a);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Art article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid article data" });
            }
            var existingArticle = _mock_repo.GetById(id);
            if (existingArticle == null)
            {
                return NotFound(new { Message = "Article not found" });
            }
            article.CloneTo(existingArticle);
            _mock_repo.Update(existingArticle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var article = _mock_repo.GetById(id);
            if (article == null)
            {
                return NotFound(new { Message = "Article not found" });
            }
            _mock_repo.Delete(id);
            return NoContent();
        }

    }
}
