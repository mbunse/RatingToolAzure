using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;

namespace ratingtoolService.Models
{
    public class RatingtoolContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.
        private const string connectionStringName = "Name=MS_TableConnectionString";

        public RatingtoolContext() : base(connectionStringName)
        {
        }
        /* 
        This three line advice the entity framework to build tables for the
        three classes BusinessPartner, Rating and PartialRating.
        */
        public DbSet<BusinessPartner> BusinessPartners { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<PartialRating> PartialRatings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = ServiceSettingsDictionary.GetSchemaName();
            if (!string.IsNullOrEmpty(schema))
            {
                modelBuilder.HasDefaultSchema(schema);
            }
            /*
            The following line of code is part of the VS template.
            It creates a column annotation "ServiceTableColumn" for data fields with attribute [TableColumn]. 
            E.g.: The attribute [TableColumn(TableColumnType.UpdatedAt)] will be translated to a 
            column annotation "UpdatedAt".
            This annotation enables the entity framework to automatically fill the corresponding fields
            when according actions took place.
            */
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }

    }

}
