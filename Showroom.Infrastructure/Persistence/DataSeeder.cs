using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Showroom.Domain.Entities;
using Showroom.Shared;

namespace Showroom.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static async Task CreateRolesAndAdminUser(IServiceProvider sp, ApplicationDbContext context)
        {
            var roleManager = sp.GetService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = RoleConstants.Administrator
                });
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = RoleConstants.Manager
                });
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = RoleConstants.Consultant
                });
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = RoleConstants.Client
                });
            }

            var userManager = sp.GetService<UserManager<User>>();

            if (userManager.Users.Any())
            {
                return;
            }


            if (await userManager.FindByNameAsync("admin") == null)
            {
                var userProfile = new UserProfile()
                {
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    Email = "admin@admin.com",
                    PhoneNumber = "",
                    OrganizationId = null
                };

                var entry = context.Add(userProfile);

                await context.SaveChangesAsync();

                var adminUser = new User
                {
                    Email = "admin@admin.com",
                    UserName = "admin",
                    Name = "Administrator",
                    Profile = entry.Entity
                };

                var results = await userManager.CreateAsync(adminUser, "Abc123!");

                await userManager.AddToRoleAsync(adminUser, RoleConstants.Administrator);
            }
        }

        public static async Task SeedDevData(IServiceProvider sp, ApplicationDbContext context)
        {
            var userManager = sp.GetService<UserManager<User>>();

            if (await userManager.FindByNameAsync("daniel.aberg@se") == null)
            {
                var managerProfile = new ManagerProfile()
                {
                    FirstName = "Daniel",
                    LastName = "Åberg",
                    Headline = "",
                    Title = "Consultant Manager",
                    ShortPresentation = "Long experience within the IT field. Tech-savy.",
                    Presentation = "",
                    Address = new Address()
                    {
                        Address1 = "Skeppsbron 7",
                        PostalCode = "211 20",
                        City = "Malmö",
                        Country = "Sweden"
                    },
                    Email = "daniel.aberg@se",
                    PhoneNumber = "0703-790997",
                    ProfileImage = "/images/daniel-aberg.jpeg",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id
                };

                var entry = context.Add(managerProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = managerProfile.Email,
                    UserName = managerProfile.Email,
                    Name = $"{managerProfile.FirstName} {managerProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Manager);
            }

            if (await userManager.FindByNameAsync("jorgen.nilsson@se") == null)
            {
                var managerProfile = new ManagerProfile()
                {
                    FirstName = "Jörgen",
                    LastName = "Nilsson",
                    Email = "jorgen.nilsson@se",
                    ProfileImage = "/images/jorgen-nilsson.jpeg",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id
                };

                var entry = context.Add(managerProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = managerProfile.Email,
                    UserName = managerProfile.Email,
                    Name = $"{managerProfile.FirstName} {managerProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Manager);
            }

            if (await userManager.FindByNameAsync("mikael.hall@se") == null)
            {
                var managerProfile = new ManagerProfile()
                {
                    FirstName = "Mikael",
                    LastName = "Häll",
                    Email = "mikael.hall@se",
                    ProfileImage = "/images/mikael-hall.jpeg",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id
                };

                var entry = context.Add(managerProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = managerProfile.Email,
                    UserName = managerProfile.Email,
                    Name = $"{managerProfile.FirstName} {managerProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Manager);
            }

            if (await userManager.FindByNameAsync("marcus.ekerhult@se") == null)
            {
                var managerProfile = new ManagerProfile()
                {
                    FirstName = "Marcus",
                    LastName = "Ekerhult",
                    Title = "Consultant Manager",
                    Address = new Address()
                    {
                        Address1 = "Alfagatan 7",
                        PostalCode = "431 49",
                        City = "Mölndal",
                        Country = "Sweden"
                    },
                    Email = "marcus.ekerhult@se",
                    PhoneNumber = "(+46) 702-907879",
                    ProfileImage = "/images/marcus-ekerhult.jpeg",
                    OrganizationId = (await context.Organizations.FindAsync("vast")).Id
                };

                var entry = context.Add(managerProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = managerProfile.Email,
                    UserName = managerProfile.Email,
                    Name = $"{managerProfile.FirstName} {managerProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Manager);
            }

            if (await userManager.FindByNameAsync("sofie.bondeson") == null)
            {
                var consultantProfile = new ConsultantProfile()
                {
                    FirstName = "Sofie",
                    LastName = "Bondeson",
                    Email = "sofie.bondeson@se",
                    PhoneNumber = "",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id,
                    ManagerId = (await context.UserProfiles.FirstAsync(x => x.Email == "daniel.aberg@se")).Id,
                    CompetenceAreaId = (await context.CompetenceAreas.FindAsync("project-management")).Id,
                    Headline = "Project Manager",
                    Presentation = "",
                    ProfileImage = "/images/sofie-bondeson.jpeg",
                    ProfileVideo = "/videos/SampleVideo_720x480_5mb.mp4"
                };

                var entry = context.Add(consultantProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = consultantProfile.Email,
                    UserName = "sofie.bondeson",
                    Name = $"{consultantProfile.FirstName} {consultantProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Consultant);
            }

            if (await userManager.FindByNameAsync("nermin.paratusic") == null)
            {
                var consultantProfile = new ConsultantProfile()
                {
                    FirstName = "Nermin",
                    LastName = "Paratusic",
                    Email = "nermin.paratusic@se",
                    PhoneNumber = "(+46)73 891 33 21",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id,
                    ManagerId = (await context.UserProfiles.FirstAsync(x => x.Email == "daniel.aberg@se")).Id,
                    CompetenceAreaId = (await context.CompetenceAreas.FindAsync("software")).Id,
                    Headline = "Senior .NET Developer",
                    Presentation = "Nermin is an experienced .NET developer who has been working on both desktop and web applications. he also got experience in SharePoint.",
                    ProfileImage = "/images/nermin-paratusic.jpeg",
                    ProfileVideo = "/videos/SampleVideo_720x480_5mb.mp4"
                };

                var entry = context.Add(consultantProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = consultantProfile.Email,
                    UserName = "nermin.paratusic",
                    Name = $"{consultantProfile.FirstName} {consultantProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Consultant);
            }


            if (await userManager.FindByNameAsync("christian.nilsson") == null)
            {
                var consultantProfile = new ConsultantProfile()
                {
                    FirstName = "Christian",
                    LastName = "Nilsson",
                    Email = "christian.nilsson@se",
                    PhoneNumber = "",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id,
                    ManagerId = (await context.UserProfiles.FirstAsync(x => x.Email == "daniel.aberg@se")).Id,
                    CompetenceAreaId = (await context.CompetenceAreas.FindAsync("software")).Id,
                    Headline = "Senior .NET Developer",
                    Presentation = "",
                    ProfileImage = "/images/christian-nilsson.jpeg",
                    ProfileVideo = "/videos/SampleVideo_720x480_5mb.mp4"
                };

                var entry = context.Add(consultantProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = consultantProfile.Email,
                    UserName = "christian.nilsson",
                    Name = $"{consultantProfile.FirstName} {consultantProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Consultant);
            }

            if (await userManager.FindByNameAsync("niklas.martensson") == null)
            {
                var consultantProfile = new ConsultantProfile()
                {
                    FirstName = "Niklas",
                    LastName = "Mårtensson",
                    Email = "niklas.martensson@se",
                    PhoneNumber = "",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id,
                    ManagerId = (await context.UserProfiles.FirstAsync(x => x.Email == "mikael.hall@se")).Id,
                    CompetenceAreaId = (await context.CompetenceAreas.FindAsync("electronics")).Id,
                    Headline = "Senior Electronics Engineer",
                    Presentation = "",
                    ProfileImage = "/images/niklas-martensson.jpeg",
                    ProfileVideo = "/videos/SampleVideo_720x480_5mb.mp4"
                };

                var entry = context.Add(consultantProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = consultantProfile.Email,
                    UserName = "niklas.martensson",
                    Name = $"{consultantProfile.FirstName} {consultantProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Consultant);
            }

            if (await userManager.FindByNameAsync("johanna.sjoberg") == null)
            {
                var consultantProfile = new ConsultantProfile()
                {
                    FirstName = "Johanna",
                    LastName = "Sjöberg",
                    Email = "johanna.sjoberg@se",
                    PhoneNumber = "",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id,
                    ManagerId = (await context.UserProfiles.FirstAsync(x => x.Email == "mikael.hall@se")).Id,
                    CompetenceAreaId = (await context.CompetenceAreas.FindAsync("mechanics")).Id,
                    Headline = "Junior Mechanical Engineer",
                    Presentation = "",
                    ProfileImage = "/images/johanna-sjoberg.jpeg",
                    ProfileVideo = "/videos/SampleVideo_720x480_5mb.mp4"
                };

                var entry = context.Add(consultantProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = consultantProfile.Email,
                    UserName = "johanna.sjoberg",
                    Name = $"{consultantProfile.FirstName} {consultantProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Consultant);
            }

            if (await userManager.FindByNameAsync("per.nilsson@borgwarner.com") == null)
            {
                var consultantProfile = new ClientProfile()
                {
                    FirstName = "Per",
                    LastName = "Nilsson",
                    Email = "per.nilsson@borgwarner.com",
                    Title = "Line Manager",
                    Company = "BorgWarner Sweden AB",
                    Section = "Rotating Electric Lines",
                    Address = new Address()
                    {
                        Address1 = "Instrumentgatan 15",
                        PostalCode = "261 51",
                        City = "Landskrona",
                        State = "Skåne",
                        Country = "Sweden"
                    },
                    PhoneNumber = "070-244 34 22",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id,
                    ReferenceId = (await context.UserProfiles.FirstAsync(x => x.Email == "mikael.hall@se")).Id,
                };

                var entry = context.Add(consultantProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = consultantProfile.Email,
                    UserName = consultantProfile.Email,
                    Name = $"{consultantProfile.FirstName} {consultantProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Client);
            }

            if (await userManager.FindByNameAsync("maria.sjolund@axis.com") == null)
            {
                var consultantProfile = new ClientProfile()
                {
                    FirstName = "Maria",
                    LastName = "Sjölund",
                    Email = "maria.sjolund@axis.com",
                    Title = "R&D Director",
                    Section = "Camera Solutions",
                    Department = "New Business",
                    Company = "Axis Communications AB",
                    Address = new Address()
                    {
                        Address1 = "Emdalavägen 14",
                        PostalCode = "223 69",
                        City = "Lund",
                        State = "Skåne",
                        Country = "Sweden"
                    },
                    PhoneNumber = "070-422 72 35",
                    OrganizationId = (await context.Organizations.FindAsync("syd")).Id,
                    ReferenceId = (await context.UserProfiles.FirstAsync(x => x.Email == "daniel.aberg@se")).Id,
                };

                var entry = context.Add(consultantProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = consultantProfile.Email,
                    UserName = consultantProfile.Email,
                    Name = $"{consultantProfile.FirstName} {consultantProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Client);
            }

            if (await userManager.FindByNameAsync("anders.bengtsson@volvocars.com") == null)
            {
                var consultantProfile = new ClientProfile()
                {
                    FirstName = "Anders",
                    LastName = "Bengtsson",
                    Email = "anders.bengtsson@volvocars.com",
                    Title = "Line Manager",
                    Section = "Core System Platform",
                    Department = "Software",
                    Company = "Volvo Cars Group AB",
                    PhoneNumber = "070-44 71 325",
                    OrganizationId = (await context.Organizations.FindAsync("vast")).Id,
                    ReferenceId = (await context.UserProfiles.FirstAsync(x => x.Email == "marcus.ekerhult@se")).Id,
                };

                var entry = context.Add(consultantProfile);

                await context.SaveChangesAsync();

                var user = new User
                {
                    Email = consultantProfile.Email,
                    UserName = consultantProfile.Email,
                    Name = $"{consultantProfile.FirstName} {consultantProfile.LastName}",
                    ProfileId = entry.Entity.Id
                };

                var results = await userManager.CreateAsync(user, "Abc123!");

                await userManager.AddToRoleAsync(user, RoleConstants.Client);
            }
        }

        public static void SeedCompetenceAreas(ApplicationDbContext context)
        {
            if (!context.CompetenceAreas.Any())
            {
                var competenceAreas = new List<CompetenceArea>
                {
                    new CompetenceArea { Id = "mechanics", Name = "Mechanics" },
                    new CompetenceArea { Id = "electronics", Name = "Electronics" },
                    new CompetenceArea { Id = "software", Name = "Software" },
                    new CompetenceArea { Id = "project-management", Name = "Project Management" },
                    new CompetenceArea { Id = "quality-assurance", Name = "Quality Assurance (QA)" }
                };
                context.AddRange(competenceAreas);
                context.SaveChanges();
            }
        }

        public static void SeedOrganizationsAreas(ApplicationDbContext context)
        {
            if (!context.Organizations.Any())
            {
                var organizations = new List<Organization>
                {
                    new Organization { Id = "vast", Name = "Väst" },
                    new Organization { Id = "ost", Name = "Öst" },
                    new Organization { Id = "syd", Name = "Syd" },
                };
                context.AddRange(organizations);
                context.SaveChanges();
            }
        }

        public static void SeedUserProfileLinkTypes(ApplicationDbContext context)
        {
            if (!context.UserProfileLinkTypes.Any())
            {
                var userProfileLinkTypes = new List<UserProfileLinkType>
                {
                    new UserProfileLinkType { Name = "Homepage" },
                    new UserProfileLinkType { Name = "LinkedIn" },
                    new UserProfileLinkType { Name = "GitHub" },
                    new UserProfileLinkType { Name = "Twitter" },
                    new UserProfileLinkType { Name = "YouTube" },
                    new UserProfileLinkType { Name = "Twitch" },
                    new UserProfileLinkType { Name = "Other" }
                };
                context.AddRange(userProfileLinkTypes);
                context.SaveChanges();
            }
        }
    }
}
