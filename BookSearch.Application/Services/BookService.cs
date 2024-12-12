using BookSearch.Application.DTOs;
using BookSearch.Application.Interfaces;
using System.Text.Json;

namespace BookSearch.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        private readonly HttpClient _http;

        public BookService(IBookRepository repo)
        {
            _repo = repo;
            _http = new HttpClient();
        }

        public async Task UpsertBooksFromExternalApi()
        {
            var response = await _http.GetStringAsync("https://openlibrary.org/search.json?q=lord+of+the+rings");
            var json = JsonDocument.Parse(response);
            var docs = json.RootElement.GetProperty("docs");
            var list = new List<BookDto>();
            foreach (var d in docs.EnumerateArray())
            {
                var dto = new BookDto
                {
                    Title = d.TryGetProperty("title", out var t) ? t.GetString() : null,
                    AuthorName = d.TryGetProperty("author_name", out var a) && a.GetArrayLength() > 0 ? a[0].GetString() : null,
                    FirstPublishYear = d.TryGetProperty("first_publish_year", out var y) ? y.GetInt32() : (int?)null
                };
                list.Add(dto);
            }
            _repo.UpsertBooks(list);
        }

        public IEnumerable<BookDto> GetBooks()
        {
            return _repo.GetAllBooks();
        }
    }
}
