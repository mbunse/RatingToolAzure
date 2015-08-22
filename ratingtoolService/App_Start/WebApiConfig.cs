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
                cfg.CreateMap<MobileBusinessPartner, Rating>();
                //This call to ForMember maps from BpCurrentRating.BusinessPartnerId 
                //to BusinessParnter.Id
                //e.g. 
                //.ForMember(dst => dst.RatingId, map => map.MapFrom(x => x.RatingId));
                cfg.CreateMap<Rating, MobileBusinessPartner>()
                    .ForMember(dst => dst.RatingBpId, map => map.MapFrom(x => x.BusinessPartner.Id))
                    .ForMember(dst => dst.ShortName, map => map.MapFrom(x => x.BusinessPartner.ShortName))
                    .ForMember(dst => dst.BusinessPartnerId, map => map.MapFrom(x => x.BusinessPartner.BusinessPartnerId));

                cfg.CreateMap<MobileRatingSheet, RatingSheetSection>()
                    .ForMember(dst => dst.PartialRatingsInSection, map => map.MapFrom(x => x.PartialRatingsInSection));
                cfg.CreateMap<RatingSheetSection, MobileRatingSheet>()
                    .ForMember(dst => dst.PartialRatingsInSection, map => map.MapFrom(x => x.PartialRatingsInSection))
                    .ForMember(dst => dst.RatingGuid, map => map.MapFrom(x => x.Rating.Id));
                cfg.CreateMap<MobilePartialRating, PartialRating>();
                cfg.CreateMap<PartialRating, MobilePartialRating>();
                    
            });

            Database.SetInitializer(new ratingtoolInitializer());
        }
    }

    public class ratingtoolInitializer : ClearDatabaseSchemaAlways<RatingtoolContext>
    {
        protected override void Seed(RatingtoolContext context)
        {
            List<BusinessPartner> BusinessPartners = new List<BusinessPartner> { };
            List<Rating> Ratings = new List<Rating> { };
            int nCorRatings = 7;
            int nSovRatings = 3;
            int iBp = 0;
            BusinessPartner bp;

            List<PartialRating> PartialRatings = new List<PartialRating> { };
            List<RatingSheetSection> RatingSheetSections = new List<RatingSheetSection> { };
            int iRatingSheetSection = 0;
            int iPartialRating = 0;
            RatingSheetSection ratingSheetSection;

            for (int i = 0; i < nCorRatings; i++)
            {
                bp = new BusinessPartner { BusinessPartnerId = ++iBp, ShortName = "Company A", Id = Guid.NewGuid().ToString() };
                BusinessPartners.Add(bp);
                Ratings.Add(new Rating
                {
                    RatingId = bp.BusinessPartnerId,
                    BusinessPartnerID = bp.BusinessPartnerId,
                    RatingStatus = Rating.Status.Approved,
                    ValidUntil = DateTime.Parse("2014-02-14"),
                    RatingClass = Rating.InternalRatingClass.B,
                    RatingMethod = Rating.RatingMethodType.COR,
                    Id = Guid.NewGuid().ToString()
                });

                ratingSheetSection = new RatingSheetSection
                {
                    RatingID = bp.BusinessPartnerId,
                    RatingSheetSectionId = ++iRatingSheetSection,
                    Id = Guid.NewGuid().ToString(),
                    Name = "Quantitive",
                    RiskGroup = PartialRating.RiskGroupType.B
                };
                RatingSheetSections.Add(ratingSheetSection);

                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Equity Capital Ratio",
                    Ratio = 12.2F,
                    Weight = 0.08F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Liquidity Ratio",
                    Ratio = 4.9F,
                    Weight = 0.08F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "EBITDA margin",
                    Ratio = 10.2F,
                    Weight = 0.08F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Sales (Mio. EUR)",
                    Ratio = 10245.23F,
                    Weight = 0.08F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Dept Ratio",
                    Ratio = 0.21F,
                    Weight = 0.08F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Dept Ratio",
                    Ratio = 0.21F,
                    Weight = 0.08F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });

                ratingSheetSection = new RatingSheetSection
                {
                    RatingID = bp.BusinessPartnerId,
                    RatingSheetSectionId = ++iRatingSheetSection,
                    Id = Guid.NewGuid().ToString(),
                    Name = "Qualitative",
                    RiskGroup = PartialRating.RiskGroupType.B
                };
                RatingSheetSections.Add(ratingSheetSection);

                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Strength",
                    RiskGroup = PartialRating.RiskGroupType.A,
                    Weight = 0.08F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Weaknesses",
                    RiskGroup = PartialRating.RiskGroupType.B,
                    Weight = 0.06F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Opportunities",
                    RiskGroup = PartialRating.RiskGroupType.C,
                    Weight = 0.06F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Trends",
                    RiskGroup = PartialRating.RiskGroupType.B,
                    Weight = 0.08F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });


                ratingSheetSection = new RatingSheetSection
                {
                    RatingID = bp.BusinessPartnerId,
                    RatingSheetSectionId = ++iRatingSheetSection,
                    Id = Guid.NewGuid().ToString(),
                    Name = "Environment",
                    RiskGroup = PartialRating.RiskGroupType.B
                };
                RatingSheetSections.Add(ratingSheetSection);
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Sector",
                    RiskGroup = PartialRating.RiskGroupType.D,
                    Weight = 0.06F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Ecomomic Cycle",
                    RiskGroup = PartialRating.RiskGroupType.C,
                    Weight = 0.06F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Country Risk",
                    RiskGroup = PartialRating.RiskGroupType.A,
                    Weight = 0.06F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });


            }
            for (int i = 0; i < nSovRatings; i++)
            {
                bp = new BusinessPartner { BusinessPartnerId = ++iBp, ShortName = "Sovereign B", Id = Guid.NewGuid().ToString() };
                BusinessPartners.Add(bp);
                Ratings.Add(new Rating
                {
                    RatingId = bp.BusinessPartnerId,
                    BusinessPartnerID = bp.BusinessPartnerId,
                    RatingStatus = Rating.Status.Approved,
                    ValidUntil = DateTime.Parse("2014-02-14"),
                    RatingClass = Rating.InternalRatingClass.A,
                    RatingMethod = Rating.RatingMethodType.SOV,
                    Id = Guid.NewGuid().ToString()
                });

                // SOV Rating
                ratingSheetSection = new RatingSheetSection
                {
                    RatingID = bp.BusinessPartnerId,
                    RatingSheetSectionId = ++iRatingSheetSection,
                    Id = Guid.NewGuid().ToString(),
                    Name = "Quantitive",
                    RiskGroup = PartialRating.RiskGroupType.B
                };
                RatingSheetSections.Add(ratingSheetSection);

                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "GNP / Population",
                    Ratio = 30125.7F,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "GDP Growth Rate",
                    Ratio = 0.4F,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Inflation Rate",
                    Ratio = 1.0F,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Investment Ratio",
                    Ratio = 16.2F,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });

                ratingSheetSection = new RatingSheetSection
                {
                    RatingID = bp.BusinessPartnerId,
                    RatingSheetSectionId = ++iRatingSheetSection,
                    Id = Guid.NewGuid().ToString(),
                    Name = "Qualitative",
                    RiskGroup = PartialRating.RiskGroupType.B
                };
                RatingSheetSections.Add(ratingSheetSection);

                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Structural Strength",
                    RiskGroup = PartialRating.RiskGroupType.A,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Current account",
                    Ratio = 0.21F,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Foreign Dept",
                    RiskGroup = PartialRating.RiskGroupType.A,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Domestic policy",
                    RiskGroup = PartialRating.RiskGroupType.B,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Foreign policy",
                    RiskGroup = PartialRating.RiskGroupType.A,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Economic System",
                    RiskGroup = PartialRating.RiskGroupType.C,
                    Weight = 0.9F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });
                PartialRatings.Add(new PartialRating
                {
                    Id = Guid.NewGuid().ToString(),
                    PartialRatingId = ++iPartialRating,
                    Name = "Stability of Banking Industry",
                    RiskGroup = PartialRating.RiskGroupType.B,
                    Weight = 0.10F,
                    RatingSheetSectionId = ratingSheetSection.RatingSheetSectionId
                });


            }

            foreach (BusinessPartner BusinessPartner in BusinessPartners)
            {
                context.Set<BusinessPartner>().Add(BusinessPartner);
            }

            foreach (Rating Rating in Ratings)
            {
                context.Set<Rating>().Add(Rating);
            }

            foreach (RatingSheetSection RatingSheetSection in RatingSheetSections)
            {
                context.Set<RatingSheetSection>().Add(RatingSheetSection);
            }

            foreach (PartialRating PartialRating in PartialRatings)
            {
                context.Set<PartialRating>().Add(PartialRating);
            }
            base.Seed(context);
        }
    }
}

