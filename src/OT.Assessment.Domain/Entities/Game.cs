namespace OT.Assessment.Tester.Infrastructure;

public class Game
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Theme { get; set; }
    public Guid ProviderId { get; set; }
    public Provider Provider { get; set; }
}