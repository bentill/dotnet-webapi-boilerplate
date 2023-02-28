using System;
using TradingAPI.MT4Server;

namespace FSH.WebApi.ClientApiController
{
    public interface IOrderClientWrapper
    {
        OrderClient Client { get; }
        void Init(QuoteClient qc);
       // void SetTradeRequestMode(MT4TradeRequestFlags mode);

        Order OrderModify(int ticket, string symbol, Op type, double volume, double price, double stoploss,
            double takeprofit,
            DateTime expiration);

        int OrderModifyAsync(Op type, int ticket, double price, double stoploss, double takeprofit,
            DateTime expiration);

        Order OrderClose(string symbol, int ticket, double volume, double price, int slippage);
        int OrderCloseAsync(string symbol, int ticket, double volume, double price, int slippage);
        void OrderCloseBy(string symbol, int ticket1, int ticket2);
        int OrderCloseByAsync(string symbol, int ticket1, int ticket2);
        void OrderMultipleCloseBy(string symbol);
        int OrderMultipleCloseByAsync(string symbol);
        void OrderDelete(int ticket, Op type, string symbol, double volume, double price);
        int OrderDeleteAsync(int ticket, Op type, string symbol, double volume, double price);
        Order OrderSend(string symbol, Op operation, double volume, double price);

        Order OrderSend(string symbol, Op operation, double volume, double price, int slippage = 0, double stoploss = 0,
            double takeprofit = 0, string comment = null, int magic = 0, DateTime expiration = new DateTime());

        int OrderSendAsync(string symbol, Op operation, double volume, double price, int slippage, double stoploss,
            double takeprofit, string comment, int magic, DateTime expiration);

        event OrderProgressEventHandler OnOrderProgress;
    }

    public class OrderClientWrapper : IOrderClientWrapper
    {
        public OrderClientWrapper()
        {
            Client = new OrderClient();
        }

        public OrderClientWrapper(IQuoteClient quoteClient)
        {
            Client = new OrderClient(quoteClient.Client);
        }

        public OrderClient Client { get; }

        public void Init(QuoteClient qc)
        {
            Client.Init(qc);
        }

        //public void SetTradeRequestMode(MT4TradeRequestFlags mode)
        //{
        //    Client.SetTradeRequestMode(mode);
        //}

        public Order OrderModify(int ticket, string symbol, Op type, double volume, double price, double stoploss,
            double takeprofit,
            DateTime expiration)
        {
            return Client.OrderModify(ticket, symbol, type, volume, price, stoploss, takeprofit, expiration);
        }

        public int OrderModifyAsync(Op type, int ticket, double price, double stoploss, double takeprofit,
            DateTime expiration)
        {
            //return Client.OrderModifyAsync(type, ticket, price, stoploss, takeprofit, expiration);
            return 0;
        }

        public Order OrderClose(string symbol, int ticket, double volume, double price, int slippage)
        {
            return Client.OrderClose(symbol, ticket, volume, price, slippage);
        }

        public int OrderCloseAsync(string symbol, int ticket, double volume, double price, int slippage)
        {
            return Client.OrderCloseAsync(symbol, ticket, volume, price, slippage);
        }

        public void OrderCloseBy(string symbol, int ticket1, int ticket2)
        {
            Client.OrderCloseBy(symbol, ticket1, ticket2);
        }

        public int OrderCloseByAsync(string symbol, int ticket1, int ticket2)
        {
            return Client.OrderCloseByAsync(symbol, ticket1, ticket2);
        }

        public void OrderMultipleCloseBy(string symbol)
        {
            Client.OrderMultipleCloseBy(symbol);
        }

        public int OrderMultipleCloseByAsync(string symbol)
        {
            return Client.OrderMultipleCloseByAsync(symbol);
        }

        public void OrderDelete(int ticket, Op type, string symbol, double volume, double price)
        {
            Client.OrderDelete(ticket, type, symbol, volume, price);
        }

        public int OrderDeleteAsync(int ticket, Op type, string symbol, double volume, double price)
        {
            return Client.OrderDeleteAsync(ticket, type, symbol, volume, price);
        }

        public Order OrderSend(string symbol, Op operation, double volume, double price)
        {
            return Client.OrderSend(symbol, operation, volume, price);
        }

        public Order OrderSend(string symbol, Op operation, double volume, double price, int slippage = 0,
            double stoploss = 0,
            double takeprofit = 0, string comment = null, int magic = 0, DateTime expiration = new DateTime())
        {
            return Client.OrderSend(symbol, operation, volume, price, slippage, stoploss, takeprofit, comment, magic,
                expiration);
        }

        public int OrderSendAsync(string symbol, Op operation, double volume, double price, int slippage,
            double stoploss,
            double takeprofit, string comment, int magic, DateTime expiration)
        {
            return Client.OrderSendAsync(symbol, operation, volume, price, slippage, stoploss, takeprofit, comment,
                magic, expiration);
        }

        public event OrderProgressEventHandler OnOrderProgress
        {
            add => Client.OnOrderProgress += value;
            remove => Client.OnOrderProgress -= value;
        }
    }
}