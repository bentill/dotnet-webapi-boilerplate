namespace FSH.WebApi.ClientApiController
{
    public interface IMt4ClientConfigs
    {
        string Host { get; set; }
        int Port { get; set; }
        uint Login { get; set; }
        string Password { get; set; }
    }

    public struct Mt4ClientConfigs : IMt4ClientConfigs
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public uint Login { get; set; }
        public string Password { get; set; }
    }
}