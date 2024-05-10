using Application.Interfaces;

namespace LoadMeteoritesInfoWS
{
    public class Worker : BackgroundService
    {
        private readonly IMeteoriteServices _meteoriteServices;
        public Worker(IMeteoriteServices meteoriteServices)
        {
            _meteoriteServices = meteoriteServices;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var res = await _meteoriteServices.LoadAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //Logger.Error
                }
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
