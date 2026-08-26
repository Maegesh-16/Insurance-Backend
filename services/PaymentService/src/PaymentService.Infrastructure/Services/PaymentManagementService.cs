using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Entities;
using PaymentService.Infrastructure.Persistence;

namespace PaymentService.Infrastructure.Services;

public class PaymentManagementService(PaymentDbContext dbContext) : IPaymentManagementService
{
    public async Task<IReadOnlyCollection<PaymentDto>> GetPaymentsAsync(Guid? policyId, CancellationToken cancellationToken)
    {
        var query = dbContext.Payments.AsNoTracking();
        if (policyId.HasValue)
        {
            query = query.Where(x => x.PolicyId == policyId.Value);
        }

        return await query
            .OrderByDescending(x => x.PaymentDate)
            .Select(x => new PaymentDto(x.PaymentId, x.PolicyId, x.Amount, x.Method, x.Status, x.PaymentDate))
            .ToListAsync(cancellationToken);
    }

    public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var entity = new Payment
        {
            PaymentId = Guid.NewGuid(),
            PolicyId = request.PolicyId,
            Amount = request.Amount,
            Method = request.Method,
            Status = request.Status,
            PaymentDate = request.PaymentDate
        };

        dbContext.Payments.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new PaymentDto(entity.PaymentId, entity.PolicyId, entity.Amount, entity.Method, entity.Status, entity.PaymentDate);
    }

    public async Task<IReadOnlyCollection<PaymentTransactionDto>> GetTransactionsAsync(Guid? paymentId, CancellationToken cancellationToken)
    {
        var query = dbContext.Transactions.AsNoTracking();
        if (paymentId.HasValue)
        {
            query = query.Where(x => x.PaymentId == paymentId.Value);
        }

        return await query
            .Select(x => new PaymentTransactionDto(x.TransactionId, x.PaymentId, x.GatewayRef, x.Status))
            .ToListAsync(cancellationToken);
    }

    public async Task<PaymentTransactionDto> CreateTransactionAsync(CreatePaymentTransactionRequest request, CancellationToken cancellationToken)
    {
        var entity = new PaymentTransaction
        {
            TransactionId = Guid.NewGuid(),
            PaymentId = request.PaymentId,
            GatewayRef = request.GatewayRef,
            Status = request.Status
        };

        dbContext.Transactions.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new PaymentTransactionDto(entity.TransactionId, entity.PaymentId, entity.GatewayRef, entity.Status);
    }

    public async Task<IReadOnlyCollection<RefundDto>> GetRefundsAsync(Guid? paymentId, CancellationToken cancellationToken)
    {
        var query = dbContext.Refunds.AsNoTracking();
        if (paymentId.HasValue)
        {
            query = query.Where(x => x.PaymentId == paymentId.Value);
        }

        return await query
            .OrderByDescending(x => x.RefundDate)
            .Select(x => new RefundDto(x.RefundId, x.PaymentId, x.Amount, x.Status, x.RefundDate))
            .ToListAsync(cancellationToken);
    }

    public async Task<RefundDto> CreateRefundAsync(CreateRefundRequest request, CancellationToken cancellationToken)
    {
        var entity = new Refund
        {
            RefundId = Guid.NewGuid(),
            PaymentId = request.PaymentId,
            Amount = request.Amount,
            Status = request.Status,
            RefundDate = request.RefundDate
        };

        dbContext.Refunds.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new RefundDto(entity.RefundId, entity.PaymentId, entity.Amount, entity.Status, entity.RefundDate);
    }

    public async Task<IReadOnlyCollection<ReceiptDto>> GetReceiptsAsync(Guid? paymentId, CancellationToken cancellationToken)
    {
        var query = dbContext.Receipts.AsNoTracking();
        if (paymentId.HasValue)
        {
            query = query.Where(x => x.PaymentId == paymentId.Value);
        }

        return await query
            .OrderByDescending(x => x.GeneratedDate)
            .Select(x => new ReceiptDto(x.ReceiptId, x.PaymentId, x.ReceiptNumber, x.GeneratedDate))
            .ToListAsync(cancellationToken);
    }

    public async Task<ReceiptDto> CreateReceiptAsync(CreateReceiptRequest request, CancellationToken cancellationToken)
    {
        var entity = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PaymentId = request.PaymentId,
            ReceiptNumber = request.ReceiptNumber,
            GeneratedDate = request.GeneratedDate
        };

        dbContext.Receipts.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ReceiptDto(entity.ReceiptId, entity.PaymentId, entity.ReceiptNumber, entity.GeneratedDate);
    }
}
