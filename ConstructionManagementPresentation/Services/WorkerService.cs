using ConstructionManagementPresentation.Models;
using System.Text.Json;

namespace ConstructionManagementPresentation.Services
{
    public class WorkerService
    {
        private readonly HttpClient _httpClient;

        public WorkerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Worker>> GetAllWorkersAsync()
        {
            try
            {
               
                var response = await _httpClient.GetAsync("/workers/getallworkers");

                
                response.EnsureSuccessStatusCode();

                
              
                var workers = await response.Content.ReadFromJsonAsync<List<Worker>>();

                return workers;
            }
            catch (HttpRequestException httpEx)
            {
               

                throw new Exception("İşçiler alınırken bir hata oluştu.", httpEx);
            }
            catch (JsonException jsonEx)
            {
            

                throw new Exception("İşçilerin verileri deseralize edilemedi.", jsonEx);
            }

        }

        public async Task<Worker> GetWorkerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Worker>($"workers/GetWorkerById/{id}");
        }

        public async Task<Worker> CreateWorkerAsync(Worker worker)
        {
            var response = await _httpClient.PostAsJsonAsync("workers", worker);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Worker>();
        }

        public async Task<bool> UpdateWorkerAsync(int id, Worker worker)
        {
            var response = await _httpClient.PostAsJsonAsync($"workers/UpdateWorker/{id}", worker);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteWorkerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"workers/DeleteWorker/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
