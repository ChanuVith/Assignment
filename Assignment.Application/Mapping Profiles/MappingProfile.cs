using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Application.Command.Tickets;
using Assignment.Domain.DTO_s.Project;
using Assignment.Domain.DTO_s.Ticket;
using Assignment.Domain.DTO_s.User;
using Assignment.Domain.Entities;
using AutoMapper;

namespace Assignment.Application.Mapping_Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ReadUserDto>();
            CreateMap<CreateUserDto, User>();

            CreateMap<Project, ReadProjectDto>();
            CreateMap<CreateProjectDto, Project>();

            CreateMap<Ticket, ReadTicketDto>();
            CreateMap<CreateTicketDto, Ticket>();

        }
    }
}
