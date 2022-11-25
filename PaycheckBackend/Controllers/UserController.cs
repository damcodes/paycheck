using Microsoft.AspNetCore.Mvc;
using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using System;
using AutoMapper;
using PaycheckBackend.Models.Dto;

namespace PaycheckBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public UserController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        //[Route("allUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers();
                var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(usersResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}