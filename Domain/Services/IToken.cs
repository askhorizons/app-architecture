using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IToken
    {
        string CreateToken(User user);
    }
}
