using FSH.WebApi.Application.MetTrader4.TradeAccounts;
using FSH.WebApi.Domain.MetaTrader4;

namespace FSH.WebApi.Application.Catalog.Brands;

public class SearchTradeAccountsRequest : PaginationFilter, IRequest<PaginationResponse<TradeAccountDto>>
{
}

public class TradeAccountsBySearchRequestSpec : EntitiesByPaginationFilterSpec<TradeAccount, TradeAccountDto>
{
    public TradeAccountsBySearchRequestSpec(SearchTradeAccountsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Login, !request.HasOrderBy());
}

public class SearchTradeAccountsRequestHandler : IRequestHandler<SearchTradeAccountsRequest, PaginationResponse<TradeAccountDto>>
{
    private readonly IReadRepository<TradeAccount> _repository;

    public SearchTradeAccountsRequestHandler(IReadRepository<TradeAccount> repository) => _repository = repository;

    public async Task<PaginationResponse<TradeAccountDto>> Handle(SearchTradeAccountsRequest request, CancellationToken cancellationToken)
    {
        var spec = new TradeAccountsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}