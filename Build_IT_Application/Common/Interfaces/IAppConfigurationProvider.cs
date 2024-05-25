namespace Build_IT_WebApplication.Common.Interfaces
{
    public interface IAppConfigurationProvider
    {
        string GetConnectionString();
        string GetRedisUrl();
    }
}
