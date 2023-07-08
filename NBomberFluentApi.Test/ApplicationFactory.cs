using Microsoft.AspNetCore.Mvc.Testing;

namespace NBomberFluentApi.Test;

public class ApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
{
    public HttpClient HttpClient { get; private set; } = default!;

    public Task InitializeAsync()
    {
        HttpClient = CreateClient();

        return Task.CompletedTask;
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        HttpClient.Dispose();

        return Task.CompletedTask;
    }
}
