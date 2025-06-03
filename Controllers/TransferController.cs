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
                return BadRequest(new { message = "Falha na transfer�ncia: " + result.ErrorMessage });

            return Ok(new { message = "Transfer�ncia realizada com sucesso.", data = result.Value });
        }
    }
}