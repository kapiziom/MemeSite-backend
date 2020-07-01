using AutoMapper;
using MemeSite.Domain;
using MemeSite.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Api.AutoMapper
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<Comment, CommentVM>()
                .ForMember(dest => dest.CommentId, opt => opt.MapFrom(src => src.CommentId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.MemeId, opt => opt.MapFrom(src => src.MemeRefId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.PageUser.UserName))
                .ForMember(dest => dest.Txt, opt => opt.MapFrom(src => src.Txt))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.EditDate, opt => opt.MapFrom(src => src.EditDate));




        }

    }
}
