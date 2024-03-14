namespace TicTacToeBlazorServer.Services
{
    public interface ITimerService
    {
        public Task StartTimerAsync(TimeSpan duration);
        public Task StopTimerAsync();
        public Task<bool> IsRunningAsync();
        public Task<long> StartTimeAsync();
        public Task<long> RemainingTimeMillisecondsAsync();
    }
}
