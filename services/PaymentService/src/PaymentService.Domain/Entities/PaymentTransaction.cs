namespace PaymentService.Domain.Entities;

public class PaymentTransaction
{
    public Guid TransactionId { get; set; }
    public Guid PaymentId { get; set; }
    public string GatewayRef { get; set; } = string.Empty;
    public string Status { get; set; } = "Initiated";
}
