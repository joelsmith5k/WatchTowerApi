
using WatchTowerApi.Models;

namespace WatchTowerApi.Services;

public class DbHealthCheckService
{
    private readonly WatchTowerContext _context;
    private readonly ILogger<DbHealthCheckService> _logger;

    public DbHealthCheckService(WatchTowerContext context, ILogger<DbHealthCheckService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CheckConnectionAsync()
    {
        try
        {
            // Execute a simple query to check the connection
            await _context.Database.CanConnectAsync();
            _logger.LogInformation("Successfully connected to the database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to the database.");
        }
    }
}