namespace BookSearch.Core.Entities;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? AuthorName { get; set; }
    public int? FirstPublishYear { get; set; }
}
