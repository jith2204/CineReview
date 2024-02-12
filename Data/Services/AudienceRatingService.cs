using AutoMapper;
using Data.Entities;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class AudienceRatingService : IAudienceRatingService
    {
        readonly IAudienceRatingRepository _audienceRatingRepository;
        readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AudienceRatingService
             (IAudienceRatingRepository audienceRatingRepository,
             IMapper mapper,
             IHttpContextAccessor httpContextAccessor)
        {
            _audienceRatingRepository = audienceRatingRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SearchResult<AudienceRatingPaginationModel>> GetAudienceRating(AudienceRatingSortFilterModel model)
        {
            var filterQuery = PredicateBuilder.True<AudienceRating>();
            Func<IQueryable<AudienceRating>, IOrderedQueryable<AudienceRating>> orderQuery = null;

            if (!string.IsNullOrEmpty(model.FilmName))
            {
                filterQuery = filterQuery.And(s => s.FilmName.Contains(model.FilmName));
            }

            if (model.Year != null)
            {
                filterQuery = filterQuery.And(s => s.Year == model.Year);
            }

            if (model.Language != null)
            {
                filterQuery = filterQuery.And(s => s.Language == model.Language);
            }

            if (model.Rating != null)
            {
                filterQuery = filterQuery.And(s => s.Rating == model.Rating);
            }

            if (model.RatingLessthan != null)
            {
                filterQuery = filterQuery.And(s => s.Rating < model.RatingLessthan);
            }

            if (model.RatingGreaterthan != null)
            {
                filterQuery = filterQuery.And(s => s.Rating > model.RatingGreaterthan);
            }

            if (model.SortOrder == "ascending")
            {
                switch (model.SortingElement)
                {
                    case "film":
                        orderQuery = q => q.OrderBy(s => s.FilmName);
                        break;
                    case "language":
                        orderQuery = q => q.OrderBy(s => s.Language);
                        break;
                    case "rating":
                        orderQuery = q => q.OrderBy(s => s.Rating);
                        break;
                    default:
                        orderQuery = q => q.OrderBy(s => s.Rating);
                        break;
                }
            }

            if (model.SortOrder == "descending")
            {
                switch (model.SortingElement)
                {
                    case "film":
                        orderQuery = q => q.OrderByDescending(s => s.FilmName);
                        break;
                    case "language":
                        orderQuery = q => q.OrderByDescending(s => s.Language);
                        break;
                    case "rating":
                        orderQuery = q => q.OrderByDescending(s => s.Rating);
                        break;
                    default:
                        orderQuery = q => q.OrderByDescending(s => s.Rating);
                        break;
                }
            }

            else
            {
                switch (model.SortingElement)
                {
                    case "film":
                        orderQuery = q => q.OrderBy(s => s.FilmName);
                        break;
                    case "language":
                        orderQuery = q => q.OrderBy(s => s.Language);
                        break;
                    case "rating":
                        orderQuery = q => q.OrderByDescending(s => s.Rating);
                        break;
                    default:
                        orderQuery = q => q.OrderByDescending(s => s.Rating);
                        break;
                }
            }

            var result = await _audienceRatingRepository.SearchAsync(filterQuery, orderQuery, model.PageNumber, model.PageSize);
            var searchResult = _mapper.Map<SearchResult<AudienceRatingPaginationModel>>(result);
            return searchResult;
        }
    }
}
