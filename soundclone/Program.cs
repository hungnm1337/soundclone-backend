using Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.SignUp;
using Repositories.Login;
using Services.SignUp;
using Services.Login;
using Services.JWT;
using System.Text;
using Microsoft.OpenApi.Models;
using Repositories.Playlist;
using Services.Playlist;
using Repositories.Track;
using Services.Track;
using Microsoft.Extensions.Configuration;
using Services.Upload;
using Repositories.LikePlaylist;
using Services.LikePlaylist;
using Repositories.Artist;
using Services.Artist;
using Repositories.Profile;
using Services.Profile;
using Repositories.LikeTrack;
using Services.LikeTrack;
using Repositories.ListData;
using Services.ListData;
using Repositories.Follow;
using Services.Follow;

namespace soundclone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SoundcloneContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "https://localhost:4200") // ? Specific origin
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
                });
            });
            // Add services to the container.
            builder.Services.AddScoped<ISignUpRepository, SignUpRepository>();
            builder.Services.AddScoped<ISignUpService, SignUpService>();
            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IArtistService, ArtistService>();
            builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            builder.Services.AddScoped<IPlaylistService, PlaylistService>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<ITrackRepository, TrackRepository>();
            builder.Services.AddScoped<ITrackService, TrackService>();
            builder.Services.AddScoped<IUploadService, UploadService>();
            builder.Services.AddScoped<ILikePlaylistRepository, LikePlaylistRepository>();
            builder.Services.AddScoped<ILikePlaylistService, LikePlaylistService>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddScoped<ILikeTrackRepository, LikeTrackRepository>();
            builder.Services.AddScoped<ILikeTrackService, LikeTrackService>();
            builder.Services.AddScoped<IListDataRepository, ListDataRepository>();
            builder.Services.AddScoped<IListDataService, ListDataService>();
            builder.Services.AddScoped<IFollowRepository, FollowRepository>();
            builder.Services.AddScoped<IFollowService, FollowService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoundClone API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
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

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                    policy.RequireRole("5"));
                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("6"));
            });

            var jwtSettings = builder.Configuration.GetSection("JWT");
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
                   ValidIssuer = jwtSettings["Issuer"] ?? "SoundClone",
                   ValidAudience = jwtSettings["Audience"] ?? "SoundCloneUsers",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? "YourSuperSecretKey12345678901234567890"))
               };
           });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
