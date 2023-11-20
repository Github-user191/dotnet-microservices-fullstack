using Cart.API.Repositories;
using Cart.API.SyncDataServices;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICartRepository, CartRepository>();

// gRPC client and server configuration
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opts => {
    // Uri to connect to gRPC server (Discount gRPC Service URL)
    opts.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});

builder.Services.AddScoped<DiscountGrpcService>();


builder.Services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();
builder.Services.AddAutoMapper(typeof(Program)); // Register AutoMapper DTO
builder.Services.AddStackExchangeRedisCache(opts => {
    opts.Configuration = builder.Configuration["RedisCacheSettings:ConnectionString"];
    Console.WriteLine($"--> Redis Connection String: {builder.Configuration["RedisCacheSettings:ConnectionString"]}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
