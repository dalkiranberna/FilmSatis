namespace MVCFilmSatis.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using MVCFilmSatis.Models;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCFilmSatis.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MVCFilmSatis.Models.ApplicationDbContext context)
        {
			if(!context.Users.Any(x => x.Email == "admin@a.com")) //bu kiþi yoksa oluþtur
			{
				Employee e1 = new Employee();
				e1.Email = "admin@a.com";
				e1.UserName = "admin@a.com";
				e1.ReportsToEmail = "admin@a.com";
				e1.Age = 24;
				e1.Gender = Gender.Female;

				IdentityRole role = new IdentityRole();
				role.Name = "Administrator";
				context.Roles.Add(role);
				context.SaveChanges();
                
				UserStore<Employee> employeeStore = new UserStore<Employee>(context); 
				UserManager<Employee> employeeManager = new UserManager<Employee>(employeeStore);

				employeeManager.Create(e1, "Aa123456!");
				employeeManager.AddToRole(e1.Id, "Administrator");

				context.SaveChanges();
			}
        }
    }
}
