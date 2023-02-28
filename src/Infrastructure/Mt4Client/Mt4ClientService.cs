using CT;
using FluentValidation.Results;
using FSH.WebApi.ClientApiController;
using FSH.WebApi.Domain.MetaTrader4;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Mt4Client;
internal class Mt4ClientService : IClientService
{
    private readonly ILogger _logger = Log.ForContext(typeof(Mt4ClientService));
    private ConcurrentDictionary<Guid, IClientApiController> clientDic = new ConcurrentDictionary<Guid, IClientApiController>();
    public Mt4ClientService()
    {
    }
    public bool Add(TradeAccount account)
    {
        return false;
    }
    public bool Remove(TradeAccount account)
    {
       
        return false;
    }
}
