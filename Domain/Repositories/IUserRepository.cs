using API.Domain.Filters;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int Id);
        Task<List<User>> Get(PaginationFilter filter);
    }
}
