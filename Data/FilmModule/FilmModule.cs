using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.Services;
using Data.Services.Interfaces;

namespace Data.FilmModule
{
    public class FilmModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdminReviewService>().As<IAdminReviewService>();
            builder.RegisterType<AdminReviewRepository>().As<IAdminReviewRepository>();
            builder.RegisterType<AudienceReviewRepository>().As<IAudienceReviewRepository>();
            builder.RegisterType<AudienceReviewService>().As<IAudienceReviewService>();
            builder.RegisterType<AudienceRatingRepository>().As<IAudienceRatingRepository>();
            builder.RegisterType<AudienceRatingService>().As<IAudienceRatingService>();
            base.Load(builder);
        }
    }
}
