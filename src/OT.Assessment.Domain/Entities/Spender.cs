using OT.Assessment.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OT.Assessment.Tester.Infrastructure;

public class Spender
{
    public Guid AccountId { get; set; }
    public string Username { get; set; }
    public decimal TotalAmount { get; set; }
}