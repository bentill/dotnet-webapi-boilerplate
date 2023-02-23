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
    public TradeAccount(uint login, string password, string server)
    {
        Login = login;
        Password = password;
        Server = server;
        Margin = 0;
        Equity =0;
    }

    public uint Login { get; private set; }
    public string Password { get; private set; }
    public string Server { get; private set; }
    public double Equity { get; set; }
    public double Margin { get; set; }
    public TradeAccount UpdatePassword(string? password)
    {
        if (password is not null && Password?.Equals(password) is not true) Password = password;
        return this;
    }
    public TradeAccount Update(double equity, double margin )
    {
        if (equity !=0 && Equity.Equals(equity) is not true) Equity = equity;

        if (margin != 0 && Margin.Equals(margin) is not true) Margin = margin;
        return this;
    }

}
