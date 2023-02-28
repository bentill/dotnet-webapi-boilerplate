namespace FSH.WebApi.ClientApiController
{
    public interface IClientApiController: IDisposable
    {
        bool Connected { get; }

        void Connect();

        void Disconnect();
    }
}