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
			CreateMap<User, UserDto>();
			CreateMap<Paycheck, PaycheckDto>();
			CreateMap<Job, JobDto>();
			CreateMap<Workday, WorkdayDto>();
			CreateMap<User, UserDtoWithJobs>();
			CreateMap<User, UserDtoWithPaychecks>();
			CreateMap<User, UserDtoWithWorkdays>();
			CreateMap<UserDtoCreate, User>();
			CreateMap<JobDtoCreate, Job>();
		}
	}
}

