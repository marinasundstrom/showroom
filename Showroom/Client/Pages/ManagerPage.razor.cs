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
    public partial class ManagerPage : ComponentBase
    {
        private UserProfile userProfile;
        private Task task;
        private ManagerProfileViewModel manager;
        private IEnumerable<Organization> organizations;
        private bool saved = false;
        private string error = string.Empty;

        [Inject]
        public IIdentityService IdentityService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

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
            task = OnInitialize();
        }

        private async Task OnInitialize()
        {
            userProfile = await IdentityService.GetUserProfileAsync();

            if (!string.IsNullOrEmpty(Id))
            {
                if (Guid.TryParse(Id, out var id))
                {
                    try
                    {
                        manager = Mapper.Map<UpdateManagerProfile>(await ManagersClient.GetManagerAsync(id));
                        if (manager.Address == null)
                        {
                            manager.Address = new Address();
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
                manager = new AddManagerProfile() { Address = new Address() };
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
        }

        private async Task SubmitForm()
        {
            saved = false;
            error = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    await ManagersClient.CreateManagerAsync(manager as AddManagerProfile);

                    NavigationManager.NavigateTo("/managers");
                }
                else
                {
                    await ManagersClient.UpdateManagerAsync(Guid.Parse(Id), manager as UpdateManagerProfile);
                }

                saved = true;
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

            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    var imageUrl = await ManagersClient.UploadProfileImageAsync(Guid.Parse(Id), new FileParameter(stream, fileName));

                    manager.ProfileImage = imageUrl;

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
}
