using Microsoft.AspNetCore.Mvc;
using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using System;
using AutoMapper;
using PaycheckBackend.Models.Dto;
using PaycheckBackend.Logger;

namespace PaycheckBackend.Controllers
{
    [Route("api/user")]
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

                _logger.LogInfo("UserController", "GetAllUsers", $"Returned all {usersResult.Count()} users");
                return Ok(usersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController", "GetAllUsers", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);

                if (user == null)
                {
                    _logger.LogError("UserController", "GetUserById", $"User with {{id: {id}}} not found");
                    return NotFound();
                }

                _logger.LogInfo("UserController", "GetUserById", $"User with {{id: {id}}} found and returned");
                var userResult = _mapper.Map<UserDto>(user);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController", "GetUserById", $"Error occured--Message: {ex.Message}");
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
                    _logger.LogError("UserController", "GetUserByIdWithJobs", $"User with {{id: {id}}} not found");
                    return NotFound();
                }

                _logger.LogInfo("UserController", "GetUserByIdWithJobs", $"User with {{id: {id}}} found with {user.Jobs.Count()} jobs");
                var userResult = _mapper.Map<UserDtoWithJobs>(user);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController", "GetUserByIdWithJobs", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Intenal server error");
            }
        }

        [HttpGet("{id}/paychecks")]
        public IActionResult GetUserByIdWithPaychecks(int id)
        {
            try
            {
                var user = _repository.User.GetUserByIdWithPaychecks(id);

                if (user == null)
                {
                    _logger.LogError("UserController", "GetUserByIdWithPaychecks", $"User with {{id: {id}}} not found");
                    return NotFound();
                }

                _logger.LogInfo("UserController", "GetUserByIdWithPaychecks", $"User with {{id: {id}}} found with {user.Paychecks.Count()} paychecks");
                var userResult = _mapper.Map<UserDtoWithPaychecks>(user);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController", "GetUserByIdWithPaychecks", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/workdays")]
        public IActionResult GetUserByIdWithWorkdays(int id)
        {
            try
            {
                var user = _repository.User.GetUserByIdWithWorkdays(id);

                if (user == null)
                {
                    _logger.LogError("UserController", "GetUserByIdWithWorkdays", $"User with {{id: {id}}} not found");
                    return NotFound();
                }

                _logger.LogInfo("UserController", "GetUserByIdWithWorkdays", $"User with {{id: {id}}} found with {user.Workdays.Count()} workdays");
                var userResult = _mapper.Map<UserDtoWithWorkdays>(user);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController", "GetUserByIdWithWorkdays", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserDtoCreate user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("UserController", "CreateUser", "New user object sent form client is null");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("UserController", "CreateUser", "New user object sent from client is invalid");
                    return BadRequest("User object is invalid");
                }

                var userToCreate = _mapper.Map<User>(user);

                _repository.User.CreateUser(userToCreate);
                _repository.Save();

                var newUser = _mapper.Map<UserDto>(userToCreate);
                _logger.LogInfo("UserController", "CreateUser", $"New user created {{email: {newUser.Email}, name: {newUser.FirstName + " " + newUser.LastName}}}");
                return CreatedAtRoute("GetUserById", new { id = newUser.UserId }, newUser);

            }
            catch (Exception ex)
            {
                _logger.LogError("UserController", "CreateUser", $"Error occured--Message: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, [FromBody]UserDtoPatch user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("UserController", "PatchUser", "User object sent from client is null");
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("UserController", "PatchUser", "User object sent from client is invalid");
                    return BadRequest();
                }

                var userToPatch = _repository.User.GetUserById(id);
                if (userToPatch is null)
                {
                    _logger.LogError("UserController", "PatchUser", $"User with {{ id: {id} }} not found");
                    return NotFound();
                }

                _logger.LogInfo("UserController", "PatchUser", $"Updating user {{ id: {userToPatch.UserId}, name: {userToPatch.FirstName + " " + userToPatch.LastName}, email: {userToPatch.Email} }}");
                _mapper.Map(user, userToPatch);
                User userPatched = _repository.User.PatchUser(userToPatch);
                _repository.Save();
                _logger.LogInfo("UserController", "PatchUser", $"Updated user {{ id: {userToPatch.UserId}, name: {userToPatch.FirstName + " " + userToPatch.LastName}, email: {userToPatch.Email} }}");

                var userResult = _mapper.Map<UserDto>(userPatched);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController", "PatchUser", $"Error occurred--Message: {ex.Message}");
                return StatusCode(500, $"Internal server error");
            }
        }
    }
}