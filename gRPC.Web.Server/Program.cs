global using  Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenLocalhost(7264, o => o.Protocols =  HttpProtocols.Http2);
// });

var app = builder.Build();

app.UseGrpcWeb();
app.MapGrpcService<StreamImplService>().EnableGrpcWeb();
//app.MapGet("/", () => "Hello World!");
app.UseCors("AllowOrigin");

app.Run();
