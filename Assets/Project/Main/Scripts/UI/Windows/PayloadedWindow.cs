namespace UI.Windows
{
    public abstract class PayloadedWindow<TPayload> : BaseWindow
    {
        public abstract void SetPayload(TPayload payload);
    }
}