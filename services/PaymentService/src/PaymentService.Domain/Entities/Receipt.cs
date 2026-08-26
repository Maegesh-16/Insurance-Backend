namespace PaymentService.Domain.Entities;

public class Receipt
{
    public Guid ReceiptId { get; set; }
    public Guid PaymentId { get; set; }
    public string ReceiptNumber { get; set; } = string.Empty;
    public DateTime GeneratedDate { get; set; }
}
