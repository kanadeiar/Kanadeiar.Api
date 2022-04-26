using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Kanadeiar.Api.Tools;

/// <summary>
/// Проверка работоспособности сервиса
/// </summary>
public class ServiceHealthCheck : IHealthCheck
{
    /// <summary>
    /// Проверка работоспособности
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Healthy"));
        }
        return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Dead"));
    }
}
