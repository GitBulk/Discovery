using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AzureOne.Models
{
    public class TeamInitializer : CreateDatabaseIfNotExists<TeamContext>
    {
        protected override void Seed(TeamContext context)
        {
            var teams = new List<Team>
         {
             new Team{Name="Adventure Works Cycles"},
             new Team{Name="Alpine Ski House"},
             new Team{Name="Blue Yonder Airlines"},
             new Team{Name="Coho Vineyard"},
             new Team{Name="Contoso, Ltd."},
             new Team{Name="Fabrikam, Inc."},
             new Team{Name="Lucerne Publishing"},
             new Team{Name="Northwind Traders"},
             new Team{Name="Consolidated Messenger"},
             new Team{Name="Fourth Coffee"},
             new Team{Name="Graphic Design Institute"},
             new Team{Name="Nod Publishers"}
         };

            Team.PlayGames(teams);

            teams.ForEach(t => context.Teams.Add(t));
            context.SaveChanges();
        }
    }
}