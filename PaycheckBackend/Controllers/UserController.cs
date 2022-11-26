using Microsoft.AspNetCore.Mvc;
using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using System;
using AutoMapper;
using PaycheckBackend.Models.Dto;
using PaycheckBackend.Logger;

namespace PaycheckBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILoggerManager _logger;

        public UserController(IRepositoryWrapper repository, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers();
                var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);

                _logger.LogInfo($"Returned all {usersResult.Count()} users");
                return Ok(usersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UserController.GetAllUsers--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);

                if (user == null)
                {
                    _logger.LogError($"User with {{id: {id}}} not found");
                    return NotFound();
                }

                _logger.LogInfo($"User with {{id: {id}}} found and returned");
                var userResult = _mapper.Map<UserDto>(user);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UserController.GetUseById--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/jobs")]
        public IActionResult GetUserByIdWithJobs(int id)
        {
            try
            {
                var user = _repository.User.GetUserByIdWithJobs(id);

                if (user == null)
                {
                    _logger.LogError($"User with {{id: {id}}} not found");
                    return NotFound();
                }

                _logger.LogInfo($"User with {{id: {id}}} found with {user.Jobs.Count()} jobs");
                var userResult = _mapper.Map<UserDtoWithJobs>(user);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UserController.GetUserByIdWithJobs--Message: {ex.Message}");
                return StatusCode(500, "Intenal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserDtoCreate user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("New user object sent form client is null");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("New user object sent from client is invalid");
                    return BadRequest("User object is invalid");
                }

                var userToCreate = _mapper.Map<User>(user);

                _repository.User.CreateUser(userToCreate);
                _repository.Save();

                var newUser = _mapper.Map<UserDto>(userToCreate);
                _logger.LogInfo($"New user created {{email: {newUser.Email}, name: {newUser.FirstName + " " + newUser.LastName}}}");
                return CreatedAtRoute("UserById", new { id = newUser.UserId }, newUser);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UserController.CreateUser--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}