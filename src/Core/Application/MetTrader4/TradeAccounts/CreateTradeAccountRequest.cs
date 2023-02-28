using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Domain.MetaTrader4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.MetTrader4.TradeAccounts;
public class CreateTradeAccountRequest : IRequest<Guid>
{
    public uint Login { get; set; }
    public string? Password { get; set; }
    public string? Server { get; set; }
}

public class CreateTradeAccountRequestValidator : CustomValidator<CreateTradeAccountRequest>
{
    public CreateTradeAccountRequestValidator()
    {
    }
}
public class CreateTradeAccountRequestHandler : IRequestHandler<CreateTradeAccountRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<TradeAccount> _repository;

    public CreateTradeAccountRequestHandler(IRepositoryWithEvents<Domain.MetaTrader4.TradeAccount> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTradeAccountRequest request, CancellationToken cancellationToken)
    {
        var account = new TradeAccount();

        await _repository.AddAsync(account, cancellationToken);

        return account.Id;
    }
}
