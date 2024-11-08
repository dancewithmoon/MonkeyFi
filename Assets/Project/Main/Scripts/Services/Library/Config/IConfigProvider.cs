namespace Services.Library.Config
{
    public interface IConfigProvider
    {
        ConfigData Config { get; }
    }
}