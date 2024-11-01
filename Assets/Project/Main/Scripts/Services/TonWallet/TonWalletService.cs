using Services.Config;

namespace Services.TonWallet
{
    public class TonWalletService
    {
        private readonly IConfigProvider _configProvider;

        public TonWalletService(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void Initialize()
        {
        }

        public void ConnectWallet()
        {
  
        }
    }
}