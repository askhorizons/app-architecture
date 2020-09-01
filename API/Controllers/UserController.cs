using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Params;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetUserParams param)
        {
            var temp = await _userRepository.Get(param);
            return Ok(_mapper.Map<IEnumerable<UserDto>>(temp));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var temp = await _userRepository.GetById(id);
            return Ok(_mapper.Map<UserDto>(temp));
        }
    }
}
