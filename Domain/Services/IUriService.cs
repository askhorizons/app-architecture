using API.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
