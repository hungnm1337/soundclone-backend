using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Admin.Analysis;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly IAnalysisService _analysisService;
        public AnalysisController(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        [HttpGet("user-registration")]
        [Authorize(Roles = "6")]
        public async Task<ActionResult<List<MonthlyCount>>> GetUserRegistrationCountByMonth()
        {   
                var result = await _analysisService.GetUserRegistrationCountByMonth();
                return Ok(result);   
        }

        [HttpGet("track-upload")]
        [Authorize(Roles = "6")]
        public async Task<ActionResult<List<MonthlyCount>>> GetTrackUploadCountByMonth()
        {
            var result = await _analysisService.GetTrackUploadCountByMonth();
            return Ok(result);
        }

        [HttpGet("invoice-count")]
        [Authorize(Roles = "6")]
        public async Task<ActionResult<List<MonthlyCount>>> GetInvoiceCountByMonth()
        {
            var result = await _analysisService.GetInvoiceCountByMonth();
            return Ok(result);
        }

        [HttpGet("invoice-amount")]
        [Authorize(Roles = "6")]
        public async Task<ActionResult<List<MonthlyAmount>>> GetTotalInvoiceAmountByMonth()
        {
            var result = await _analysisService.GetTotalInvoiceAmountByMonth();
            return Ok(result);
        }
    }
}
