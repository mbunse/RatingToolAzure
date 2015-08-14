using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using ratingtoolService.DataObjects;
using ratingtoolService.Models;

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
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            // Set default and null value handling to "Include" for Json Serializer
            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            
            Database.SetInitializer(new ratingtoolInitializer());
        }
    }

    public class ratingtoolInitializer : ClearDatabaseSchemaAlways<ratingtoolContext>
    {
        protected override void Seed(ratingtoolContext context)
        {
            List<DataObjects.BusinessPartner> BusinessPartners = new List<DataObjects.BusinessPartner>
            {
                new DataObjects.BusinessPartner { Id = "1", ShortName = "Company A" },
                new DataObjects.BusinessPartner { Id = "2", ShortName = "Sovereign B" }
            };

            foreach (DataObjects.BusinessPartner BusinessPartner in BusinessPartners)
            {
                context.Set<DataObjects.BusinessPartner>().Add(BusinessPartner);
            }

            List<DataObjects.Rating> Ratings = new List<DataObjects.Rating>
            {
                new Rating
                {
                    Id = "1",
                    BusinessPartnerID = "1",
                    RatingStatus = Rating.Status.Approved,
                    ValidUntil = DateTime.Parse("2014-02-14"),
                    RatingClass = Rating.InternalRatingClass.B2,
                    RatingMethod = Rating.RatingMethodType.COR
                },
                new Rating
                {
                    Id = "2",
                    BusinessPartnerID = "2",
                    RatingStatus = Rating.Status.Approved,
                    ValidUntil = DateTime.Parse("2014-01-01"),
                    RatingClass = Rating.InternalRatingClass.A1,
                    RatingMethod = Rating.RatingMethodType.SOV
                }
            };

            foreach (DataObjects.Rating Rating in Ratings)
            {
                context.Set<DataObjects.Rating>().Add(Rating);
            }

            List<DataObjects.PartialRating> PartialRatings = new List<DataObjects.PartialRating>
            {
                // COR Rating
                new PartialRating { Id = "1", Name = "Equity Capital Ratio", Ratio = 12.2F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = "1" },
                new PartialRating { Id = "2", Name = "Liquidity Ratio", Ratio = 4.9F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = "1" },
                new PartialRating { Id = "3", Name = "EBITDA margin", Ratio = 10.2F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = "1" },
                new PartialRating { Id = "4", Name = "Sales (Mio. EUR)", Ratio = 10245.23F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = "1" },
                new PartialRating { Id = "5", Name = "Dept Ratio", Ratio = 0.21F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = "1" },
                new PartialRating { Id = "6", Name = "Dept Ratio", Ratio = 0.21F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.08F, RatingID = "1" },
                new PartialRating { Id = "7", Name = "Strength", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.08F, RatingID = "1" },
                new PartialRating { Id = "8", Name = "Weaknesses", RiskGroup = PartialRating.RiskGroupType.B, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.06F, RatingID = "1" },
                new PartialRating { Id = "9", Name = "Strength", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.06F, RatingID = "1" },
                new PartialRating { Id = "10", Name = "Opportunities", RiskGroup = PartialRating.RiskGroupType.C, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.06F, RatingID = "1" },
                new PartialRating { Id = "11", Name = "Trends", RiskGroup = PartialRating.RiskGroupType.B, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.08F, RatingID = "1" },
                new PartialRating { Id = "12", Name = "Sector", RiskGroup = PartialRating.RiskGroupType.D, RatingSection = PartialRating.RatingSectionType.Environment, Weight = 0.06F, RatingID = "1" },
                new PartialRating { Id = "13", Name = "Ecomomic Cycle", RiskGroup = PartialRating.RiskGroupType.C, RatingSection = PartialRating.RatingSectionType.Environment, Weight = 0.06F, RatingID = "1" },
                new PartialRating { Id = "14", Name = "Country Risk", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Environment, Weight = 0.06F, RatingID = "1" },
                // SOV Rating
                new PartialRating { Id = "15", Name = "GNP / Population", Ratio = 30125.7F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "16", Name = "GDP Growth Rate", Ratio = 0.4F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "17", Name = "Inflation Rate", Ratio = 1.0F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "18", Name = "Investment Ratio", Ratio = 16.2F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "19", Name = "Structural Strength", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "20", Name = "Current account", Ratio = 0.21F, RatingSection = PartialRating.RatingSectionType.Quantitive, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "21", Name = "Foreign Dept", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "22", Name = "Domestic policy", RiskGroup = PartialRating.RiskGroupType.B, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "23", Name = "Foreign policy", RiskGroup = PartialRating.RiskGroupType.A, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "24", Name = "Economic System", RiskGroup = PartialRating.RiskGroupType.C, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.9F, RatingID = "1" },
                new PartialRating { Id = "25", Name = "Stability of Banking Industry", RiskGroup = PartialRating.RiskGroupType.B, RatingSection = PartialRating.RatingSectionType.Qualitative, Weight = 0.10F, RatingID = "1" }

            };

            foreach (DataObjects.PartialRating PartialRating in PartialRatings)
            {
                context.Set<DataObjects.PartialRating>().Add(PartialRating);
            }
            base.Seed(context);
        }
    }
}

