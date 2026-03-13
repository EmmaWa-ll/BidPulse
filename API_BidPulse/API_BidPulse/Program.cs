
using API_BidPulse.Core.Interfaces;
using API_BidPulse.Core.Services;
using API_BidPulse.Data;
using API_BidPulse.Data.Interfaces;
using API_BidPulse.Data.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddCors();


builder.Services.AddSwaggerGen(opt =>
{
    opt.EnableAnnotations();
});


builder.Services.AddDbContext<BidPulseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BidPulseDB")));


////DI  REPO
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IAuctionRepo, AuctionRepo>();
builder.Services.AddScoped<IBidRepo, BidRepo>();

////DI Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IBidService, BidService>();



var app = builder.Build();



app.UseCors(options => options.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}


app.Run();
