using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MemeSite.Data.Models;
using MemeSite.ViewModels;

namespace MemeSite.AutoMapper
{
    public class ViewModelToModelMappingProfile : Profile
    {
        public ViewModelToModelMappingProfile()
        {

            CreateMap<AddCommentVM, Comment>()
                .ForPath(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForPath(dest => dest.MemeRefId, opt => opt.MapFrom(src => src.MemeId))
                .ForPath(dest => dest.Txt, opt => opt.MapFrom(src => src.Txt))
                .ForPath(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
                .ForPath(dest => dest.IsArchived, opt => opt.MapFrom(src => false));


            CreateMap<EditCommentVM, Comment>()
                 .ForPath(dest => dest.Txt, opt => opt.MapFrom(src => src.Txt));

            CreateMap<AddFavouriteVM, Favourite>()
                .ForPath(dest => dest.MemeRefId, opt => opt.MapFrom(src => src.MemeId))
                .ForPath(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForPath(dest => dest.CreateFavDate, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
