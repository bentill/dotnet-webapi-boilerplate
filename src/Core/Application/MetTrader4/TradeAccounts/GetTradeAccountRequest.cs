using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Domain.MetaTrader4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.MetTrader4.TradeAccounts;
public class GetTradeAccountRequest : IRequest<TradeAccountDto>
{
    public Guid Id { get; set; }

    public GetTradeAccountRequest(Guid id) => Id = id;
}
public class TradeAccountByIdSpec : Specification<TradeAccount, TradeAccountDto>, ISingleResultSpecification
{
    public TradeAccountByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}
public class TradeAccountRequestHandler : IRequestHandler<GetTradeAccountRequest, TradeAccountDto>
{
    private readonly IRepository<TradeAccount> _repository;
    private readonly IStringLocalizer _t;
    public TradeAccountRequestHandler(IRepository<TradeAccount> repository, IStringLocalizer<TradeAccountRequestHandler> localizer)
    {
        _repository = repository;
        this._t = localizer;
    }

    public async Task<TradeAccountDto> Handle(GetTradeAccountRequest request, CancellationToken cancellationToken)
    {
        var resut = await _repository.GetBySpecAsync(
            (ISpecification<TradeAccount, TradeAccountDto>)new TradeAccountByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["TradeAccount {0} Not Found.", request.Id]);
        return resut;
    }
}