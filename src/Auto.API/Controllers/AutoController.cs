﻿using Auto.API.Models;
using Auto.BizLogic.Models.Dto;
using Auto.BizLogic.Services;
using Auto.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Auto.API.Controllers
{
    [Route("api/autos")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private readonly IAutoService _autoService;
        private readonly ILogger<AutoController> _logger;

        public AutoController(ILogger<AutoController> logger, IAutoService autoService)
        {
            _autoService = autoService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AutoDto>), 201)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<ApiResponse<AutoDto>>> CreateAuto(CreateAutoDto createAutoDto)
        {
            var auto = await _autoService.CreateAuto(createAutoDto);

            var url = Url.Action(nameof(GetAutoById), new { id = auto.AutoId });

            return Created(url, new ApiResponse<AutoDto>(auto, message: "Auto created successfully"));
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AutoDto>>), 200)]
        public async Task<ActionResult<ApiResponse<IEnumerable<AutoDto>>>> SearchAutos([FromQuery] AutoSearchDto autoSearchDto)
        {
            var autos = await _autoService.SearchAutos(autoSearchDto);

            return Ok(new ApiResponse<IEnumerable<AutoDto>>(autos, message: "Autos retrieved successfully"));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AutoDto>>), 200)]
        public async Task<ActionResult<ApiResponse<IEnumerable<AutoDto>>>> GetAutos()
        {
            var autos = await _autoService.GetAllAutos();

            return Ok(new ApiResponse<IEnumerable<AutoDto>>(autos, message: "Autos retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AutoDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<ActionResult<ApiResponse<AutoDto>>> GetAutoById(int id)
        {
            var auto = await _autoService.GetAutoById(id);

            if (auto == null)
            {
                return NotFound(new ApiResponse<string>("Auto not found", success: false));
            }

            return Ok(new ApiResponse<AutoDto>(auto, message: "Auto retrieved successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<ApiResponse<string>>> DeleteUserById(int id)
        {
            var result = await _autoService.DeleteAutoByIdAsync(id);

            if (result == false)
            {
                return NotFound(new ApiResponse<string>("Auto not found", success: false));
            }

            return NoContent();
        }
    }
}