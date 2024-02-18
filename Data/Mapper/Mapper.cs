using AutoMapper;
using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<SearchResult<AdminReview>, SearchResult<AdminReviewPaginationModel>>().ReverseMap();
            CreateMap<AdminReview, AdminReviewPaginationModel>().ReverseMap();
            CreateMap<SearchResult<AudienceReview>, SearchResult<AudienceReviewPaginationModel>>().ReverseMap();
            CreateMap<AudienceReview, AudienceReviewPaginationModel>().ReverseMap();
            CreateMap<SearchResult<AudienceRating>, SearchResult<AudienceRatingPaginationModel>>().ReverseMap();
            CreateMap<AudienceRating, AudienceRatingPaginationModel>().ReverseMap();
            CreateMap<AudienceRating, IEnumerable<TrendingMoviesModel>>().ReverseMap();
        }
    }
}
