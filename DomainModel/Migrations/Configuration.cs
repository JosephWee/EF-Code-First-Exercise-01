namespace DomainModel.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;
    using Ent = DomainModel.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<DomainModel.VehicleRentalContext>
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DomainModel.VehicleRentalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //Create FK that is set to null when parent row is deleted:
            //ChildTable, ParentTable, FK, ParentPK
            var list_FK_SET_NULL
                = new List<Tuple<string, string, string, string>>()
            {
                new Tuple<string, string, string, string>(
                    "dbo.MotorVehicles", "dbo.Locations", "Location_LocationId", "LocationId")
            };

            foreach (var tup in list_FK_SET_NULL)
            {
                string FKName =
                    string.Format(
                        "FK_{0}_{1}_{2}",
                        tup.Item1,
                        tup.Item2,
                        tup.Item3
                    );

                string sqlDropFK =
                    string.Format(
                            @"IF EXISTS (SELECT 1 FROM SYS.FOREIGN_KEYS WHERE object_id = OBJECT_ID(N'[{0}]') AND parent_object_id = OBJECT_ID(N'{1}'))
                                ALTER TABLE {1} DROP CONSTRAINT [{0}];",
                            FKName,
                            tup.Item1
                        );

                string sqlCreateFK =
                    string.Format(
                            "ALTER TABLE {1} ADD CONSTRAINT [{0}] FOREIGN KEY ({3}) REFERENCES {2} ({4}) ON UPDATE NO ACTION ON DELETE SET NULL;",
                            FKName,
                            tup.Item1,
                            tup.Item2,
                            tup.Item3,
                            tup.Item4
                        );

                log.Info(FKName);
                log.Info(sqlDropFK);
                log.Info(sqlCreateFK);

                context.Database.ExecuteSqlCommand(sqlDropFK);
                context.Database.ExecuteSqlCommand(sqlCreateFK);
            }

            //Create UNIQUE CLUSTERED MULTI-COLUMN INDEX on the following:
            //TableName, Coulmn1Name, Collumn2Name
            var list_UNIQUE_CONSTRAINT_CLUSTERED =
                new List<Tuple<string, string, string>>()
            {
                new Tuple<string, string, string>(
                    "dbo.Addresses", "Suburb_SuburbId", "AddressLine1"),
                new Tuple<string, string, string>(
                    "dbo.Suburbs", "City_CityId", "Name"),
                new Tuple<string, string, string>(
                    "dbo.Cities", "State_StateId", "Name"),
                new Tuple<string, string, string>(
                    "dbo.States", "Country_CountryId", "Name"),
                new Tuple<string, string, string>(
                    "dbo.MotorVehicleModels", "VehicleMake_VehicleMakeId", "Name")
            };

            foreach (var tup in list_UNIQUE_CONSTRAINT_CLUSTERED)
            {
                string FKName =
                    string.Format(
                        "IX_UNIQUE_{0}_{1}_{2}",
                        tup.Item1,
                        tup.Item2,
                        tup.Item3
                    );

                string sqlDropFK =
                    string.Format(
                            @"IF EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = N'{0}')
                                ALTER TABLE {1} DROP CONSTRAINT [{0}];",
                            FKName,
                            tup.Item1
                        );

                string sqlCreateFK =
                    string.Format(
                            "ALTER TABLE {1} ADD CONSTRAINT [{0}] UNIQUE ({2}, {3});",
                            FKName,
                            tup.Item1,
                            tup.Item2,
                            tup.Item3
                        );

                log.Info(FKName);
                log.Info(sqlDropFK);
                log.Info(sqlCreateFK);

                context.Database.ExecuteSqlCommand(sqlDropFK);
                context.Database.ExecuteSqlCommand(sqlCreateFK);
            }
        }
    }
}