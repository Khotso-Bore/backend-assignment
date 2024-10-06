using OT.Assessment.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OT.Assessment.Tester.Infrastructure;

public class CasinoWager
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid GameId { get; set; }
    public Guid TransactionId { get; set; }
    public Guid BrandId { get; set; }
    public Guid ExternalReferenceId { get; set; }
    public Guid TransactionTypeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public int NumberOfBets { get; set; }
    public string CountryCode { get; set; }
    public string SessionData { get; set; }
    public long Duration { get; set; }
    
    [ForeignKey("AccounId")]
    public Player Player { get; set; }
    [ForeignKey("GameId")]
    public Game Game { get; set; }
}