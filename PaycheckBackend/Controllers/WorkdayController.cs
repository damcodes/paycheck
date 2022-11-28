using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaycheckBackend.Models.Dto;
using PaycheckBackend.Logger;
using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using AutoMapper;

namespace PaycheckBackend.Controllers
{
    [Route("api/workday")]
    [ApiController]
    public class WorkdayController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILoggerManager _logger;

        public WorkdayController(IRepositoryWrapper repository, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetWorkdayById")]
        public IActionResult GetWorkdayById(int id)
        {
            try
            {
                var workday = _repository.Workday.GetWorkdayById(id);

                if (workday == null)
                {
                    _logger.LogError("WorkdayController", "GetWorkdayById", $"Workday with {{ id: {id} }} not found");
                    return NotFound();
                }

                var workdayResult = _mapper.Map<WorkdayDto>(workday);
                _logger.LogInfo("WorkdayController", "GetWorkdayById", $"Workday with {{ id: {id} }} found and returned");
                return Ok(workdayResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("WorkdayController", "GetWorkdayById", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateWorkday([FromBody]WorkdayDtoCreate workday)
        {
            try
            {
                if (workday is null)
                {
                    _logger.LogError("WorkdayController", "CreateWorkday", "Workday object sent from client is null");
                    return BadRequest("Workday object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("WorkdayController", "CreateWorkday", "Workday object sent from client is invalid");
                    return BadRequest("Workday object is invalid");
                }

                var workdayToCreate = _mapper.Map<Workday>(workday);
                Paycheck? paycheck = _repository.Paycheck.GetPaycheckById(workdayToCreate.PaycheckId);
                Job? job = _repository.Job.GetJobById(workdayToCreate.JobId);

                _repository.Workday.CreateWorkday(workdayToCreate, job);
                _logger.LogInfo("WorkdayController", "CreateWorkday", $"Updating paycheck amount {{ id: {paycheck.PaycheckId}, amount: {paycheck.Amount} }}");
                _repository.Paycheck.CalculateAndAdjustPaycheckAmount(paycheck, workdayToCreate);
                _repository.Save();
                 
                var newWorkday = _mapper.Map<WorkdayDto>(workdayToCreate);
                _logger.LogInfo("WorkdayController", "CreateWorkday", $"Finished updating paycheck amount {{ id: {paycheck.PaycheckId}, amount: {paycheck.Amount} }}");
                _logger.LogInfo("WorkdayController", "CreateWorkday", $"New workday created with {{ id: {newWorkday.WorkdayId} }} and paycheck amount adjusted");
                return CreatedAtRoute("GetWorkdayById", new { id = newWorkday.WorkdayId }, newWorkday);
            }
            catch (Exception ex)
            {
                _logger.LogError("WorkdayController", "CreateWorkday", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

