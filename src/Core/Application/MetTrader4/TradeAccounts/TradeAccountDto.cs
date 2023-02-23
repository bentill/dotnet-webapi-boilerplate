using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.MetTrader4.TradeAccounts;
public class TradeAccountDto:IDto
{
    public Guid Id { get; set; }
    public uint Login { get; set; } = default!;
    public string? Server { get; set; }
    public double Equity { get; set; }
}
