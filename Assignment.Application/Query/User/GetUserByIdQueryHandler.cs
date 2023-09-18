using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Abstractions;
using Assignment.Domain.DTO_s.User;
using AutoMapper;
using MediatR;

namespace Assignment.Application.Query.User
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ReadUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ReadUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // Implement logic to retrieve a user by ID and map to ReadUserDto
            var user = await _userRepository.GetUserByIdAsync(request.UserId);

            if (user == null)
            {
                return null; // Handle the case where the user is not found
            }

            return _mapper.Map<ReadUserDto>(user);
        }
    }
}
