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

namespace PaycheckBackend.Controllers
{
    [Route("api/paycheck")]
    [ApiController]
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
    }
}

