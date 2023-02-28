using System;
using System.Collections.Generic;
using TradingAPI.MT4Server;

namespace FSH.WebApi.ClientApiController
{
    public interface IQuoteClient
    {
        OrderClient OrderClient { get; }
        Logger Log { get; }
        int User { get; }
        string Host { get; }
        DateTime CalculateTime { get; }
        double AccountBalance { get; }
        double AccountCredit { get; }
        double AccountProfit { get; }
        double AccountEquity { get; }
        double AccountMargin { get; }
        double AccountFreeMargin { get; }
        string AccountName { get; }
        int AccountLeverage { get; }
        ConGroup Account { get; }
        ConSymbolGroup[] Groups { get; }
        DateTime ServerTime { get; }
        int ServerBuild { get; }
        int AccountMode { get; }
        bool IsInvestor { get; }
        List<Order> ClosedOrders { get; }
        string[] Symbols { get; }
        ConGroupSec[] GroupParameters { get; }
        bool Connected { get; }
        DateTime ConnectTime { get; }
        int Port { get; }
        AccountType AccountType { get; }
        string PathForSavingSym { get; set; }
        string PathForSavingSrv { get; set; }
        string LoginIdPath { get; set; }
        bool SkipUpdateClosePrice { get; set; }
        QuoteClientEx Client { get; }
        void Init(int user, string password, string host, int port);
        void Init(int user, string password, string serverFilePath);
        ServerInfo GetServerInfo();
        SymbolInfo GetSymbolInfo(string symbol);
        ConGroupSec GetSymbolGroupParams(string symbol);
        ConSymbolGroup GetSymbolGroup(string symbol);
        bool IsSubscribed(string symbol);
        void Subscribe(string symbol);
        void Unsubscribe(string symbol);
        void Subscribe(string[] symbols);
        QuoteEventArgs GetQuote(string symbol);
        void Connect();
        void Connect(int timeout);
        void ConnectAsync();
        void SetSocketKeepAlive(ulong time, ulong interval);
        Order[] GetOpenedOrders();
        Order GetOpenedOrder(int ticket);
        void RequestQuoteHistory(string symbol, Timeframe tf, DateTime from, short count);
        Bar[] DownloadQuoteHistory(string symbol, Timeframe tf, DateTime from, short count);
        Order[] DownloadOrderHistory(DateTime from, DateTime to);
        void Disconnect();
        event SymbolsUpdateEventHandler OnSymbolsUpdate;
        event OrderUpdateEventHandler OnOrderUpdate;
        event QuoteHistoryEventHandler OnQuoteHistory;
        event DisconnectEventHandler OnDisconnect;
        event ConnectEventHandler OnConnect;
        event QuoteEventHandler OnQuote;
    }

    public class QuoteClientWrapper : IQuoteClient
    {
        public QuoteClientWrapper()
        {
            Client = new QuoteClientEx();
        }

        public QuoteClientWrapper(int user, string password, string host, int port)
        {
            Client = new QuoteClientEx(user, password, host, port);
        }

        public QuoteClientWrapper(int user, string password, string host, int port, DateTime closedOrderFrom,
            DateTime closedOrderTo)
        {
            Client = new QuoteClientEx(user, password, host, port, closedOrderFrom, closedOrderTo);
        }

        public QuoteClientEx Client { get; }

        //public void ReconnectAsync()
        //{
        //    Client.ReconnectAsync();
        //}

        public void Init(int user, string password, string host, int port)
        {
            Client.Init(user, password, host, port);
        }

        public void Init(int user, string password, string serverFilePath)
        {
            Client.Init(user, password, serverFilePath);
        }

        public ServerInfo GetServerInfo()
        {
            return Client.GetServerInfo();
        }

        public SymbolInfo GetSymbolInfo(string symbol)
        {
            return Client.GetSymbolInfo(symbol);
        }

        public ConGroupSec GetSymbolGroupParams(string symbol)
        {
            return Client.GetSymbolGroupParams(symbol);
        }

        public ConSymbolGroup GetSymbolGroup(string symbol)
        {
            return Client.GetSymbolGroup(symbol);
        }

        public bool IsSubscribed(string symbol)
        {
            return Client.IsSubscribed(symbol);
        }

        public void Subscribe(string symbol)
        {
            Client.Subscribe(symbol);
        }

        public void Unsubscribe(string symbol)
        {
            Client.Unsubscribe(symbol);
        }

        public void Subscribe(string[] symbols)
        {
            Client.Subscribe(symbols);
        }

        public QuoteEventArgs GetQuote(string symbol)
        {
            return Client.GetQuote(symbol);
        }

        public void Connect()
        {
            Client.Connect();
        }

        public void Connect(int timeout)
        {
            Client.Connect(timeout);
        }

        //public void ConnectInternal()
        //{
        //    Client.ConnectInternal();
        //}

        public void ConnectAsync()
        {
            Client.ConnectAsync();
        }

        public void SetSocketKeepAlive(ulong time, ulong interval)
        {
            Client.SetSocketKeepAlive(time, interval);
        }

        public Order[] GetOpenedOrders()
        {
            return Client.GetOpenedOrders();
        }

        public Order GetOpenedOrder(int ticket)
        {
            return Client.GetOpenedOrder(ticket);
        }

        public void RequestQuoteHistory(string symbol, Timeframe tf, DateTime from, short count)
        {
            Client.RequestQuoteHistory(symbol, tf, from, count);
        }

        public Bar[] DownloadQuoteHistory(string symbol, Timeframe tf, DateTime from, short count)
        {
            return Client.DownloadQuoteHistory(symbol, tf, from, count);
        }

        public Order[] DownloadOrderHistory(DateTime from, DateTime to)
        {
            return Client.DownloadOrderHistory(from, to);
        }

        public void Disconnect()
        {
            Client.Disconnect();
        }

        public OrderClient OrderClient => Client.OrderClient;

        public Logger Log => Client.Log;

        public int User => Client.User;


        public string PathForSavingSym
        {
            get => Client.PathForSavingSym;
            set => Client.PathForSavingSym = value;
        }

        public string PathForSavingSrv
        {
            get => Client.PathForSavingSrv;
            set => Client.PathForSavingSrv = value;
        }

        public string LoginIdPath
        {
            get => Client.LoginIdPath;
            set => Client.LoginIdPath = value;
        }

        public bool SkipUpdateClosePrice
        {
            get => Client.SkipUpdateClosePrice;
            set => Client.SkipUpdateClosePrice = value;
        }

        public string Host => Client.Host;


        public DateTime CalculateTime => Client.CalculateTime;

        public double AccountBalance => Client.AccountBalance;

        public double AccountCredit => Client.AccountCredit;

        public double AccountProfit => Client.AccountProfit;

        public double AccountEquity => Client.AccountEquity;

        public double AccountMargin => Client.AccountMargin;

        public double AccountFreeMargin => Client.AccountFreeMargin;

        public string AccountName => Client.AccountName;

        public int AccountLeverage => Client.AccountLeverage;

        public ConGroup Account => Client.Account;

        public ConSymbolGroup[] Groups => Client.Groups;

        public DateTime ServerTime => Client.ServerTime;

        public int ServerBuild => Client.ServerBuild;

        public int AccountMode => Client.GetAccountMode();

        public bool IsInvestor => Client.IsInvestor;

        public List<Order> ClosedOrders => Client.ClosedOrders;

        public string[] Symbols => Client.Symbols;

        public ConGroupSec[] GroupParameters => Client.GroupParameters;

        public bool Connected => Client.Connected;

        public DateTime ConnectTime => Client.ConnectTime;

        public int Port => Client.Port;

        public AccountType AccountType => Client.AccountType;

        public event SymbolsUpdateEventHandler OnSymbolsUpdate
        {
            add => Client.OnSymbolsUpdate += value;
            remove => Client.OnSymbolsUpdate -= value;
        }

        public event OrderUpdateEventHandler OnOrderUpdate
        {
            add => Client.OnOrderUpdate += value;
            remove => Client.OnOrderUpdate -= value;
        }

        public event QuoteHistoryEventHandler OnQuoteHistory
        {
            add => Client.OnQuoteHistory += value;
            remove => Client.OnQuoteHistory -= value;
        }

        public event DisconnectEventHandler OnDisconnect
        {
            add => Client.OnDisconnect += value;
            remove => Client.OnDisconnect -= value;
        }

        public event ConnectEventHandler OnConnect
        {
            add => Client.OnConnect += value;
            remove => Client.OnConnect -= value;
        }

        public event QuoteEventHandler OnQuote
        {
            add => Client.OnQuote += value;
            remove => Client.OnQuote -= value;
        }
    }
}