using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MeetMind.Application.Interfaces;
using MeetMind.Infrastructure.Persistence;
using MeetMind.Infrastructure.Persistence.Repositories;
using MeetMind.Infrastructure.Security;

namespace MeetMind.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<MeetMindDbContext>(options =>
            options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IActionItemRepository, ActionItemRepository>();
        services.AddScoped<IMeetingSummaryRepository, MeetingSummaryRepository>();
        services.AddScoped<IAudioRecordingRepository, AudioRecordingRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();
        services.AddScoped<IKeyDecisionRepository, KeyDecisionRepository>();
        services.AddScoped<ITranscriptRepository, TranscriptRepository>();
        services.AddScoped<ITranscriptSegmentRepository, TranscriptSegmentRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
