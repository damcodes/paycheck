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
		}
	}
}

