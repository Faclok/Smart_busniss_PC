using System.Threading;
using System.Threading.Tasks;
using ChatGPTSharp;

public static class ChatGPT
{
    private const string key = "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";

    private static readonly ChatGPTClient _client = new(key);

    private static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

    public static async Task<string> GetAnswer(string request)
    {
        await semaphoreSlim.WaitAsync();

        var data = await _client.SendMessage(request);

        semaphoreSlim.Release();

        return data.Response;
    }
}
