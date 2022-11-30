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
    [Route("api/job")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {

        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILoggerManager _logger;

        public JobController(IRepositoryWrapper repository, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllJobs()
        {
            try
            {
                var jobs = _repository.Job.GetAllJobs();
                var jobsResult = _mapper.Map<IEnumerable<JobDto>>(jobs);

                _logger.LogInfo("JobController", "GetAllJobs", $"Returned all {jobsResult.Count()} jobs");
                return Ok(jobsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("JobController", "GetAllJobs", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetJobById")]
        public IActionResult GetJobById(int id)
        {
            try
            {
                var job = _repository.Job.GetJobById(id);

                if (job == null)
                {
                    _logger.LogError("JobController", "GetJobById", $"Job with {{id: {id}}} not found");
                    return NotFound();
                }

                var jobResult = _mapper.Map<JobDto>(job);

                _logger.LogInfo("JobController", "GetJobById", $"Job with {{id: {id}}} found and returned");
                return Ok(jobResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("JobController", "GetJobById", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateJob([FromBody]JobDtoCreate job)
        {
            try
            {
                if (job is null)
                {
                    _logger.LogError("JobController", "CreateJob", "Job object sent from client is null");
                    return BadRequest("Job object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("JobController", "CreateJob", "Job object sent from client is invalid");
                    return BadRequest("Job object is invalid");
                }

                var jobToCreate = _mapper.Map<Job>(job);

                _repository.Job.CreateJob(jobToCreate);
                _repository.Save();

                var newJob = _mapper.Map<JobDto>(jobToCreate);
                _logger.LogInfo("JobController", "CreateJob", $"New job created {{company: {newJob.Company}, userId: {newJob.UserId}}}");
                return CreatedAtRoute("GetJobById", new { id = newJob.JobId }, newJob);
            }
            catch (Exception ex)
            {
                _logger.LogError("JobController", "CreateJob", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

