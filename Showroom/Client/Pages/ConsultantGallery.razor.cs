using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Showroom.Client.Models;
using Showroom.Server.Client;

using Microsoft.AspNetCore.Components;

namespace Showroom.Client.Pages
{
    public partial class ConsultantGallery : ComponentBase
    {
        private Task task;
        private ListItem competenceArea;
        private DateTime? availableFrom;
        private Models.ListItem organization;
        private IEnumerable<ListItem> competenceAreas;
        private IEnumerable<Models.ListItem> organizations;
        private List<ProfileShort> consultants;
        private int pageNumber = 0;
        private readonly int numberOfItemsPerPage = 3;
        private long total;

        private async Task ClearFilter()
        {
            competenceArea = competenceAreas.FirstOrDefault();
            availableFrom = null;
            organization = organizations.FirstOrDefault();
            await UpdateFilter();
        }

        private void OnCardClick(ConsultantProfile consultantProfile)
        {
            NavigationManager.NavigateTo($"/consultants/{consultantProfile.Id}");
        }

        protected override void OnInitialized()
        {
            task = OnInitialize();
        }

        private async Task OnInitialize()
        {
            try
            {
                var foo = Mapper.ProjectTo<Models.ListItem>((
                await CompetenceAreasClient.GetCompetenceAreasAsync()).AsQueryable()).ToList();

                foo.Insert(0, new Models.ListItem() { Id = null, Name = "All" });

                competenceAreas = foo;

                competenceArea = competenceAreas.FirstOrDefault();
            }
            /* catch (ApiException exc)
            {
            }
            catch (HttpRequestException exc)
            {
            } */
            catch (Exception exc)
            {
                await JSHelpers.Alert(exc.Message);
            }

            try
            {
                var foo = Mapper.ProjectTo<Models.ListItem>((
                    await OrganizationsClient.GetOrganizationsAsync()).AsQueryable()).ToList();

                foo.Insert(0, new Models.ListItem() { Id = null, Name = "All" });

                organizations = foo;

                organization = organizations.FirstOrDefault();

            }
            /*catch (ApiException exc)
            {

            }
            catch (HttpRequestException exc)
            {

            }*/
            catch (Exception exc)
            {
                await JSHelpers.Alert(exc.Message);
            }

            try
            {
                var result = await ConsultantGalleryClient.GetConsultantsAsync(pageNumber++, numberOfItemsPerPage, null, null, null);
                consultants = new List<ProfileShort>();
                consultants.AddRange(result.Items);
                total = result.TotalItems;
            }
            /*catch (ApiException exc)
            {

            }
            catch (HttpRequestException exc)
            {

            }*/
            catch (Exception exc)
            {
                await JSHelpers.Alert(exc.Message);
            }
        }

        private async Task UpdateFilter()
        {
            pageNumber = 0;
            var result = await ConsultantGalleryClient.GetConsultantsAsync(pageNumber++, numberOfItemsPerPage, organization.Id, competenceArea.Id, availableFrom);
            consultants.Clear();
            consultants.AddRange(result.Items);
            total = result.TotalItems;
        }

        private async Task LoadMore()
        {
            var result = await ConsultantGalleryClient.GetConsultantsAsync(pageNumber++, numberOfItemsPerPage, organization.Id, competenceArea.Id, availableFrom);
            consultants.AddRange(result.Items);
            total = result.TotalItems;

            await JSHelpers.ScrollToBottom();
        }
    }
}
