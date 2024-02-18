using AutoMapper;
using Data.Entities;
using Data.Models;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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
        readonly ApplicationContext _context;
        public AudienceRatingService
             (IAudienceRatingRepository audienceRatingRepository,
             IMapper mapper,
             ApplicationContext context)
        {
            _audienceRatingRepository = audienceRatingRepository;
            _mapper = mapper;
            _context = context;
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

        public async Task<IEnumerable<TrendingMoviesModel>> GetTrendingMovies()
        {
            IList<TrendingMoviesModel> trendingMoviesModel = new List<TrendingMoviesModel>();

            var audienceRatings = _context.AudienceRating
                
                .Where(s => s.LastReviewedTime.Date <= DateTime.Today && 
                            s.LastReviewedTime.Date >= DateTime.Today.AddDays(-7)).ToList();

            foreach (var audienceRating in audienceRatings)
            {
                var audienceReviews = _context.AudienceReview

               .Where(s => s.UpdatedTime.Date <= DateTime.Today &&
                           s.UpdatedTime.Date >= DateTime.Today.AddDays(-7) &&
                           s.FilmName == audienceRating.FilmName &&
                           s.Language == audienceRating.Language &&
                           s.Year == audienceRating.Year);

                if (audienceReviews.Any())
                {
                    var ratingsSum = audienceReviews.Sum(s => s.Rating);

                    var ratingsAverage = ratingsSum / audienceReviews.Count();

                    trendingMoviesModel.Add(new TrendingMoviesModel
                    {
                        FilmName = audienceRating.FilmName,
                        Language = audienceRating.Language,
                        Year = audienceRating.Year,
                        Rating = ratingsAverage
                    });
                }
            }

            var result = trendingMoviesModel.OrderByDescending(trendingMoviesModel => trendingMoviesModel.Rating);

            return _mapper.Map<IEnumerable<TrendingMoviesModel>>(result);
        }
    }
}
