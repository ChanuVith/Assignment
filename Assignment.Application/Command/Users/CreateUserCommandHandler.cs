using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.User;
using Assignment.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Assignment.Application.Command.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ReadUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ReadUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Map CreateUserDto to your domain model, create the user, and map the result to ReadUserDto
            var user = _mapper.Map<User>(request.UserData);

            // Additional validation or business logic can be added here

            var createdUser = await _userRepository.CreateUserAsync(user);
            return _mapper.Map<ReadUserDto>(createdUser);
        }
    }
}
