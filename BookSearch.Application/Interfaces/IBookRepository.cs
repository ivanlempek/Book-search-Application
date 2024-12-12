namespace BookSearch.Application.Interfaces
{
    public interface IBookRepository
    {
        void UpsertBooks(IEnumerable<DTOs.BookDto> books);
        IEnumerable<DTOs.BookDto> GetAllBooks();
    }
}
