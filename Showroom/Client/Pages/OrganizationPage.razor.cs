
using System;
using System.Threading.Tasks;

using Showroom.Client.Services;
using Showroom.Server.Client;

using Microsoft.AspNetCore.Components;

namespace Showroom.Client.Pages
{
    public partial class OrganizationPage : ComponentBase
    {
        Task task;
        Organization organization;
        bool saved = false;
        string error = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IOrganizationsClient OrganizationsClient { get; set; }

        [Inject]
        public IJSHelpers JSHelpers { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override void OnInitialized()
        {
            task = OnInitalize();
        }

        private async Task OnInitalize()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    organization = await OrganizationsClient.GetOrganizationAsync(Id);
                    if (organization.Address == null)
                    {
                        organization.Address = new Address();
                    }
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
            }
            else
            {
                organization = new Organization() { Address = new Address() };
            }
        }

        async Task SubmitForm()
        {
            saved = false;
            error = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    await OrganizationsClient.CreateOrganizationAsync(organization);

                    NavigationManager.NavigateTo("/organizations");
                }
                else
                {
                    await OrganizationsClient.UpdateOrganizationAsync(Id, organization);

                    saved = true;
                }
            }
            /* catch (ApiException exc)
            {
            }
            catch (HttpRequestException exc)
            {
            } */
            catch (Exception exc)
            {
                saved = false;
                error = exc.Message;
            }
        }

    }
}
