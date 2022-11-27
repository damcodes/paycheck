﻿using System;
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
            CreateMap<JobDtoCreate, Job>();
            CreateMap<Job, JobDto>();

			//paycheck
            CreateMap<Paycheck, PaycheckDto>();
			CreateMap<Paycheck, PaycheckDtoUserWithPaychecks>();
            CreateMap<PaycheckDtoCreate, Paycheck>();

            //workday
            CreateMap<Workday, WorkdayDto>();
        }
	}
}

