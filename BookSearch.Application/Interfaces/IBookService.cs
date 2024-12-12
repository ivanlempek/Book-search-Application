namespace BookSearch.Application.Interfaces
{
    public interface IBookService
    {
        Task UpsertBooksFromExternalApi();
        IEnumerable<DTOs.BookDto> GetBooks();
    }
}
