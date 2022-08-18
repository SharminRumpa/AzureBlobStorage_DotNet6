using AzureBlobStorage_DotNet6.Data;
using AzureBlobStorage_DotNet6.Implementation;
using AzureBlobStorage_DotNet6.Interface;
using AzureBlobStorage_DotNet6.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add databse connection
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.Configure<AzureBlobStorage>(builder.Configuration.GetSection("AzureBlobStorage"));

//builder.Services.AddScoped<IPictureRepository, PictureRepository>();

#region BLR class registration
var types = typeof(IBLRBase).Assembly.GetTypes()
        .Where(
            type => typeof(IBLRBase).IsAssignableFrom(type)
                    && !type.IsInterface
                    && !type.IsAbstract
        );

foreach (Type type in types)
{
    var BLRInterface = type.GetInterfaces().FirstOrDefault(interf => !interf.Name.Equals(nameof(IBLRBase)));
    if (BLRInterface != null)
    {
        builder.Services.AddScoped(BLRInterface, type);
    }
}

#endregion

// Default Cross Policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

#region CORS options

//var origins = builder.Configuration.GetValue<string>("CORS:AllowedOrigins").Split(',');

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOriginAnyMethodAnyHeader",
//                          builder =>
//                          {
//                              builder.WithOrigins(origins)
//                                                  .AllowAnyHeader()
//                                                  .AllowAnyMethod();
//                          });
//});


#endregion


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseDeveloperExceptionPage();
app.UseCors();
//app.UseCors("AllowSpecificOriginAnyMethodAnyHeader");

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();
