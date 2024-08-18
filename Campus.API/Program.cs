using Campus.API.Helpers;
using Campus.API.Middleware;
using Campus.Domain;
using Campus.Repository.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;


services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//automapper service
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//database context service
services.AddDbContext<DataContext>();


//define CORS to make sure known URLs access the API
string corsName = "CorsName";
services.AddCors(options =>
{
    options.AddPolicy(corsName, policyBuilder => policyBuilder
        .WithOrigins("http://localhost", "https://localhost")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var appSettingsSection = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

// configure jwt authentication
var key = Encoding.ASCII.GetBytes(appSettingsSection.Secret);
services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var studentRepository = context.HttpContext.RequestServices.GetRequiredService<IStudentRepository>();
            var studentId = int.Parse(context.Principal.Identity.Name);
            var student = studentRepository.GetById(studentId);
            if (student == null)
            {
                // return unauthorized if user no longer exists
                context.Fail("Unauthorized");
            }
            return Task.CompletedTask;
        }
    };
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//add all dependencies
services.AddScoped<IStudentRepository, StudentRepository>();
services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddApplicationInsightsTelemetry();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// global error handler
app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
