using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using ClientApiController;
using CT;
using Serilog;
using TradingAPI.MT4Server;
using Logger = TradingAPI.MT4Server.Logger;
using TradeRecord = CT.Trading.TradeRecord;

namespace FSH.WebApi.ClientApiController
{
    [DebuggerDisplay("{ServerName} {Login}")]
    public class ClientController : IClientApiController
    {
        private readonly string loginDll;
        private readonly string password;
        private readonly bool readOnlyMode;

        public ConcurrentDictionary<int, List<Order>> OrderMagic = new ConcurrentDictionary<int, List<Order>>();

        private readonly ILogger _logger = Log.ForContext(typeof(ClientController));

        public ClientController(IMt4ClientConfigs configs, string connectionMode = ConnectionMode.LoginDll)
        {
            loginDll = connectionMode;
            Login = (int)configs.Login;
            password = configs.Password;
            Server = configs.Host;
            Port = configs.Port;
            ServerName = "GBEbrokers-GBE Development Server";
            Connect();
            //InitOrderClient();
        }

        public string Server { get; }
        public int Port { get; }
        public int Login { get; }


        public string ServerName { get; }

        public IOrderClientWrapper OrderClient { get; private set; }
        public IQuoteClient QuoteClient { get; private set; }

        public string SymbolSavingPath => FileSystem.Combine(FileSystem.ProgramDirectory, "symbol");

        public DateTime LastTimeUpdateFromTradingServer => QuoteClient.CalculateTime;

        public double Equity => QuoteClient.AccountEquity;
        public double Profit => QuoteClient.AccountProfit;
        public double Margin => QuoteClient.AccountMargin;
        public double FreeMargin => QuoteClient.AccountFreeMargin;
        public string AccountName => QuoteClient.AccountName;
        public int Leveragel => QuoteClient.AccountLeverage;

        public bool Connected => QuoteClient.Connected;

        /// <summary>
        /// if connect fail log info
        /// </summary>
        public void Connect()
        {
            _logger.Debug($"Connecting to \"{ServerName}\" Login {Login}");
            QuoteClient = new QuoteClientWrapper(Login, password, Server, Port);
            //Directory.CreateDirectory(SymbolSavingPath);
            //QuoteClient.PathForSavingSym = SymbolSavingPath;
            //QuoteClient.LoginIdPath = loginDll;

            try
            {
                QuoteClient.Connect();
                SubscribeSymbols();
                QuoteClient.SkipUpdateClosePrice = false;
                QuoteClient.SetSocketKeepAlive(2000, 100);
                QuoteClient.OnDisconnect += QC_OnDisconnect;
                QuoteClient.OnOrderUpdate += QuoteClient_OnOrderUpdate;
                //Logger.OnMsg += QC_LogOnMsg;
                _logger.Debug($"<green>Connected<default> to \"{ServerName}\" Login {Login}");
            }
            catch (Exception ex)
            {
                QuoteClient.Disconnect();
                switch (ex.Message)
                {
                    case "Invalid account":
                        throw new InvalidOperationException($"Login {Login} invalid account info");
                    default:
                        throw new InvalidOperationException(
                            $"Login {Login} Could not connect to MT4 server");
                }
            }
        }

        public void Disconnect()
        {
            QuoteClient?.Disconnect();
        }

        public string LogSourceName => $"Terminal #{Login}";

        public event DisconnectEventHandler OnDisconnect;

        public event OrderUpdateEventHandler OnOrderUpdate;

        private void QuoteClient_OnOrderUpdate(object sender, OrderUpdateEventArgs order)
        {
            OnOrderUpdate?.Invoke(this, order);
        }

        /// <summary>
        ///     GetQoute to dictionary when connected
        /// </summary>
        /// <returns>dictionary Quotes</returns>
        public ConcurrentDictionary<string, QuoteEventArgs> QuoteEvent()
        {
            var lst = new Dictionary<string, QuoteEventArgs>();
            foreach (var item in QuoteClient.Symbols) lst[item] = GetQuote(item); // Allow null
            return new ConcurrentDictionary<string, QuoteEventArgs>(lst);
        }

        public IList<TradeRecord> GetTradesByMagic(int magic)
        {
            var orders = QuoteClient.GetOpenedOrders();
            return magic == 0
                ? orders.Select(order => order.ToTradeRecord()).ToList()
                : orders.Where(x => x.MagicNumber == magic).Select(order => order.ToTradeRecord()).ToList();
        }

        public IList<TradeRecord> GetAllTrades()
        {
            var orders = QuoteClient.GetOpenedOrders();
            return orders.Select(order => order.ToTradeRecord()).ToList();
        }

        /// <summary>
        ///     subscribe all symbol
        /// </summary>
        private void SubscribeSymbols()
        {
            QuoteClient.Subscribe(QuoteClient.Symbols);
        }

        private void InitOrderClient()
        {
            if (QuoteClient.Connected)
            {
                OrderClient = new OrderClientWrapper(QuoteClient);
                //OrderClient.SetTradeRequestMode(MT4TradeRequestFlags.TT_FLAG_EXPERT);
                OrderClient.OnOrderProgress += OC_OnOrderProgress;
            }
        }

        private void QC_OnDisconnect(object sender, DisconnectEventArgs args)
        {
            _logger.Debug($"<red>Disconnected<default> to \"{ServerName}\"Login {Login}");
            OnDisconnect?.Invoke(this, args);
            //    Reconnect();
        }

        private void QC_LogOnMsg(object sender, string msg, Logger.MsgType type)
        {
            // reverse the order
            //var level = LogLevel.Verbose - (int)type;
            //switch (msg)
            //{
            //    case string ping10s when ping10s.Contains("Ping 10s"):
            //    case string cmdPing when cmdPing.Contains("Cmd Ping"):
            //        level = LogLevel.Warning;
            //        msg +=
            //            ". <red>The connection may not work properly at the moment, please check more details in logs.";
            //        break;
            //    case string ping8s when ping8s.Contains("Ping II"):
            //        level = LogLevel.Verbose;
            //        break;
            //}

            //this.Log(level, $"Login #{Login}: {msg}");
        }

        private void OC_OnOrderProgress(object sender, OrderProgressEventArgs args)
        {
            if (args.Exception == null)
            {
                _logger.Debug("RequestID = " + args.TempID + ", State = " + args.Type);
                switch (args.Type)
                {
                    case ProgressType.Opened:
                    case ProgressType.Closed:
                        _logger.Debug("RequestID = " + args.TempID + ", Ticket = " + args.Order.Ticket);
                        break;
                }
            }
            else
            {
                _logger.Error(args.Exception.Message);
            }
        }

        public void Reconnect()
        {
            UnSubscribe();
            Connect();
            //InitOrderClient();
        }

        private void UnSubscribe()
        {
            if (QuoteClient != null)
            {
                QuoteClient.OnDisconnect -= QC_OnDisconnect;
                ///Logger.OnMsg += QC_LogOnMsg;
                QuoteClient.OnOrderUpdate -= QuoteClient_OnOrderUpdate;
            }

            if (OrderClient != null) OrderClient.OnOrderProgress -= OC_OnOrderProgress;
        }

        public bool TryGetSymbolInfo(string symbol, out double contractsize)
        {
            contractsize = 0;
            try
            {
                contractsize = QuoteClient.GetSymbolInfo(symbol).ContractSize;
            }
            catch
            {
                // ignored
            }

            return contractsize > 0;
        }

        /// <summary>
        ///     Get min lot and lot step (BEWARE: in lot)
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="lotmin"></param>
        /// <param name="lotstep"></param>
        /// <returns></returns>
        public bool TryGetLotSpecs(string symbol, out (double minLot, double maxLot, double lotStep) specs)
        {
            specs = (0d, 0d, 0d);
            try
            {
                var para = QuoteClient.GetSymbolGroupParams(symbol);
                specs = (para.lot_min / 100d, para.lot_max / 100d, para.lot_step / 100d);
            }
            catch
            {
                // ignored
            }

            return specs.minLot > 0 && specs.lotStep > 0 && specs.maxLot > 0;
        }

        public QuoteEventArgs GetQuote(string symbol)
        {
            var quote = QuoteClient.GetQuote(symbol);
            var count = 10;
            while (quote == null && --count > 0)
            {
                Thread.Sleep(100);
                quote = QuoteClient.GetQuote(symbol);
            }

            if (quote == null)
                return null;
            return quote;
        }

        public void Dispose()
        {
            QuoteClient?.Disconnect();
            QuoteClient = null;
            OrderClient = null;
        }
    }
}