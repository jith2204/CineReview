using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Exceptions;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AdminReviewService : IAdminReviewService
    {
        readonly ApplicationContext _context;
        readonly IAdminReviewRepository _adminReviewRepository;
        readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminReviewService
            (ApplicationContext context,
            IAdminReviewRepository adminReviewRepository,
            IMapper mapper,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _adminReviewRepository = adminReviewRepository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// Creates an Admin Review
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The Successful Output Response</returns>
        /// <exception cref="InternalServerException"></exception>
        /// <exception cref="DuplicateException"></exception>
        /// <exception cref="NoContentException"></exception>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="ForbiddenException"></exception >
        /// <exception cref="UnauthorizedException"></exception >
        public async Task<ResponseModel> CreateAdminReview(AdminReviewModel model)
        {
            ResponseModel response = new();
            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (loggedUser != null)
            {
                var role = await _userManager.GetRolesAsync(loggedUser);

                if (role.Contains("Admin"))
                {
                    var review = await _context.AdminReview.FirstOrDefaultAsync(s => s.FilmName.Equals(model.FilmName) && s.Language == model.Language && s.Release_Date.Year == model.Release_Date.Year);
                    if (review == null)
                    {
                        AdminReview adminReview = new()
                        {
                            FilmName = model.FilmName,
                            Release_Date = model.Release_Date,
                            Language = model.Language,
                            Genre = model.Genre,
                            Rating = model.Rating,
                            Review = model.Review,
                            Liked = model.Liked,
                            CreatedTime = DateTime.Now
                        };

                        _context.AdminReview.Add(adminReview);

                        if (_context.SaveChanges() > 0)
                        {
                            response.Success = true;
                            response.Message = "Review has been Successfully Created";
                        }

                        else
                        {
                            throw new InternalServerException("Some error occured while Creating the Review");
                        }
                    }
                    else
                    {
                        throw new DuplicateException("Review was already Created");
                    }
                }
                else
                {
                    throw new ForbiddenException("Only Admin has the Authority to Create a Review");
                }
            }
            else
            {
                throw new UnauthorizedException("User Not Found");
            }
            return response;
        }

        /// <summary>
        /// Deletes an Admin Review
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="language"></param>
        /// <param name="year"></param>
        /// <returns>The Successful Output Response</returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="InternalServerException"></exception>
        /// <exception cref="ForbiddenException"></exception>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task<ResponseModel> DeleteAdminReview(string filmName, Language language, int year)
        {
            ResponseModel response = new();
            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (loggedUser != null)
            {
                var role = await _userManager.GetRolesAsync(loggedUser);

                if (role.Contains("Admin"))
                {
                    var review = await _context.AdminReview.FirstOrDefaultAsync(s => s.FilmName.Equals(filmName) && s.Language == language && s.Release_Date.Year == year);
                    if (review == null)
                    {
                        throw new EntityNotFoundException("No Review Exist");
                    }
                    else
                    {
                        _context.AdminReview.Remove(review);

                        if (_context.SaveChanges() > 0)
                        {
                            response.Success = true;
                            response.Message = "Review has been Successfully Deleted";
                        }

                        else
                        {
                            throw new InternalServerException("Some error occured while Deleting the Review");
                        }
                    }
                }
                else
                {
                    throw new ForbiddenException("Only Admin has the Authority to Delete a Review");
                }
            }
            else
            {
                throw new UnauthorizedException("No User Found");
            }
            return response;
        }


        /// <summary>
        /// Displays all Admin Reviews by Filtering and Sorting with Pagination
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of all Admin Reviews</returns>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task<SearchResult<AdminReviewPaginationModel>> GetAdminReview(AdminReviewSortFilterModel model)
        {
            var filterQuery = PredicateBuilder.True<AdminReview>();
            
            Func<IQueryable<AdminReview>, IOrderedQueryable<AdminReview>> orderQuery = null;

            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (loggedUser != null)
            {
                if (!string.IsNullOrEmpty(model.FilmName))
                {
                    filterQuery = filterQuery.And(s => s.FilmName.Contains(model.FilmName));
                }

                if (model.Release_Date != null)
                {
                    filterQuery = filterQuery.And(s => s.Release_Date == model.Release_Date);
                }

                if (model.Release_Year != null)
                {
                    filterQuery = filterQuery.And(s => s.Release_Date.Year == model.Release_Year);
                }

                if (model.Release_Month != null)
                {
                    filterQuery = filterQuery.And(s => s.Release_Date.Month == model.Release_Month);
                }

                if (model.Language != null)
                {
                    filterQuery = filterQuery.And(s => s.Language == model.Language);
                }

                if (model.Genre != null)
                {
                    filterQuery = filterQuery.And(s => s.Genre == model.Genre);
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

                if (model.SortOrder == "ascending")
                {
                    switch (model.SortingElement)
                    {
                        case "film":
                            orderQuery = q => q.OrderBy(s => s.FilmName);
                            break;
                        case "releasedate":
                            orderQuery = q => q.OrderBy(s => s.Release_Date);
                            break;
                        case "language":
                            orderQuery = q => q.OrderBy(s => s.Language);
                            break;
                        case "genre":
                            orderQuery = q => q.OrderBy(s => s.Genre);
                            break;
                        case "rating":
                            orderQuery = q => q.OrderBy(s => s.Rating);
                            break;
                        case "liked":
                            orderQuery = q => q.OrderBy(s => s.Liked);
                            break;
                        default:
                            orderQuery = q => q.OrderBy(s => s.CreatedTime);
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
                        case "releasedate":
                            orderQuery = q => q.OrderByDescending(s => s.Release_Date);
                            break;
                        case "language":
                            orderQuery = q => q.OrderByDescending(s => s.Language);
                            break;
                        case "genre":
                            orderQuery = q => q.OrderByDescending(s => s.Genre);
                            break;
                        case "rating":
                            orderQuery = q => q.OrderByDescending(s => s.Rating);
                            break;
                        case "liked":
                            orderQuery = q => q.OrderByDescending(s => s.Liked);
                            break;
                        default:
                            orderQuery = q => q.OrderByDescending(s => s.CreatedTime);
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
                        case "releasedate":
                            orderQuery = q => q.OrderByDescending(s => s.Release_Date);
                            break;
                        case "language":
                            orderQuery = q => q.OrderBy(s => s.Language);
                            break;
                        case "genre":
                            orderQuery = q => q.OrderBy(s => s.Genre);
                            break;
                        case "rating":
                            orderQuery = q => q.OrderByDescending(s => s.Rating);
                            break;
                        case "liked":
                            orderQuery = q => q.OrderByDescending(s => s.Liked);
                            break;
                        default:
                            orderQuery = q => q.OrderByDescending(s => s.CreatedTime);
                            break;
                    }
                }

            }
            else
            {
                throw new UnauthorizedException("No User Found");
            }
            var result = await _adminReviewRepository.SearchAsync(filterQuery, orderQuery, model.PageNumber, model.PageSize);
            var searchResult = _mapper.Map<SearchResult<AdminReviewPaginationModel>>(result);
            return searchResult;
        }


        /// <summary>
        /// Updates an Admin Review
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="language"></param>
        /// <param name="year"></param>
        /// <param name="model"></param>
        /// <returns>The Successful Output Response</returns>
        /// <exception cref="DuplicateException"></exception>
        /// <exception cref="InternalServerException"></exception>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="ForbiddenException"></exception>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task<ResponseModel> UpdateAdminReview(string filmName, Language language, int year, AdminReviewModel model)
        {
            ResponseModel response = new();
            var loggedUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (loggedUser != null)
            {
                var role = await _userManager.GetRolesAsync(loggedUser);

                if (role.Contains("Admin"))
                {
                    if (filmName != model.FilmName || language != model.Language || year != model.Release_Date.Year)
                    {
                        var film = await _context.AdminReview.FirstOrDefaultAsync(s => s.FilmName.Equals(model.FilmName) && s.Language == model.Language && s.Release_Date.Year == model.Release_Date.Year);

                        if (film != null)
                        {
                            throw new DuplicateException("Updating Review Already Exist");
                        }
                    }
                    
                    var filmReview = await _context.AdminReview.FirstOrDefaultAsync(s => s.FilmName.Equals(filmName) && s.Language == language && s.Release_Date.Year == year);

                    if (filmReview != null)
                    {
                        filmReview.FilmName = model.FilmName;
                        filmReview.Release_Date = model.Release_Date;
                        filmReview.Language = model.Language;
                        filmReview.Genre = model.Genre;
                        filmReview.Rating = model.Rating;

                        if (!string.IsNullOrEmpty(model.Review))
                        {
                            filmReview.Review = model.Review;
                        }

                        filmReview.Liked = model.Liked;
                        filmReview.UpdatedTime = DateTime.Now;

                        _context.AdminReview.Update(filmReview);

                        if (_context.SaveChanges() > 0)
                        {
                            response.Success = true;
                            response.Message = "Review has been Successfully Updated";
                        }
                        else
                        {
                            throw new InternalServerException("Some error occured while Updating the Review");
                        }
                    }
                    else
                    {
                        throw new EntityNotFoundException("No Review Exist");
                    }
                }
                else
                {
                    throw new ForbiddenException("Only Admin has the authority to Update a Review");
                }
            }
            else
            {
                throw new UnauthorizedException("No User Found");
            }
            return response;
        }
    }
}
