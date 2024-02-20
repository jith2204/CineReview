using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Exceptions;
using Data.Models;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class AudienceReviewService : IAudienceReviewService
    {
        readonly ApplicationContext _context;
        readonly IAudienceReviewRepository _audienceReviewRepository;
        readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AudienceReviewService
           (ApplicationContext context,
           IAudienceReviewRepository audienceReviewRepository,
           IMapper mapper,
           UserManager<User> userManager,
           IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _audienceReviewRepository = audienceReviewRepository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel> CreateAudienceReview(AudienceReviewModel model)
        {
            ResponseModel response = new();
            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            using (var transaction = _context.Database.BeginTransaction())
            {
                if (loggedUser != null)
                {
                    var role = await _userManager.GetRolesAsync(loggedUser);

                    if (role.Contains("Audience"))
                    {
                        var filmReview = await _context.AudienceReview.FirstOrDefaultAsync(s => s.FilmName.Equals(model.FilmName) && s.Language == model.Language && s.Year == model.Year && s.UpdatedBy == loggedUser.Id);

                        if (filmReview != null)
                        {
                            throw new DuplicateException("Audience Review Already Exist");
                        }

                        var review = await _context.AdminReview.FirstOrDefaultAsync(s => s.FilmName.Equals(model.FilmName) && s.Language == model.Language && s.Release_Date.Year == model.Year);

                        if (review != null)
                        {
                            AudienceReview audienceReview = new()
                            {
                                FilmName = model.FilmName,
                                Year = model.Year,
                                Language = model.Language,
                                Rating = model.Rating,
                                Review = model.Review,
                                Liked = model.Liked,
                                UpdatedTime = DateTime.Now,
                                UpdatedBy = loggedUser.Id
                            };

                            _context.AudienceReview.Add(audienceReview);

                            var rating = await _context.AudienceRating.FirstOrDefaultAsync(s => s.FilmName.Equals(model.FilmName) && s.Language == model.Language && s.Year == model.Year);

                            if (rating != null)
                            {
                                rating.TotalRatings += model.Rating;
                                var totalRatings = rating.TotalRatings;

                                rating.TotalReviewers += 1;
                                var totalReviewers = rating.TotalReviewers;

                                rating.Rating = totalRatings / totalReviewers;

                                rating.LastReviewedTime = DateTime.Now;

                                _context.AudienceRating.Update(rating);
                            }
                            else
                            {
                                AudienceRating audienceRating = new()
                                {
                                    FilmName = model.FilmName,
                                    Language = model.Language,
                                    Year = model.Year,
                                    TotalRatings = model.Rating,
                                    TotalReviewers = 1,
                                    Rating = model.Rating,
                                    LastReviewedTime = DateTime.Now,
                                };
                                
                                _context.AudienceRating.Add(audienceRating);
                            }

                            if (_context.SaveChanges() > 0)
                            {
                                transaction.Commit();
                                response.Success = true;
                                response.Message = "Your Review has been Successfully Created";
                            }

                            else
                            {
                                transaction.Rollback();
                                response.Success = false;
                                response.Message = "Some error occured while Creating the Review";
                            }
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "You are only able to add Movies of Admin's List of Movies";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Only Audience has the Authority to Create Audience Review";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "No User Found";
                }
            }
            return response;
        }

        public async Task<SearchResult<AudienceReviewPaginationModel>> GetAudienceReview(AudienceReviewSortFilterModel model)
        {

            var filterQuery = PredicateBuilder.True<AudienceReview>();
            Func<IQueryable<AudienceReview>, IOrderedQueryable<AudienceReview>> orderQuery = null;

            if (!string.IsNullOrEmpty(model.FilmName))
            {
                filterQuery = filterQuery.And(s => s.FilmName.Contains(model.FilmName));
            }

            if (model.Release_Year != null)
            {
                filterQuery = filterQuery.And(s => s.Year == model.Release_Year);
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

            if (model.Liked == true)
            {
                filterQuery = filterQuery.And(s => s.Liked == true);
            }

            else if (model.Liked == false)
            {
                filterQuery = filterQuery.And(s => s.Liked == false);
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                filterQuery = filterQuery.And(s => s.User.Email.Contains(model.Email));
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                filterQuery = filterQuery.And(s => s.User.Name.Contains(model.Name));
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                filterQuery = filterQuery.And(s => s.User.UserName.Contains(model.UserName));
            }

            if (model.SortOrder == "ascending")
            {
                switch (model.SortingElement)
                {
                    case "film":
                        orderQuery = q => q.OrderBy(s => s.FilmName);
                        break;
                    case "releaseyear":
                        orderQuery = q => q.OrderBy(s => s.Year);
                        break;
                    case "language":
                        orderQuery = q => q.OrderBy(s => s.Language);
                        break;
                    case "rating":
                        orderQuery = q => q.OrderBy(s => s.Rating);
                        break;
                    case "liked":
                        orderQuery = q => q.OrderBy(s => s.Liked);
                        break;
                    default:
                        orderQuery = q => q.OrderBy(s => s.UpdatedTime);
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
                    case "year":
                        orderQuery = q => q.OrderByDescending(s => s.Year);
                        break;
                    case "language":
                        orderQuery = q => q.OrderByDescending(s => s.Language);
                        break;
                    case "rating":
                        orderQuery = q => q.OrderByDescending(s => s.Rating);
                        break;
                    case "liked":
                        orderQuery = q => q.OrderByDescending(s => s.Liked);
                        break;
                    default:
                        orderQuery = q => q.OrderByDescending(s => s.UpdatedTime);
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
                    case "year":
                        orderQuery = q => q.OrderByDescending(s => s.Year);
                        break;
                    case "language":
                        orderQuery = q => q.OrderBy(s => s.Language);
                        break;
                    case "rating":
                        orderQuery = q => q.OrderByDescending(s => s.Rating);
                        break;
                    case "liked":
                        orderQuery = q => q.OrderByDescending(s => s.Liked);
                        break;
                    default:
                        orderQuery = q => q.OrderByDescending(s => s.UpdatedTime);
                        break;
                }
            }

            var result = await _audienceReviewRepository.SearchAsync(filterQuery, orderQuery, model.PageNumber, model.PageSize, "User");
            var searchResult = _mapper.Map<SearchResult<AudienceReviewPaginationModel>>(result);
            return searchResult;
        }

        public async Task<ResponseModel> UpdateAudienceReview(string filmName, Language language, int year, AudienceReviewModel model)
        {
            ResponseModel response = new();
            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            using (var transaction = _context.Database.BeginTransaction())
            {
                if (loggedUser != null)
                {
                    var role = await _userManager.GetRolesAsync(loggedUser);

                    if (role.Contains("Audience"))
                    {
                        var adminReview = await _context.AdminReview.FirstOrDefaultAsync(s => s.FilmName.Equals(model.FilmName) && s.Language == model.Language && s.Release_Date.Year == model.Year);

                        if (adminReview == null)
                        {
                            throw new NoContentException("Updating Review should be in Admin's List of Movies");
                        }

                        if (filmName != model.FilmName || language != model.Language || year != model.Year)
                        {
                            var film = await _context.AudienceReview.FirstOrDefaultAsync(s => s.FilmName.Equals(model.FilmName) && s.Language == model.Language && s.Year == model.Year && s.UpdatedBy == loggedUser.Id);

                            if (film != null)
                            {
                                throw new DuplicateException("Updating Review Already Exist");
                            }
                        }
                        
                        var filmReview = await _context.AudienceReview.FirstOrDefaultAsync(s => s.FilmName.Equals(filmName) && s.Language == language && s.Year == year && s.UpdatedBy == loggedUser.Id);

                        if (filmReview != null)
                        {
                            var filmRating = await _context.AudienceRating.FirstOrDefaultAsync(s => s.FilmName.Equals(filmName) && s.Language == language && s.Year == year);

                            if (filmRating != null)
                            {
                                var rating = await _context.AudienceRating.FirstOrDefaultAsync(s => s.FilmName.Equals(model.FilmName) && s.Language == model.Language && s.Year == model.Year);
                                
                                if (rating != null)
                                {
                                    if (filmName.Equals(model.FilmName) && language == model.Language && year == model.Year)
                                    {
                                        rating.TotalRatings -= filmReview.Rating;
                                        rating.TotalRatings += model.Rating;
                                        var totalRatings = rating.TotalRatings;

                                        rating.Rating = totalRatings / rating.TotalReviewers;
                                        rating.LastReviewedTime = DateTime.Now;

                                        _context.AudienceRating.Update(rating);
                                    }
                                    else if(filmName != model.FilmName || language != model.Language || year != model.Year)
                                    {
                                        rating.TotalRatings += model.Rating;
                                        var totalRatings = rating.TotalRatings;

                                        rating.TotalReviewers += 1;

                                        rating.Rating = totalRatings / rating.TotalReviewers;
                                        rating.LastReviewedTime = DateTime.Now;

                                        _context.AudienceRating.Update(rating);

                                   
                                        filmRating.TotalRatings -= filmReview.Rating;
                                        var ratings = filmRating.TotalRatings;

                                        filmRating.TotalReviewers -= 1;
                                        var reviewers = filmRating.TotalReviewers;

                                        filmRating.Rating = ratings / reviewers;

                                        _context.AudienceRating.Update(filmRating);
                                    }
                                }
                               
                                else
                                {
                                    AudienceRating audienceRating = new()
                                    {
                                        FilmName = model.FilmName,
                                        Language = model.Language,
                                        Year = model.Year,
                                        TotalRatings = model.Rating,
                                        TotalReviewers = 1,
                                        Rating = model.Rating,
                                        LastReviewedTime = DateTime.Now,
                                    };

                                    _context.AudienceRating.Add(audienceRating);

                                    filmRating.TotalRatings -= filmReview.Rating;
                                    var ratings = filmRating.TotalRatings;

                                    filmRating.TotalReviewers -= 1;
                                    var reviewers = filmRating.TotalReviewers;

                                    filmRating.Rating = ratings / reviewers;

                                    _context.AudienceRating.Update(filmRating);
                                }
                            }

                            else
                            {
                                response.Success = false;
                                response.Message = "Unable to Found Data in Audience Rating";
                            }
                            
                            filmReview.FilmName = model.FilmName;
                            filmReview.Year = model.Year;
                            filmReview.Language = model.Language;
                            filmReview.Rating = model.Rating;

                            if (model.Review != null)
                            {
                                filmReview.Review = model.Review;
                            }

                            filmReview.Liked = model.Liked;
                            filmReview.UpdatedTime = DateTime.Now;

                            _context.AudienceReview.Update(filmReview);
                            
                            if (_context.SaveChanges() > 0)
                            {
                                transaction.Commit();
                                response.Success = true;
                                response.Message = "Review has been Successfully Updated";
                            }
                            else
                            {
                                transaction.Rollback();
                                response.Success = false;
                                response.Message = "Some error occured while Updating the Review";
                            }
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "Review Not Exist";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Only Audience has the authority to Update Audience Review";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "No User Found";
                }
            }
            return response;
        }

        public async Task<ResponseModel> DeleteAudienceReview(string filmName, Language language, int year)
        {
            ResponseModel response = new();
            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            using (var transaction = _context.Database.BeginTransaction())
            {
                if (loggedUser != null)
                {
                    var role = await _userManager.GetRolesAsync(loggedUser);

                    if (role.Contains("Audience"))
                    {
                        var review = await _context.AudienceReview.FirstOrDefaultAsync(s => s.FilmName.Equals(filmName) && s.Language == language && s.Year == year && s.UpdatedBy == loggedUser.Id);
                        
                        if (review == null)
                        {
                            response.Success = false;
                            response.Message = "Review Not Exist";
                        }
                        else
                        {
                            var rating = await _context.AudienceRating.FirstOrDefaultAsync(s => s.FilmName.Equals(filmName) && s.Language == language && s.Year == year);
                          
                            if(rating != null)
                            {
                                if (rating.TotalReviewers > 1)
                                {
                                    rating.TotalRatings -= review.Rating;
                                    var ratings = rating.TotalRatings;

                                    rating.TotalReviewers -= 1;
                                    var reviewers = rating.TotalReviewers;

                                    rating.Rating = ratings / reviewers;

                                    _context.AudienceRating.Update(rating);
                                }
                                else
                                {
                                    _context.AudienceRating.Remove(rating);
                                }

                            }
                            else
                            {
                                response.Success = false;
                                response.Message = "Rating Not Exist";
                            }

                            _context.AudienceReview.Remove(review);

                            if (_context.SaveChanges() > 0)
                            {
                                transaction.Commit();
                                response.Success = true;
                                response.Message = "Review has been Successfully Deleted";
                            }

                            else
                            {
                                transaction.Rollback();
                                response.Success = false;
                                response.Message = "Some error occured while Deleting the Review";
                            }
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Only Audience has the Authority to Delete a Review";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "No User Found";
                }
            }
            return response;
        }
    }
}
