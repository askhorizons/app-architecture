using Domain.Entities;
using Domain.Params;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int Id);
        Task<PageList<User>> Get(GetUserParams p);
    }
}
