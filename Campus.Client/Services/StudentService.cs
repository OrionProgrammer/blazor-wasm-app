using Campus.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Campus.Client.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ISecurityService _securityService;
        private readonly AccountService _accountService;

        public StudentService(HttpClient httpClient,
                              ISecurityService securityService,
                              AccountService accountService)
        {
            _httpClient = httpClient;
            _securityService = securityService;
            _accountService = accountService;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async void AddJWTToken()
        {
            // Retrieve the JWT token from the security service
            var token = await _accountService.GetUserJWTToken();

            // Check if the token is not null or empty
            if (!string.IsNullOrEmpty(token))
            {
                // Add the token to the default request headers
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        //de-register from aa course
        public async Task<bool> DeRegister(int courseId)
        {
            AddJWTToken();

            var response = await _httpClient.DeleteAsync($"student/{courseId}");
            return response.IsSuccessStatusCode;
        }

        //getch all courses for student
        public async Task<List<StudentCourseModel>> FetchMyCourses(string studentId)
        {
            AddJWTToken();

            var entities = new List<StudentCourseModel>();

            var response = await _httpClient.GetAsync($"student/list/{studentId}");

            if (response.IsSuccessStatusCode)
            {
                var returnValue = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<StudentCourseModel>>(returnValue);
            }

            return entities;
        }

        //register for a course
        public async Task<bool> Register(StudentCourseModel studentCourseModel)
        {
            AddJWTToken();

            var response = await _httpClient.PostAsJsonAsync("student/register", studentCourseModel);
            return response.IsSuccessStatusCode;
        }
    }
}
