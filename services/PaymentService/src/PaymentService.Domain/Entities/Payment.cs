namespace PaymentService.Domain.Entities;

public class Payment
{
    public Guid PaymentId { get; set; }
    public Guid PolicyId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime PaymentDate { get; set; }
}
