using Blazored.SessionStorage;
using Campus.Client.Helpers;
using Campus.Client.Services.Interfaces;
using Campus.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Campus.Client.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;
        private readonly ISecurityService _securityService;

        public StudentService(HttpClient httpClient,
                              ISessionStorageService sessionStorage,
                              ISecurityService securityService)
        {
            _httpClient = httpClient;
            _securityService = securityService;
            _sessionStorage = sessionStorage;
        }

        //Method inlined to be faster
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async void AddAntiforgeryToken()
        {
            _httpClient.DefaultRequestHeaders.Add("X-CSRF-TOKEN", await _securityService.GetAntiforgeryToken());
        }

        public async Task<bool> DeRegister(int courseId)
        {
            AddAntiforgeryToken();

            var response = await _httpClient.DeleteAsync($"student/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<StudentCourseModel>> FetchMyCourses(string studentId)
        {
            AddAntiforgeryToken();

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
            AddAntiforgeryToken();

            var response = await _httpClient.PostAsJsonAsync("student/register", studentCourseModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<UserSessionModel> GetUserSessionModel()
        {
            return await _sessionStorage.ReadEncryptedItemAsync<UserSessionModel>("UserSession");
        }
    }
}
