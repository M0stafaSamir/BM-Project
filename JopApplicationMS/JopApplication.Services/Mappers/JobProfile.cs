using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JopApplication.Core.DTOs.Job;
using JopApplication.Core.Models;

namespace JopApplication.Services.Mappers
{
    public class JobProfile:Profile
    {
        public JobProfile()
        {
            CreateMap<CreateJobDto, Job>(); 

            CreateMap<Job, GetJobDto>()
            .ForMember(dest => dest.CreatedByName,
               opt => opt.MapFrom(src => src.CreatedBy.UserName))
            .ForMember(dest => dest.Applied,
               opt => opt.MapFrom(src => src.Applications.Count()));

            CreateMap<UpdateJobDto, Job>();

        }

        //dsdsds




    }
}
