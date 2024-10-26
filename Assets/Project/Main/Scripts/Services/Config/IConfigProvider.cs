namespace Services.Config
{
    public interface IConfigProvider
    {
        ConfigData Config { get; }
    }
}