using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using ratingtoolService.DataObjects;
using ratingtoolService.Models;
using AutoMapper;

namespace ratingtoolService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // TODO: comment out again
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            // Set default and null value handling to "Include" for Json Serializer
            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;

            // Map data transfer objects to models
            // using AutoMapper: https://github.com/AutoMapper/AutoMapper/wiki/Getting-started
            Mapper.Initialize(cfg =>
            {
                //Type on the left is source type, type on the right is destination type,
                //thereofre CreateMap<BpCurrentRating, BusinessPartner> allows to map
                //from an instance to the DTO BpCurrentRating to a new instance of 
                //the model BusinessPartner
                cfg.CreateMap<BpCurrentRating, Rating>();
                //This call to ForMember maps from BpCurrentRating.BusinessPartnerId 
                //to BusinessParnter.Id
                //e.g. 
                //.ForMember(dst => dst.RatingId, map => map.MapFrom(x => x.RatingId));
                cfg.CreateMap<Rating, BpCurrentRating>()
                    .ForMember(dst => dst.RatingBpId, map => map.MapFrom(x => x.BusinessPartner.Id))
                    .ForMember(dst => dst.ShortName, map => map.MapFrom(x => x.BusinessPartner.ShortName))
                    .ForMember(dst => dst.BusinessPartnerId, map => map.MapFrom(x => x.BusinessPartner.BusinessPartnerId));

                cfg.CreateMap<RatingSheet, PartialRating>();
                cfg.CreateMap<PartialRating, RatingSheet>()
                    .ForMember(dst => dst.RatingGuid, map => map.MapFrom(x => x.Rating.Id));
            });

            Database.SetInitializer(new ratingtoolInitializer());
        }
    }

    public class ratingtoolInitializer : ClearDatabaseSchemaAlways<RatingtoolContext>
    {
        protected override void Seed(RatingtoolContext context)
        {
            List<BusinessPartner> BusinessPartners = new List<BusinessPartner>
            {
                new BusinessPartner { BusinessPartnerId = 1, ShortName = "Company A", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 2, ShortName = "Company A", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 3, ShortName = "Company A", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 4, ShortName = "Company A", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 5, ShortName = "Company A", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 6, ShortName = "Company A", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 7, ShortName = "Company A", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 8, ShortName = "Sovereign B", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 9, ShortName = "Sovereign B", Id = Guid.NewGuid().ToString() },
                new BusinessPartner { BusinessPartnerId = 10, ShortName = "Sovereign B", Id = Guid.NewGuid().ToString() }
            };

            foreach (BusinessPartner BusinessPartner in BusinessPartners)
            {
                context.Set<BusinessPartner>().Add(BusinessPartner);
            }

            List<Rating> Ratings = new List<Rating> { };
            int nCorRatings = 7;
            int nSovRatings = 3;
            for (int i = 0; i < nCorRatings; i++)
            {
                Ratings.Add(new Rating
                {
                    RatingId = i + 1,
                    BusinessPartnerID = i + 1,
                    RatingStatus = Rating.Status.Approved,
                    ValidUntil = DateTime.Parse("2014-02-14"),
                    RatingClass = Rating.InternalRatingClass.B,
                    RatingMethod = Rating.RatingMethodType.COR,
                    Id = Guid.NewGuid().ToString()
                });
            }
            for (int i = 0; i < nSovRatings; i++)
            {
                Ratings.Add(new Rating
                {
                    RatingId = i + 1 + nCorRatings,
                    BusinessPartnerID = i + 1 + nCorRatings,
                    RatingStatus = Rating.Status.Approved,
                    ValidUntil = DateTime.Parse("2014-02-14"),
                    RatingClass = Rating.InternalRatingClass.A,
                    RatingMethod = Rating.RatingMethodType.SOV,
                    Id = Guid.NewGuid().ToString()
                });
            }
            
            foreach (Rating Rating in Ratings)
            {
                context.Set<Rating>().Add(Rating);
            }

            List<PartialRating> PartialRatings = new List<PartialRating> { };

            for (int i = 0; i < nCorRatings; i++)
            {
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 1, Name = "Equity Capital Ratio", Ratio = 12.2F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 2, Name = "Liquidity Ratio", Ratio = 4.9F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 3, Name = "EBITDA margin", Ratio = 10.2F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 4, Name = "Sales (Mio. EUR)", Ratio = 10245.23F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 5, Name = "Dept Ratio", Ratio = 0.21F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 6, Name = "Dept Ratio", Ratio = 0.21F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 7, Name = "Strength", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.08F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 8, Name = "Weaknesses", RiskGroup = PartialRating.RiskGroupType.B, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.06F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 9, Name = "Strength", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.06F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 10, Name = "Opportunities", RiskGroup = PartialRating.RiskGroupType.C, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.06F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 11, Name = "Trends", RiskGroup = PartialRating.RiskGroupType.B, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.08F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 12, Name = "Sector", RiskGroup = PartialRating.RiskGroupType.D, RatingSection = PartialRating.RatingSectionType.Environment, Weight = 0.06F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 13, Name = "Ecomomic Cycle", RiskGroup = PartialRating.RiskGroupType.C, RatingSection = PartialRating.RatingSectionType.Environment, Weight = 0.06F, RatingID = i + 1 });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 14, Name = "Country Risk", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Environment, Weight = 0.06F, RatingID = i + 1 });
            }
            for (int i = 0; i < nSovRatings; i++)
            {
                // SOV Rating
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 1, Name = "GNP / Population", Ratio = 30125.7F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 2, Name = "GDP Growth Rate", Ratio = 0.4F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 3, Name = "Inflation Rate", Ratio = 1.0F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 4, Name = "Investment Ratio", Ratio = 16.2F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 5, Name = "Structural Strength", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 6, Name = "Current account", Ratio = 0.21F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 7, Name = "Foreign Dept", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 8, Name = "Domestic policy", RiskGroup = PartialRating.RiskGroupType.B, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 9, Name = "Foreign policy", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 10, Name = "Economic System", RiskGroup = PartialRating.RiskGroupType.C, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.9F, RatingID = i + 1 + nCorRatings });
                PartialRatings.Add(new PartialRating { Id = Guid.NewGuid().ToString(), PartialRatingId = i + 11, Name = "Stability of Banking Industry", RiskGroup = PartialRating.RiskGroupType.B, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.10F, RatingID = i + 1 + nCorRatings });
            }

            foreach (PartialRating PartialRating in PartialRatings)
            {
                context.Set<PartialRating>().Add(PartialRating);
            }
            base.Seed(context);
        }
    }
}

