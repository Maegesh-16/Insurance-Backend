namespace PaymentService.Application.Contracts;

public interface IPaymentManagementService
{
    Task<IReadOnlyCollection<PaymentDto>> GetPaymentsAsync(Guid? policyId, CancellationToken cancellationToken);
    Task<PaymentDto> CreatePaymentAsync(CreatePaymentRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<PaymentTransactionDto>> GetTransactionsAsync(Guid? paymentId, CancellationToken cancellationToken);
    Task<PaymentTransactionDto> CreateTransactionAsync(CreatePaymentTransactionRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<RefundDto>> GetRefundsAsync(Guid? paymentId, CancellationToken cancellationToken);
    Task<RefundDto> CreateRefundAsync(CreateRefundRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ReceiptDto>> GetReceiptsAsync(Guid? paymentId, CancellationToken cancellationToken);
    Task<ReceiptDto> CreateReceiptAsync(CreateReceiptRequest request, CancellationToken cancellationToken);
}
