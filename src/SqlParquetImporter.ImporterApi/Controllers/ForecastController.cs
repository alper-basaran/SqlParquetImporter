using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlParquetImporter.Domain.Interfaces.Services;

namespace SqlParquetImporter.ImporterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastController : ControllerBase
    {

        private readonly ILogger<ForecastController> _logger;
        private readonly IForecastImportService _importService;

        public ForecastController(ILogger<ForecastController> logger, IForecastImportService importService)
        {
            _logger = logger;
            _importService = importService;
        }

        [HttpPost]
        public IActionResult Post(int pageSize)
        {
            var result = _importService.ImportMissingForecasts(pageSize);
            return Ok();
        }
    }
}
