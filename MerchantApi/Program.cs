using MerchantApi.Database;
using MerchantApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
//builder.Services.AddDbContext<Merchant_StoreDbContext>(opt => opt.UseInMemoryDatabase("MerchantDB"));
builder.Services.AddDbContext<Merchant_StoreDbContext>(opt =>
{ 
   opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(
options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
);
app.Run();
