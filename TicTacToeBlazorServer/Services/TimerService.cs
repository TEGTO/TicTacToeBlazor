using Microsoft.JSInterop;

namespace TicTacToeBlazorServer.Services
{
    public class TimerService : ITimerService
    {
        private const string StartTimeKey = "StartTime";
        private const string SettedDuration = "SettedDuration";

        private readonly IJSRuntime _jsRuntime;

        private long startTime;
        private long settedDurationMilliseconds;

        public async Task<bool> IsRunningAsync()
        {
            await GetCachedData();
            long elapsedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTime;
            return settedDurationMilliseconds - elapsedTime > 0;
        }
        public async Task<long> StartTimeAsync()
        {
            await GetCachedData();
            return startTime;
        }
        public async Task<long> RemainingTimeMillisecondsAsync()
        {
            await GetCachedData();
            long elapsedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTime;
            long remainingTime = settedDurationMilliseconds - elapsedTime;
            return remainingTime < 0 ? 0 : remainingTime;
        }

        public TimerService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task StartTimerAsync(TimeSpan duration)
        {
            await GetCachedData();
            if (!await IsRunningAsync())
            {
                startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                settedDurationMilliseconds = (long)duration.TotalMilliseconds;
                await SaveToCache();
            }
        }
        public async Task StopTimerAsync()
        {
            startTime = 0;
            settedDurationMilliseconds = 0;
            await SaveToCache();
        }
        private async Task GetCachedData()
        {
            try
            {
                startTime = Convert.ToInt64(await _jsRuntime.InvokeAsync<string>("getCookie", StartTimeKey));
                settedDurationMilliseconds = Convert.ToInt64(await _jsRuntime.InvokeAsync<string>("getCookie", SettedDuration));
            }
            catch (JSException ex)
            {
                Console.WriteLine($"Error retrieving timer data from local storage: {ex.Message}");
                startTime = 0;
                settedDurationMilliseconds = 0;
            }
        }
        private async Task SaveToCache()
        {
            try
            {
                await _jsRuntime.InvokeAsync<string>("deleteAndAppendCookie", StartTimeKey, startTime.ToString());
                await _jsRuntime.InvokeAsync<string>("deleteAndAppendCookie", SettedDuration, settedDurationMilliseconds.ToString());
            }
            catch (JSException ex)
            {
                Console.WriteLine($"Error storing timer data in local storage: {ex.Message}");
            }
        }
    }
}
