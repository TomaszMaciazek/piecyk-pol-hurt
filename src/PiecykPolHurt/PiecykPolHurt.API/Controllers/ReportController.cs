using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PiecykPolHurt.API.Authorization;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.ApplicationLogic.Services;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Dto.Report;
using PiecykPolHurt.Model.Queries;

namespace PiecykPolHurt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Authorize(Policy = $"{Policy.Seller},{Policy.Admin},{Policy.Cooperant}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ReportListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedList<ReportListItemDto>>> GetReports([FromQuery] ReportQuery query)
        {
            try
            {
                //do zmiany
                var isAdmin = true;

                return Ok(await _reportService.GetReportsAsync(query, isAdmin));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportDefinitionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReportDefinitionDto>> GetReportDefinition([FromRoute] int id)
        {
            try
            {
                return Ok(await _reportService.GetReportById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("page/{id}")]
        [Authorize(Policy = $"{Policy.Seller},{Policy.Admin},{Policy.Cooperant}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportGenerationPageDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReportGenerationPageDto>> GetReportGenerationPageData([FromRoute] int id)
        {
            try
            {
                return Ok(await _reportService.GetReportPageAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> AddReport([FromBody] CreateReportDefinitionCommand command)
        {
            try
            {
                var result = await _reportService.CreateReportDefinitionAsync(command);
                if (!result)
                {
                    return Conflict();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("generate")]
        [Authorize(Policy = $"{Policy.Seller},{Policy.Admin},{Policy.Cooperant}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GenerateReport([FromBody] GeneratorQuery query)
        {
            try
            {
                var result = await _reportService.GenerateReportAsync(query);
                return File(
                    fileContents: result,
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: $"report_{DateTime.Now.ToString("G")}.xlsx"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> UpdateReport([FromBody] UpdateReportDefinitionCommand command)
        {
            try
            {
                var result = await _reportService.UpdateReportDefinitionAsync(command);
                if (!result)
                {
                    return Conflict();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
