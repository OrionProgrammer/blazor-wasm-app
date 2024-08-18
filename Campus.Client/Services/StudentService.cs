using Campus.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Campus.Client.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ISecurityService _securityService;

        public StudentService(HttpClient httpClient,
                              ISecurityService securityService)
        {
            _httpClient = httpClient;
            _securityService = securityService;
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

        //register for a course
        public async Task<bool> Register(StudentCourseModel studentCourseModel)
        {
            var response = await _httpClient.PostAsJsonAsync("student/register", studentCourseModel);
            return response.IsSuccessStatusCode;
        }
    }
}
