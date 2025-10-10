using System.Text;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

// Db & Services ของโปรเจกต์
using Backend_BeautyPagrant.Models;
using Backend_BeautyPagrant.Services;


var builder = WebApplication.CreateBuilder(args);

// ===== MVC / JSON =====
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// ===== DbContext =====
builder.Services.AddDbContext<BeautyPagrantContext>();

// ===== Services (DI) =====
// auth (ตามของคุณ)
builder.Services.AddScoped<AuthService>();

// file storage
builder.Services.AddScoped<IFileStorage, FileStorage>();

// event services
builder.Services.AddScoped<IEventComponentImageBinder, EventComponentImageBinder>();
builder.Services.AddScoped<IEventAppService, EventAppService>();

// form services
builder.Services.AddScoped<IFormComponentImageBinder, FormComponentImageBinder>();
builder.Services.AddScoped<IFormTemplateAppService, FormTemplateAppService>();
builder.Services.AddScoped<IFormResultAppService, FormResultAppService>();

// ===== Auth (JWT) =====
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    };
});

// ===== Swagger =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Backend-BeautyPagrant",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. ใส่เฉพาะ token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ===== CORS =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// ===== Dev helpers =====
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ===== Static files (serve /uploads) =====
app.UseHttpsRedirection();
app.UseCors("AllowAngular");

// เสิร์ฟ wwwroot ถ้ามี (จะไม่มีผลถ้าไม่มี wwwroot)
app.UseStaticFiles();

// เสิร์ฟโฟลเดอร์ uploads จาก ContentRoot/uploads -> /uploads/**
var uploadsPhysicalPath = Path.Combine(app.Environment.ContentRootPath, "uploads");
if (!Directory.Exists(uploadsPhysicalPath))
{
    Directory.CreateDirectory(uploadsPhysicalPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPhysicalPath),
    RequestPath = "/uploads"
});

// ===== AuthZ =====
app.UseAuthentication();
app.UseAuthorization();

// ===== Routing =====
app.MapControllers();

app.Run();
