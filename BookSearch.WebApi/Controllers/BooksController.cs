using Microsoft.AspNetCore.Mvc;
using BookSearch.Application.Interfaces;

namespace BookSearch.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = _service.GetBooks();
            return Ok(books);
        }

        [HttpPost("force-job")]
        public async Task<IActionResult> ForceJob()
        {
            await _service.UpsertBooksFromExternalApi();
            return Ok("Job executed.");
        }
    }
}
