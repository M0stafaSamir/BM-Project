using AutoMapper;
using JopApplication.Core.DTOs.Application;
using JopApplication.Core.DTOs.Job;
using JopApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Mappers
{
    public class ApplicationProfile: Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateApplicationDto, Application>();

            CreateMap<Application, GetApplicationDto>()
            .ForMember(dest => dest.Applicant,
            opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.JobTitle,
            opt => opt.MapFrom(src => src.Job.Title));
        }
    }
}
