
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
    public partial class ConsultantPage : ComponentBase
    {
        private UserProfile userProfile;
        private ConsultantProfileViewModel consultant;
        private IEnumerable<CompetenceArea> competenceAreas;
        private IEnumerable<Organization> organizations;
        private IEnumerable<ManagerProfile> managers;
        private bool saved = false;
        private string error = string.Empty;
        private Task task;

        [Inject]
        public IIdentityService IdentityService { get; set; }

        [Inject]
        public IConsultantProfilesClient ConsultantProfilesClient { get; set; }

        [Inject]
        public ICompetenceAreasClient CompetenceAreasClient { get; set; }

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
                        consultant = Mapper.Map<UpdateConsultantProfile>(
                            await ConsultantProfilesClient.GetConsultantProfileAsync(id));

                        if (consultant.Address == null)
                        {
                            consultant.Address = new Address();
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
                consultant = new AddConsultantProfile() { Address = new Address() };
            }

            try
            {
                competenceAreas = await CompetenceAreasClient.GetCompetenceAreasAsync();
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
                    var foo = await ConsultantProfilesClient.CreateConsultantProfileAsync(consultant as AddConsultantProfile);

                    consultant = Mapper.Map<UpdateConsultantProfile>(foo);

                    Id = foo.Id.ToString();

                    try
                    {
                        if (stream != null)
                        {
                            var imageUrl = await ConsultantProfilesClient.UploadProfileImageAsync(Guid.Parse(Id), new FileParameter(stream, fileName));

                            consultant.ProfileImage = imageUrl;

                            videoSaved = true;
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
                    await ConsultantProfilesClient.UpdateConsultantProfileAsync(Guid.Parse(Id), consultant as UpdateConsultantProfile);
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

        private bool videoSaved;

        private async Task SubmitVideoForm(IBrowserFile file)
        {
            videoSaved = false;

            try
            {
                var videoUrl = await ConsultantProfilesClient.UploadVideoAsync(Guid.Parse(Id), new FileParameter(file.OpenReadStream(), file.Name));

                consultant.ProfileVideo = videoUrl;

                videoSaved = true;

                await videoUploadModal.Close();
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

        private string imageSource;
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
                    var imageUrl = await ConsultantProfilesClient.UploadProfileImageAsync(Guid.Parse(Id), new FileParameter(stream, fileName));

                    consultant.ProfileImage = imageUrl;

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
