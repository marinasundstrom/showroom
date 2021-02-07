using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Showroom.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetenceAreas",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Address_Address1 = table.Column<string>(nullable: true),
                    Address_Address2 = table.Column<string>(nullable: true),
                    Address_PostalCode = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_State = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileLinkTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileLinkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ProfileImage = table.Column<string>(nullable: true),
                    ProfileVideo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Address_Address1 = table.Column<string>(nullable: true),
                    Address_Address2 = table.Column<string>(nullable: true),
                    Address_PostalCode = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_State = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ReferenceId = table.Column<Guid>(nullable: true),
                    CompetenceAreaId = table.Column<string>(nullable: true),
                    Headline = table.Column<string>(nullable: true),
                    ShortPresentation = table.Column<string>(nullable: true),
                    Presentation = table.Column<string>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: true),
                    AvailableFromDate = table.Column<DateTime>(nullable: true),
                    ManagerProfile_Headline = table.Column<string>(nullable: true),
                    ManagerProfile_ShortPresentation = table.Column<string>(nullable: true),
                    ManagerProfile_Presentation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserProfiles_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_CompetenceAreas_CompetenceAreaId",
                        column: x => x.CompetenceAreaId,
                        principalTable: "CompetenceAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserProfiles_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Organizations_OrganizationId1",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Organizations_OrganizationId2",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    ProfileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientCases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    ClientProfileId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    DateOpened = table.Column<DateTime>(nullable: false),
                    DateClosed = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCases_UserProfiles_ClientProfileId",
                        column: x => x.ClientProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCases_UserProfiles_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientConsultantInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    ConsultantId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientConsultantInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientConsultantInterests_UserProfiles_ClientId",
                        column: x => x.ClientId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientConsultantInterests_UserProfiles_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagerCompetenceAreas",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    CompetenceAreaId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerCompetenceAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagerCompetenceAreas_CompetenceAreas_CompetenceAreaId",
                        column: x => x.CompetenceAreaId,
                        principalTable: "CompetenceAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManagerCompetenceAreas_UserProfiles_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileLink",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    LinkTypeId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileLink_UserProfileLinkTypes_LinkTypeId",
                        column: x => x.LinkTypeId,
                        principalTable: "UserProfileLinkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfileLink_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultantRecommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    ConsultantId = table.Column<Guid>(nullable: false),
                    ClientCaseId = table.Column<Guid>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DateViewedByClient = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultantRecommendations_ClientCases_ClientCaseId",
                        column: x => x.ClientCaseId,
                        principalTable: "ClientCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultantRecommendations_UserProfiles_ClientId",
                        column: x => x.ClientId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultantRecommendations_UserProfiles_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultantRecommendations_UserProfiles_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileId",
                table: "AspNetUsers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCases_ClientProfileId",
                table: "ClientCases",
                column: "ClientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCases_ManagerId",
                table: "ClientCases",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientConsultantInterests_ClientId",
                table: "ClientConsultantInterests",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientConsultantInterests_ConsultantId",
                table: "ClientConsultantInterests",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantRecommendations_ClientCaseId",
                table: "ConsultantRecommendations",
                column: "ClientCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantRecommendations_ClientId",
                table: "ConsultantRecommendations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantRecommendations_ConsultantId",
                table: "ConsultantRecommendations",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantRecommendations_ManagerId",
                table: "ConsultantRecommendations",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerCompetenceAreas_CompetenceAreaId",
                table: "ManagerCompetenceAreas",
                column: "CompetenceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerCompetenceAreas_ManagerId",
                table: "ManagerCompetenceAreas",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileLink_LinkTypeId",
                table: "UserProfileLink",
                column: "LinkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileLink_UserProfileId",
                table: "UserProfileLink",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_OrganizationId",
                table: "UserProfiles",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ReferenceId",
                table: "UserProfiles",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CompetenceAreaId",
                table: "UserProfiles",
                column: "CompetenceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ManagerId",
                table: "UserProfiles",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_OrganizationId1",
                table: "UserProfiles",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_OrganizationId2",
                table: "UserProfiles",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClientConsultantInterests");

            migrationBuilder.DropTable(
                name: "ConsultantRecommendations");

            migrationBuilder.DropTable(
                name: "ManagerCompetenceAreas");

            migrationBuilder.DropTable(
                name: "UserProfileLink");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClientCases");

            migrationBuilder.DropTable(
                name: "UserProfileLinkTypes");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "CompetenceAreas");
        }
    }
}
