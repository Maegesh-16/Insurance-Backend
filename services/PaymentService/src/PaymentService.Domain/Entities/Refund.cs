namespace PaymentService.Domain.Entities;

public class Refund
{
    public Guid RefundId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = "Requested";
    public DateTime RefundDate { get; set; }
}
