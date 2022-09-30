using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp.Client.Pages
{
	public partial class PatientDetailPage
	{

        [Parameter]
        public string? PathParam { get; set; }

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = true;

        private bool siteLoaded = false;

        public string? comment { get; set; }

        Patient patient = new Patient();
        List<Comment>? patientCommentHistory = new List<Comment>();

        string[] headings = { "Date", "Time", "Nurse", "Comment" };

        private void NavigateToHomePage()
        {
            NavigationManager.NavigateTo("/");
        }

        private async void AddComment()
        {
            Comment commentObj = new Comment();

            commentObj.CommentDescription = comment;
            commentObj.NurseAllocated = patient.NurseAllocated;
            commentObj.DateTimeOfComment = DateTime.Now;
            patient.CommentHistory.Add(commentObj);

            await localStorage.SetItemAsync(patient.URN, patient.CommentHistory);

            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                List<Patient> patients = await Http.GetFromJsonAsync<List<Patient>>("/api/Patients") ?? new List<Patient>();

                foreach (var patient in patients)
                {
                    if (patient.PartitionKey == PathParam)
                    {
                        this.patient = patient;
                        if (patient.CommentHistory == null)
                        {
                            patient.CommentHistory = new List<Comment>();
                        }
                    }
                }

                var comments = await localStorage.GetItemAsync<string>(patient.URN);
                patient.CommentHistory = JsonSerializer.Deserialize<List<Comment>>(comments);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            siteLoaded = true;
            StateHasChanged();
        }

    }
}
