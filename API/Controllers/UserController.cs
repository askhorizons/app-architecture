using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Filters;
using API.Domain.Wrappers;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Infrastructure;
using Microsoft.AspNetCore.Http;
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
        private readonly AppDbContext _context;
        private readonly IUriService _uriService;

        public UserController(IUserRepository userRepository, IMapper mapper,
            AppDbContext context, IUriService uriService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Users
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Users.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserDto>(
                _mapper.Map<List<UserDto>>(pagedData), validFilter, totalRecords, _uriService, route);

            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.Message = "Resource found";

            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var temp = await _userRepository.GetById(id);
            if (temp == null)
            {
                return NotFound(new Response<UserDto>(null)
                {
                    Message = $"User with id {id} no more exists",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }


            return Ok(new Response<UserDto>(_mapper.Map<UserDto>(temp))
            {
                Message = "Resource found",
                StatusCode = StatusCodes.Status200OK
            });
        }
    }
}
