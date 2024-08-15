namespace API.Helpers;

using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Student, StudentModel>().ReverseMap();
        CreateMap<Student, StudentModel>();

        CreateMap<StudentCourse, StudentCourseModel>().ReverseMap();
        CreateMap<StudentCourse, StudentCourseModel>();

    }
}
