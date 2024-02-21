using AutoMapper;
using Data.Entities;
using Data.Exceptions;
using Data.Models;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public AudienceRatingService
             (IAudienceRatingRepository audienceRatingRepository,
             IMapper mapper,
             ApplicationContext context,
             IHttpContextAccessor httpContextAccessor,
             UserManager<User> userManager)
        {
            _audienceRatingRepository = audienceRatingRepository;
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }


        /// <summary>
        /// Displays all Audience Ratings by Filtering and Sorting with Pagination
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of all Audience Ratings</returns>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task<SearchResult<AudienceRatingPaginationModel>> GetAudienceRating(AudienceRatingSortFilterModel model)
        {
            var filterQuery = PredicateBuilder.True<AudienceRating>();
           
            Func<IQueryable<AudienceRating>, IOrderedQueryable<AudienceRating>> orderQuery = null;

            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (loggedUser != null)
            {

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
            }
            else
            {
                throw new UnauthorizedException("No User Found");
            }
            var result = await _audienceRatingRepository.SearchAsync(filterQuery, orderQuery, model.PageNumber, model.PageSize);
            var searchResult = _mapper.Map<SearchResult<AudienceRatingPaginationModel>>(result);
            return searchResult;
            
        }


        /// <summary>
        /// Displays all Trending Movies of last 14 days based on Audience Ratings  
        /// </summary>
        /// <returns>List of Trending Movies</returns>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task<IEnumerable<TrendingMoviesModel>> GetTrendingMovies()
        {
            IList<TrendingMoviesModel> trendingMoviesModel = new List<TrendingMoviesModel>();

            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (loggedUser != null)
            {
                var audienceRatings = _context.AudienceRating

                .Where(s => s.LastReviewedTime.Date <= DateTime.Today &&
                            s.LastReviewedTime.Date >= DateTime.Today.AddDays(-14)).ToList();

                foreach (var audienceRating in audienceRatings)
                {
                    var audienceReviews = _context.AudienceReview

                   .Where(s => s.UpdatedTime.Date <= DateTime.Today &&
                               s.UpdatedTime.Date >= DateTime.Today.AddDays(-14) &&
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
                            Language = audienceRating.Language.ToString(),
                            Year = audienceRating.Year,
                            Rating = ratingsAverage
                        });
                    }
                }
            }
            else
            {
                throw new UnauthorizedException("No User Found");
            }
            var result = trendingMoviesModel.OrderByDescending(trendingMoviesModel => trendingMoviesModel.Rating).Take(10);

            return _mapper.Map<IEnumerable<TrendingMoviesModel>>(result);
        }
    }
}
