using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Report;
using System.Security.Claims;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService) 
        {
            _reportService = reportService;
        }

        [HttpGet("report")]
        [Authorize(Roles = "6")]
        public async Task<ActionResult<List<SystemReportDTO>>> GetAllSystemReport()
        {
            try
            {
                var allReport = await _reportService.GetAllSystemReports();
                return Ok(allReport);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("report/{id:int}")]
        [Authorize]
        public async Task<ActionResult<SystemReportDetailDTO>> GetSystemReportById(int id)
        {
            try
            {
                var Report = await _reportService.GetSystemReportById(id);
                return Ok(Report);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("reportby/{id:int}")]
        [Authorize(Roles = "5")]
        public async Task<ActionResult<List<SystemReportDTO>>> GetSystemReportByUserId(int id)
        {
            try
            {
                var Report = await _reportService.GetAllSystemReportsByUserId(id);
                return Ok(Report);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "5")]
        public async Task<ActionResult<bool>> CreateSystemReport([FromBody]SystemReportDTO model)
        {
            try
            {
                var Report = await _reportService.CreateSystemReport(model);
                return Ok(Report);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("reply")]
        [Authorize(Roles = "6")]
        public async Task<ActionResult<bool>> ReplySystemReport([FromBody] ReplySystemReportDTO model)
        {
            try
            {
                var Report = await _reportService.ReplySystemReport(model);
                return Ok(Report);
            }
            catch
            {
                return StatusCode(500);
            }
        }




    }
}
