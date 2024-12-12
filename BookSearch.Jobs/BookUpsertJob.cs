using BookSearch.Application.Interfaces;

namespace BookSearch.Jobs
{
    public class BookUpsertJob
    {
        private readonly IBookService _service;
        public BookUpsertJob(IBookService service)
        {
            _service = service;
        }

        public async Task Run()
        {
            await _service.UpsertBooksFromExternalApi();
        }
    }
}
