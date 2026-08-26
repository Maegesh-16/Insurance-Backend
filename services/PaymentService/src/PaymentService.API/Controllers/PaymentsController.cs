using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Contracts;

namespace PaymentService.API.Controllers;

[ApiController]
[Route("api/payments")]

public class PaymentsController(IPaymentManagementService paymentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PaymentDto>>> GetPayments([FromQuery] Guid? policyId, CancellationToken cancellationToken)
    {
        return Ok(await paymentService.GetPaymentsAsync(policyId, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Finance")]
    public async Task<ActionResult<PaymentDto>> CreatePayment([FromBody] CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var result = await paymentService.CreatePaymentAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetPayments), new { policyId = result.PolicyId }, result);
    }

    [HttpGet("transactions")]
    public async Task<ActionResult<IReadOnlyCollection<PaymentTransactionDto>>> GetTransactions([FromQuery] Guid? paymentId, CancellationToken cancellationToken)
    {
        return Ok(await paymentService.GetTransactionsAsync(paymentId, cancellationToken));
    }

    [HttpPost("transactions")]
    [Authorize(Roles = "Admin,Finance")]
    public async Task<ActionResult<PaymentTransactionDto>> CreateTransaction([FromBody] CreatePaymentTransactionRequest request, CancellationToken cancellationToken)
    {
        var result = await paymentService.CreateTransactionAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetTransactions), new { paymentId = result.PaymentId }, result);
    }

    [HttpGet("refunds")]
    public async Task<ActionResult<IReadOnlyCollection<RefundDto>>> GetRefunds([FromQuery] Guid? paymentId, CancellationToken cancellationToken)
    {
        return Ok(await paymentService.GetRefundsAsync(paymentId, cancellationToken));
    }

    [HttpPost("refunds")]
    [Authorize(Roles = "Admin,Finance")]
    public async Task<ActionResult<RefundDto>> CreateRefund([FromBody] CreateRefundRequest request, CancellationToken cancellationToken)
    {
        var result = await paymentService.CreateRefundAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetRefunds), new { paymentId = result.PaymentId }, result);
    }

    [HttpGet("receipts")]
    public async Task<ActionResult<IReadOnlyCollection<ReceiptDto>>> GetReceipts([FromQuery] Guid? paymentId, CancellationToken cancellationToken)
    {
        return Ok(await paymentService.GetReceiptsAsync(paymentId, cancellationToken));
    }

    [HttpPost("receipts")]
    [Authorize(Roles = "Admin,Finance")]
    public async Task<ActionResult<ReceiptDto>> CreateReceipt([FromBody] CreateReceiptRequest request, CancellationToken cancellationToken)
    {
        var result = await paymentService.CreateReceiptAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetReceipts), new { paymentId = result.PaymentId }, result);
    }

    [HttpGet("health")]
    [AllowAnonymous]
    public IActionResult Health() => Ok(new { status = "Payment service is running" });
}
