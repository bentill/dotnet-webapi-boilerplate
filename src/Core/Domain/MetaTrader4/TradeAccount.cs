using FSH.WebApi.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FSH.WebApi.Domain.MetaTrader4;
public class TradeAccount : AuditableEntity, IAggregateRoot
{
    public TradeAccount()
    {
    }

    public long BrokerID { get;set; }

    public TradeAccountType Type { get; set; }

    public string Name { get; set; }

    public string Group { get; set; }

    public string Description { get; set; }

    public string Comment { get; set; }

    public string Configuration { get; set; }

    public string Owner { get; set; }

    public TimeSpan InactivityLimit { get; set; }

    public Currency Currency { get; set; }

    public TradeAccountFlags Flags { get; set; }

    public int Leverage { get; set; }

    public double Balance { get; set; }

    public double Credit { get; set; }

    public double Equity { get; set; }

    public double Margin { get; set; }

    public double FreeMargin { get; set; }

    public double FreeMarginPercent { get; set; }

    public double NotifyLevel { get; set; }

    public double Profit { get; set; }

    public uint TradeCountMax { get; set; }

    public TimeSpan TradeConfirmationTimeout { get; set; }

    public int OrderPriority { get; set; }

    public double OrderQuantityMinimum { get; set; }

    public double OrderQuantityStep { get; set; }

    public double OrderQuantityMaximum { get; set; }

    public MarginCalculationMode MarginCalculationMode { get; set; }

    public MarginStopOutCalculationMode MarginStopOutCalculationMode { get; set; }

    public MarginStopOutMode MarginStopOutMode { get; set; }

    public double MarginWarningPercent { get; set; }

    public double MarginStopout { get; set; }

    public double InterestRate { get; set; }

    public DateTime Timestamp { get; set; }

}
public enum MarginStopOutMode
{
    None,
    HiHi,
    LoLo,
    HiLo,
    LoHi,
    FiFo,
    LiFo,
    Intraday_Fifo
}
public enum MarginStopOutCalculationMode
{
    Percentage,
    Comparison
}
public enum MarginCalculationMode
{
    Undefined = -1,
    NoFloating,
    ProfitAndLoss,
    CurrentProfit,
    CurrentLoss
}
public enum TradeAccountFlags
{
    None = 0x0,
    Demo = 0x1,
    SwapFree = 0x2,
    HedgeProhibited = 0x4,
    Enabled = 0x8,
    CanChangePassword = 0x10,
    CloseOnly = 0x20,
    SendReports = 0x40,
    FetchSymbols = 0x80,
    FetchTrades = 0x100,
    AllowTrading = 0x200
}
public enum Currency
{
    Invalid = 0,
    AUD = 1,
    BGN = 2,
    BRL = 3,
    CAD = 4,
    EUR = 10,
    USD = 35
}
public enum TradeAccountType
{
    Undefined = 0,
    MT5 = 2,
    MT4 = 3,
    DB = 4,
    TradeGate = 23,
    FIX = 1000,
    MT4Manager = 2000,
    MT4ManagerSub = 2001
}