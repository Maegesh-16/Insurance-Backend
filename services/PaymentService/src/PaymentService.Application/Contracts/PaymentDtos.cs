using System.ComponentModel.DataAnnotations;

namespace PaymentService.Application.Contracts;

public sealed record PaymentDto(Guid PaymentId, Guid PolicyId, decimal Amount, string Method, string Status, DateTime PaymentDate);
public sealed record CreatePaymentRequest(
	Guid PolicyId,
	[Range(typeof(decimal), "0.01", "1000000000")] decimal Amount,
	[Required, MaxLength(20)] string Method,
	[Required, MaxLength(30)] string Status,
	DateTime PaymentDate);

public sealed record PaymentTransactionDto(Guid TransactionId, Guid PaymentId, string GatewayRef, string Status);
public sealed record CreatePaymentTransactionRequest(
	Guid PaymentId,
	[Required, MaxLength(120)] string GatewayRef,
	[Required, MaxLength(30)] string Status);

public sealed record RefundDto(Guid RefundId, Guid PaymentId, decimal Amount, string Status, DateTime RefundDate);
public sealed record CreateRefundRequest(
	Guid PaymentId,
	[Range(typeof(decimal), "0.01", "1000000000")] decimal Amount,
	[Required, MaxLength(30)] string Status,
	DateTime RefundDate);

public sealed record ReceiptDto(Guid ReceiptId, Guid PaymentId, string ReceiptNumber, DateTime GeneratedDate);
public sealed record CreateReceiptRequest(
	Guid PaymentId,
	[Required, MaxLength(80)] string ReceiptNumber,
	DateTime GeneratedDate);
