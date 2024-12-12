using BookSearch.Application.Interfaces;
using BookSearch.Application.DTOs;
using BookSearch.Core.Entities;

namespace BookSearch.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;
        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            return _context.Books.Select(x => new BookDto
            {
                Title = x.Title,
                AuthorName = x.AuthorName,
                FirstPublishYear = x.FirstPublishYear
            }).ToList();
        }

        public void UpsertBooks(IEnumerable<BookDto> books)
        {
            var existing = _context.Books.ToList();
            foreach (var b in books)
            {
                var entity = existing.FirstOrDefault(e => e.Title == b.Title && e.AuthorName == b.AuthorName);
                if (entity == null)
                {
                    _context.Books.Add(new Book
                    {
                        Title = b.Title,
                        AuthorName = b.AuthorName,
                        FirstPublishYear = b.FirstPublishYear
                    });
                }
                else
                {
                    entity.FirstPublishYear = b.FirstPublishYear;
                }
            }
            _context.SaveChanges();
        }
    }
}
