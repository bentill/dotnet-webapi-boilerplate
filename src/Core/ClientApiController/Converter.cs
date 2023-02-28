using CT.Trading;
using TradingAPI.MT4Server;
using TradeRecord = CT.Trading.TradeRecord;

namespace ClientApiController
{
    public static class Converter
    {
        public static TradeRecord ToTradeRecord(this Order order)
        {
            return new TradeRecord
            {
                Ticket = order.Ticket,
                Type = (TradeType) (int) order.Type,
                Magic = order.MagicNumber,
                Quantity = order.Lots,
                OpenTime = order.OpenTime,
                OpenPrice = order.OpenPrice,
                SymbolName = order.Symbol,
                Comment = order.Comment,
                CloseTime = order.CloseTime,
                Profit = order.Profit,
                TakeProfit = order.TakeProfit,
                Commission = order.Commission,
                StopLoss = order.StopLoss,
                Swap = order.Swap,
                ClosePrice = order.ClosePrice
            };
        }
    }
}