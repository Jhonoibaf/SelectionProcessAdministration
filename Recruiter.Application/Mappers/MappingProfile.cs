using AutoMapper;
using Recruiters.Application.DTOs;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Models;

namespace Recruiters.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateModel, Candidate>();
            CreateMap<CandidateDto, Candidate>();

            CreateMap<CandidateDto, CandidateModel>()
            .ForMember(dest => dest.InsertDate,
                       opt => opt.Condition((src, dest) => dest.InsertDate == default(DateTime))) 
            .ForMember(dest => dest.ModifyDate,
                       opt => opt.MapFrom(src => DateTime.Now));


            CreateMap<CandidateExperienceModel, CandidateExperience>();
            CreateMap<CandidateExperienceDto, CandidateExperience>();

            CreateMap<CandidateExperienceDto, CandidateExperienceModel>()
            .ForMember(dest => dest.InsertDate,
                       opt => opt.Condition((src, dest) => dest.InsertDate == default(DateTime)))
            .ForMember(dest => dest.ModifyDate,
                       opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
