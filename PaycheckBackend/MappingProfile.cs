using System;
using AutoMapper;
using PaycheckBackend.Models;
using PaycheckBackend.Models.Dto;

namespace PaycheckBackend
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//user
			CreateMap<User, UserDto>();
			CreateMap<User, UserDtoWithJobs>();
			CreateMap<User, UserDtoWithPaychecks>();
			CreateMap<User, UserDtoWithWorkdays>();
            CreateMap<UserDtoCreate, User>();

            //job
            CreateMap<Job, JobDto>();
            CreateMap<JobDtoCreate, Job>();

            //paycheck
            CreateMap<Paycheck, PaycheckDto>();
			CreateMap<Paycheck, PaycheckDtoUserWithPaychecks>();
			CreateMap<Paycheck, PaycheckDtoWithWorkdays>();
            CreateMap<PaycheckDtoCreate, Paycheck>();

            //workday
            CreateMap<Workday, WorkdayDto>();
			CreateMap<WorkdayDtoCreate, Workday>();
        }
	}
}

