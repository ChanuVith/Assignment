using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.DTO_s.User;
using MediatR;

namespace Assignment.Application.Query.User
{
    public class GetUserByIdQuery : IRequest<ReadUserDto>
    {
        public int UserId { get; set; }
    }
}
