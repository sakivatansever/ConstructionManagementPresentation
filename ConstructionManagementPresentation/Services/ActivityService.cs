using ConstructionManagementPresentation.Models;
using System.Collections.Generic;
using System.Net.Http;


namespace ConstructionManagementPresentation.Services
{
    public class ActivityService
    {
        private readonly  HttpClient _httpClient;

        public ActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Activity>?> GetAllActivitiesAsync()
        {

            return await _httpClient.GetFromJsonAsync<List<Activity>>("activities/GetAllActivities");


        }

        public async Task<Activity?> GetActivityByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Activity>($"activities/GetActivityById/{id}");
        }

        public async Task<Activity?> CreateActivityAsync(Activity activity)
        {
            var response = await _httpClient.PostAsJsonAsync("activities/CreateActivity", activity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Activity>();
        }

        public async Task<Activity?> UpdateActivityAsync(int id, Activity activity)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"activities/UpdateActivity/{id}", activity);

               
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Activity>();
                }

              
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error updating activity: {response.StatusCode}, {errorContent}");

                return null; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred during update: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteActivityAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"activities/DeleteActivity/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
