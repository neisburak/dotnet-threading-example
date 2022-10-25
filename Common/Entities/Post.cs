namespace Common.Entities;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;

    public override string ToString() => $"{Id} - {Title}";
}