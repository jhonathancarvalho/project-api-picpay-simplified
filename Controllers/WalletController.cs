using Microsoft.AspNetCore.Mvc;
using PicPaySimplified.Models.Request;
using PicPaySimplified.Services.Wallet;

namespace PicPaySimplified.Controllers
{
    [ApiController]
    [Route("carteira")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> PostWallet([FromBody] WalletRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    mensagem = "Dados inválidos.",
                    erros = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            var result = await _walletService.ExecuteAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { mensagem = result.ErrorMessage });

            return Created(string.Empty, new { mensagem = "Carteira criada com sucesso." });
        }
    }
}