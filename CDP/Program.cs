using CDP.Infrastructure;
using CDP.Middlewear;
using UnitofWork;

var builder = WebApplication.CreateBuilder(args);

// Add Identity Register
new IdentityRegister(builder);
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;

});

// Add Dependency Register
new DependencyRegister(builder);
// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Cors Policy
builder.Services.AddCors(options =>
options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Hawks//
//builder.WebHost.UseUrls("https://192.168.19.94:5003/");
//PTCL//
//builder.WebHost.UseUrls("https://192.168.26.14:5004/");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthentication(); // Usually, this should be before authorization
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseGloablCustomMiddleware(); // Fix typo from "UseGloablCustomMiddleware"

app.MapControllers();

app.Run();
