using BlazorApp.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp.Client.Pages
{
    public partial class Index
    {

        private MaterEmergencyCareCentre mecc = new MaterEmergencyCareCentre();

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = true;
        private bool siteLoaded = false;

        string[] headings = { "Bed", "Status", "Patient", "DOB", "Presenting Issue", "Last comment", "Last update", "Nurse", "Action" };

        protected override async Task OnInitializedAsync()
        {
            GetSummaryInformation();
            await Task.Yield();
        }

        public async void GetSummaryInformation()
        {

            siteLoaded = false;
            try
            {
                mecc = await Http.GetFromJsonAsync<MaterEmergencyCareCentre>("/api/SummaryInformation") ?? new MaterEmergencyCareCentre();
                mecc.Beds = await Http.GetFromJsonAsync<List<Bed>>("/api/Beds") ?? new List<Bed>();
                mecc.Patients = await Http.GetFromJsonAsync<List<Patient>>("/api/Patients") ?? new List<Patient>();

                // marry beds to patients
                foreach (var bed in mecc.Beds)
                {
                    foreach (var patient in mecc.Patients)
                    {
                        if (patient.PartitionKey == bed.PartitionKey)
                        {
                            //marry a bed to a patient
                            bed.Patient = patient;
                            bed.PatientURN = patient.URN;

                        }
                    }
                }

                // set the status for the beds
                foreach (var bed in mecc.Beds)
                {
                    if (bed.PatientURN != "" && bed.PatientURN != null && bed.PatientURN.Count() > 2)
                    {

                        bed.Status = false;
                        try
                        {
                            var comments = await localStorage.GetItemAsync<string>(bed.PatientURN);
                            List<Comment>? patientCommentHistory = JsonSerializer.Deserialize<List<Comment>>(comments);
                            bed.LastComment = patientCommentHistory.Last().CommentDescription;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                    }
                }




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            siteLoaded = true;
            StateHasChanged();
        }
        public void GetPatientDetails(string bedId)
        {
            NavigationManager.NavigateTo("/patient-details/" + bedId);
        }

        public void AdmitNewPatient(string bedId)
        {
            NavigationManager.NavigateTo("/admit/" + bedId);
            // add comment 'Admitted'
        }

        public void AddComment(string bedId)
        {
            NavigationManager.NavigateTo("/patient-details/" + bedId);
        }

        public async Task DischargePatient(string bedId)
        {
            Console.WriteLine("Discharge Patient Function Called");

            try
            {
                string query = "/api/DischargePatient/" + bedId;
                var result = await Http.GetAsync(query);
                GetSummaryInformation();

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
        }
    }
}
