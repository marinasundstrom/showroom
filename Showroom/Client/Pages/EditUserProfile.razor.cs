using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Showroom.Client.Services;
using Showroom.Server.Client;

namespace Showroom.Client.Pages
{
    public partial class EditUserProfile : ComponentBase
    {
        private Task task;
        private UserProfileViewModel user;
        private bool saved = false;
        private string error = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUserClient UserClient { get; set; }

        [Inject]
        public IJSHelpers JSHelpers { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        protected override void OnInitialized()
        {
            task = OnInitialize();
        }

        private async Task OnInitialize()
        {
            try
            {
                var result = await UserClient.GetUserProfileAsync(null);

                switch (result)
                {
                    case ClientProfile cp:
                        user = Mapper.Map<UpdateClientProfile>(cp);
                        break;

                    case ConsultantProfile cp:
                        user = Mapper.Map<UpdateConsultantProfile>(cp);
                        break;

                    case ManagerProfile cp:
                        user = Mapper.Map<UpdateManagerProfile>(cp);
                        break;

                    default:
                        user = Mapper.Map<UpdateUserProfile>(result);
                        break;
                }

                if (user.Address == null)
                {
                    user.Address = new Address();
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

        private async Task SubmitForm()
        {
            saved = false;
            error = string.Empty;

            try
            {
                await UserClient.UpdateUserProfileAsync(user);

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

            try
            {
                var imageUrl = await UserClient.UploadProfileImageAsync(new FileParameter(stream, fileName));

                user.ProfileImage = imageUrl;

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
