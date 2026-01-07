using System.Text;
using PortalGtf.Core.Interfaces;
using PortalGtf.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PortalGtf.Application.Services.AuthServices;
using PortalGtf.Application.Services.CidadeService;
using PortalGtf.Application.Services.EditorialServices;
using PortalGtf.Application.Services.EmissoraRegiaoServices;
using PortalGtf.Application.Services.EmissoraServices;
using PortalGtf.Application.Services.EstadoServices;
using PortalGtf.Application.Services.FuncaoServices;
using PortalGtf.Application.Services.PermissaoServices;
using PortalGtf.Application.Services.RegiaoServices;
using PortalGtf.Application.Services.TemaEditorialServices;
using PortalGtf.Application.Services.UsuarioEmissoarServices;
using PortalGtf.Application.Services.UsuarioServices;
using PortalGtf.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<PortalGtfNewsDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});
// Repositories
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();
builder.Services.AddScoped<IRegiaoService, RegiaoService>();
builder.Services.AddScoped<ITemaEditorialRepository, TemaEditorialRepository>();
builder.Services.AddScoped<ITemaEditorialService, TemaEditorialService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFuncaoRepository, FuncaoRepository>();
builder.Services.AddScoped<IFuncaoService, FuncaoService>();
builder.Services.AddScoped<IPermissaoRepository, PermissaoRepository>();
builder.Services.AddScoped<IPermissaoService, PermissaoService>();
builder.Services.AddScoped<IEmissoraRepository, EmissoraRepository>();
builder.Services.AddScoped<IEmissoraService, EmissoraService>();
builder.Services.AddScoped<IEditorialService, EditorialService>();
builder.Services.AddScoped<IEditorialRepository, EditorialRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<ICidadeService, CidadeService>();
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();

builder.Services.AddScoped<IFuncaoPermissaoRepository, FuncaoPermissaoRepository>();
builder.Services.AddScoped<IFuncaoService, FuncaoService>();

builder.Services.AddScoped<IEmissoraRegiaoRepository, EmissoraRegiaoRepository>();
builder.Services.AddScoped<IEmissoraRegiaoService, EmissoraRegiaoService>();

builder.Services.AddScoped<IUsuarioEmissoraRepository, UsuarioEmissoraRepository>();
builder.Services.AddScoped<IUsuarioEmissoraService, UsuarioEmissoraService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials();
    });
});
#region AUTENTICACAO JWT BEARER

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GTF NEWS",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Informe o token JWT no formato: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
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
            new string[] {}
        }
    });
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();