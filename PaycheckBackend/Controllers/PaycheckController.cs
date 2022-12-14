using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaycheckBackend.Models.Dto;
using PaycheckBackend.Logger;
using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PaycheckBackend.Controllers
{
    [Route("api/paycheck")]
    [ApiController]
    [Authorize]
    public class PaycheckController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILoggerManager _logger;

        public PaycheckController(IRepositoryWrapper repository, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetPaycheckById")]
        public IActionResult GetPaycheckById(int id)
        {
            try
            {
                var paycheck = _repository.Paycheck.GetPaycheckById(id);

                if (paycheck == null)
                {
                    _logger.LogError("PaycheckController", "GetPaycheckById", $"Paycheck with {{id: {id} }} not found");
                    return NotFound();
                }

                var paycheckResult = _mapper.Map<PaycheckDto>(paycheck);

                _logger.LogInfo("PaycheckController", "GetPaycheckById", $"Paycheck with {{id: {id}}} found and returned");
                return Ok(paycheckResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("PaycheckController", "GetPaycheckById", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/workdays", Name = "GetPaycheckByIdWithWorkdays")]
        public IActionResult GetPaycheckByIdWithWorkdays(int id)
        {
            try
            {
                var paycheck = _repository.Paycheck.GetPaycheckByIdWithWorkdays(id);

                if (paycheck == null)
                {
                    _logger.LogError("PaycheckController", "GetPaycheckByIdWithWorkdays", $"Paycheck with {{id: {id} }} not found");
                    return NotFound();
                }

                var paycheckResult = _mapper.Map<PaycheckDtoWithWorkdays>(paycheck);

                _logger.LogInfo("PaycheckController", "GetPaycheckByIdWithWorkdays", $"Paycheck with {{id: {id}}} found with {paycheck.Workdays.Count()} workdays and returned");
                return Ok(paycheckResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("PaycheckController", "GetPaycheckByIdWithWorkdays", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreatePaycheck([FromBody]PaycheckDtoCreate paycheck)
        {
            try
            {
                if (paycheck is null)
                {
                    _logger.LogError("PaycheckController", "CreatePaycheck", "Paycheck object sent from client is null");
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("PaycheckController", "CreatePaycheck", "Paycheck object sent from client is invalid");
                    return BadRequest();
                }

                var paycheckToCreate = _mapper.Map<Paycheck>(paycheck);
                Job? job = _repository.Job.GetJobById(paycheck.JobId);

                _repository.Paycheck.CreatePaycheck(paycheckToCreate, job);
                _repository.Save();

                var newPaycheck = _mapper.Map<PaycheckDto>(paycheckToCreate);
                _logger.LogInfo("PaycheckController", "CreatePaycheck", $"New paycheck created {{ id: {newPaycheck.PaycheckId} }}");
                return CreatedAtRoute("GetPaycheckById", new { id = newPaycheck.PaycheckId }, newPaycheck);
            }
            catch (Exception ex)
            {
                _logger.LogError("PaycheckController", "CreatePaycheck", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/recalculate")]
        public IActionResult RecalculatePaycheck(int id)
        {
            try
            {
                var paycheck = _repository.Paycheck.GetPaycheckByIdWithWorkdays(id);

                if (paycheck == null)
                {
                    _logger.LogError("PaycheckController", "GetPaycheckById", $"Paycheck with {{id: {id} }} not found");
                    return NotFound();
                }

                _logger.LogInfo("PaycheckController", "RecalculatePaycheck", $"Recalculating paycheck amount {{ id: {paycheck.PaycheckId}, amount: {paycheck.Amount} }}");
                _repository.Workday.RecalculateWagesEarned(paycheck.Workdays, paycheck.Job);
                Paycheck paycheckRecalc = _repository.Paycheck.RecalculatePaycheck(paycheck);
                _repository.Save();
                _logger.LogInfo("PaycheckController", "RecalculatePaycheck", $"Finished recalc paycheck amount {{ id: {paycheck.PaycheckId}, amount: {paycheck.Amount} }}");

                var paycheckResult = _mapper.Map<PaycheckDto>(paycheckRecalc);
                return CreatedAtRoute("GetPaycheckById", new { id = paycheckResult.PaycheckId }, paycheckResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("PaycheckController", "RecalculatePaycheck", $"Error occurred--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

