using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using AutoMapper;

using Showroom.Client.Services;
using Showroom.Server.Client;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Showroom.Client.Pages
{
    public partial class ClientPage : ComponentBase
    {
        private Task task;
        private UserProfile userProfile;
        private ClientProfileViewModel client;
        private IEnumerable<ManagerProfile> managers;
        private IEnumerable<Organization> organizations;
        private bool saved = false;
        private string error = string.Empty;

        [Inject]
        public IIdentityService IdentityService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IClientProfilesClient ClientProfilesClient { get; set; }

        [Inject]
        public IOrganizationsClient OrganizationsClient { get; set; }

        [Inject]
        public IManagersClient ManagersClient { get; set; }

        [Inject]
        public IJSHelpers JSHelpers { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override void OnInitialized()
        {
            task = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            userProfile = await IdentityService.GetUserProfileAsync();

            if (!string.IsNullOrEmpty(Id))
            {
                if (Guid.TryParse(Id, out var id))
                {
                    try
                    {
                        client = Mapper.Map<UpdateClientProfile>(
                            await ClientProfilesClient.GetClientProfileAsync(Guid.Parse(Id)));
                        if (client.Address == null)
                        {
                            client.Address = new Address();
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
                    // Handle expected guid
                }
            }
            else
            {
                client = new AddClientProfile() { Address = new Address() };
            }

            try
            {
                organizations = await OrganizationsClient.GetOrganizationsAsync();
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
                managers = await ManagersClient.GetManagersAsync(null);
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

        private async Task SubmitForm()
        {
            saved = false;
            error = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    await ClientProfilesClient.CreateClientProfileAsync(client as AddClientProfile);

                    NavigationManager.NavigateTo("/clients");
                }
                else
                {
                    await ClientProfilesClient.UpdateClientProfileAsync(Guid.Parse(Id), client as UpdateClientProfile);

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


        private string imageSource;
        private bool videoSaved;
        private string fileName;
        private Stream stream;

        private async Task SubmitProfileImageForm(IBrowserFile file)
        {
            videoSaved = false;

            fileName = file.Name;
            stream = file.OpenReadStream();

            imageSource = Base64ImageEncoder.EncodeImage(stream, file.ContentType);

            try
            {
                //var imageUrl = await ClientProfilesClient.UploadProfileImageAsync(new FileParameter(stream, fileName));

                //client.ProfileImage = imageUrl;

                videoSaved = true;

                await imageUploadModal.Close();
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
    }
}
