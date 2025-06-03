using Microsoft.AspNetCore.Mvc;
using PicPaySimplified.Models.Request;
using PicPaySimplified.Services.Transfer;

namespace PicPaySimplified.Controllers
{
    [ApiController]
    [Route("transferencia")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost]
        public async Task<IActionResult> PostTransfer([FromBody] TransferRequest request)
        {
            var result = await _transferService.ExecuteAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { message = "Falha na transferência: " + result.ErrorMessage });

            return Ok(new { message = "Transferência realizada com sucesso.", data = result.Value });
        }
    }
}