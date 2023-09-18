using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Domain.DTO_s.User;
using MediatR;

namespace Assignment.Application.Command.Users
{
    public class CreateUserCommand : IRequest<ReadUserDto>
    {
        public CreateUserDto UserData { get; set; }
    }
}
