﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Interfaces
{
    public interface IAudienceRatingService
    {
        Task<SearchResult<AudienceRatingPaginationModel>> GetAudienceRating(AudienceRatingSortFilterModel model);

    }
}
