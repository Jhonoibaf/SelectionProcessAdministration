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
            CreateMap<CandidateDto, CandidateModel>()
                    .ForMember(dest => dest.InsertDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<CandidateDto, CandidateModel>()
                .ForMember(dest => dest.InsertDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifyDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CandidateExperienceModel, CandidateExperience>();
            CreateMap<CandidateExperienceDto, CandidateExperienceModel>()
                .ForMember(dest => dest.InsertDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<CandidateExperienceDto, CandidateExperienceModel>()
                .ForMember(dest => dest.InsertDate, opt => opt.Ignore())
                .ForMember(dest => dest.InsertDate, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
