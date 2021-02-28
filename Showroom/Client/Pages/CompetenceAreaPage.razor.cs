using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Showroom.Client.Services;
using Showroom.Server.Client;

namespace Showroom.Client.Pages
{
    public partial class CompetenceAreaPage : ComponentBase
    {
        Task task;
        CompetenceArea competenceArea;
        bool saved = false;
        string error = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ICompetenceAreasClient CompetenceAreasClient { get; set; }

        [Inject]
        public IJSHelpers JSHelpers { get; set; }


        [Parameter]
        public string Id { get; set; }

        protected override void OnInitialized()
        {
            task = Initialize();
        }

        private async Task Initialize()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    competenceArea = await CompetenceAreasClient.GetCompetenceAreaAsync(Id);
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
                competenceArea = new CompetenceArea();
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
                    await CompetenceAreasClient.CreateCompetenceAreaAsync(competenceArea);

                    NavigationManager.NavigateTo("/competenceareas");
                }
                else
                {
                    await CompetenceAreasClient.UpdateCompetenceAreaAsync(Id, competenceArea);

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
