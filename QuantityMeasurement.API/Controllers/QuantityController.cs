using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // ✅ ADD THIS
using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Model.DTO;

namespace QuantityMeasurement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🔐 PROTECT ALL APIs
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        [HttpPost("compare")]
        public IActionResult Compare([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Compare(request.Q1, request.Q2);
            return Ok(new { result });
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Add(request.Q1, request.Q2, request.TargetUnit);
            return Ok(new { result });
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Subtract(request.Q1, request.Q2, request.TargetUnit);
            return Ok(new { result });
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Divide(request.Q1, request.Q2);
            return Ok(new { result });
        }

        [HttpPost("convert")]
        public IActionResult Convert([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Convert(request.Q1, request.TargetUnit);
            return Ok(new { result });
        }
    }
}