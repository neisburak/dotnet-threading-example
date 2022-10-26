namespace Console.Models;

public class Status
{
    public int ThreadId { get; set; }
    public DateTime Date { get; set; }

    public override string ToString() => $"Thread: {ThreadId}, Date: {Date.ToShortTimeString()}";
}