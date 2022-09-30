using BlazorApp.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BlazorApp.Client.Pages
{
    public partial class AdmitPatientPage
    {

        [Parameter]
        public string? BedId { get; set; }

        Patient NewPatient = new Patient();

        bool success;


        private void NavigateToHomePage()
        {
            NewPatient = new Patient();
            NavigationManager.NavigateTo("/");
        }

        private async void OnValidSubmit(EditContext context)
        {
            success = true;

            // Upsert the Patient (currently supports 8 patients)
            try
            {
                NewPatient.PartitionKey = BedId;
                NewPatient.RowKey = "000" + BedId?.ToString();

                string json = JsonSerializer.Serialize(NewPatient);
                StringContent content = new StringContent(json);
                var result = await Http.PostAsync("/api/UpdatePatient", content);
                NavigationManager.NavigateTo("/");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            StateHasChanged();
        }
    }
}
