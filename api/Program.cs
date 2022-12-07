using Services.CheckWebsite;
using Services.CheckWebsiteDataService;
using Services.WebsiteInfoService;

// background worker
using JobWorker;

// SETUP
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var stations = new[]{""};

// ENDPOINTS
// GET /job/all
app.Get("/jobs/all", async () => {
    List<Website> jobs = await JobScheduler.GetWeatherReportJobsAsync();
    return jobs;
});

app.MapGet("/jobs/due", async () => {
    List<Website> jobs = await JobScheduler.GetScheduledJobsToRunAsync();
    return jobs;
});

// POST /job/create 
app.Post(
    "/jobs/create",
    // do stuff here
    async (Website? job) => {
        var output = await JobScheduler.ScheduleReportJobAsync(job!);
        return output;
    }
);

// GET /obs/{station}
app.Get(
    "/obs/{id}/raw", 
    async (string id) => {

        Observation? obs = await CheckWebsite.GetLastestAsync(id);
        // write to db here (call wep service to write to db)
        Console.WriteLine($"Returned value: {obs?.RawMessage}");
        return obs?.RawMessage;
    }
);

// EXECUTE
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#working-with-ports
app.Run("https://localhost:3000");




