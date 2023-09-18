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
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<ReadUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<ReadUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            // Implement query logic using _userRepository and map to ReadUserDto
            var users = await _userRepository.GetUsersAsync(request.CustomerId, request.ProjectId);
            return _mapper.Map<List<ReadUserDto>>(users);
        }
    }
}
