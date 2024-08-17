using Blazored.SessionStorage;
using Campus.Client.Helpers;
using Campus.Client.Services.Interfaces;
using Campus.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Campus.Client.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public StudentService(HttpClient httpClient,
                              ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> DeRegister(int courseId)
        {
            var response = await _httpClient.DeleteAsync($"student/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<StudentCourseModel>> FetchMyCourses(string studentId)
        {
            var entities = new List<StudentCourseModel>();

            var response = await _httpClient.GetAsync($"student/list/{studentId}");

            if (response.IsSuccessStatusCode)
            {
                var returnValue = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<StudentCourseModel>>(returnValue);
            }

            return entities;
        }

        public async Task<bool> Register(StudentCourseModel studentCourseModel)
        {
            var response = await _httpClient.PostAsJsonAsync("student/register", studentCourseModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<UserSessionModel> GetUserSessionModel()
        {
            return await _sessionStorage.ReadEncryptedItemAsync<UserSessionModel>("UserSession");
        }
    }
}
