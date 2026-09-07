namespace Domain;

public class Activity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public required string Description { get; set; }
    public required string Category { get; set; }
    public bool IsCancelled { get; set; } = false;
    public required string City { get; set; }
    public required string Venue { get; set; }
    public required string Latitude { get; set; }
    public required string Longitude { get; set; }
}
