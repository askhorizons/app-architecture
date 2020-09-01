using Domain.Entities;
using Domain.Params;
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

        public async Task<PageList<User>> Get(GetUserParams p)
        {
            var list = GetList(p);
            return await PageList<User>.CreateAsync(list, p.PageIndex, p.PageSize);
        }

        public async Task<User> GetById(int Id)
        {
            var p = new GetUserParams();
            return await GetList(p).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public IQueryable<User> GetList(GetUserParams param)
        {
            const string ASC = "nameAsc";
            const string DES = "nameDesc";

            var p = _context.Users.AsQueryable();
            p = p.OrderBy(x => x.FirstName);
            
            if (string.IsNullOrEmpty(param.Search) == false)
            {
                p = p.Where(x => x.FirstName.ToLower().Contains(param.Search.ToLower()));
            }
            if (string.IsNullOrEmpty(param.Sort) == false)
            {
                switch (param.Sort)
                {
                    case ASC:
                        p = p.OrderBy(x => x.FirstName);
                        break;
                    case DES:
                        p = p.OrderByDescending(x => x.FirstName);
                        break;
                    default:
                        p = p.OrderBy(x => x.FirstName);
                        break;
                }
            }
            return p;
        }
    }
}
