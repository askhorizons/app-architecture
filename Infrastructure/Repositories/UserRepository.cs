using API.Domain.Filters;
using API.Domain.Wrappers;
using Domain.Entities;
using Domain.Repositories;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Get(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var pagedData = await _context.Users
                                        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                        .Take(validFilter.PageSize)
                                        .ToListAsync();

            var totalRecords = await _context.Users.CountAsync();
            return pagedData;
        }

        public async Task<User> GetById(int Id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == Id);
        }
    }
}
