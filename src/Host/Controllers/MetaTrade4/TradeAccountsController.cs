using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Application.MetTrader4.TradeAccounts;
using FSH.WebApi.Domain.MetaTrader4;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers.MetaTrade4;
public class TradeAccountsController : VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Get TradeAccount details.", "")]
    public Task<TradeAccountDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetTradeAccountRequest(id));
    }

    //[HttpPost("search")]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    //[OpenApiOperation("Search TradeAccounts using available filters.", "")]
    //public Task<PaginationResponse<TradeAccountDto>> SearchAsync(SearchTradeAccountsRequest request)
    //{
    //    return Mediator.Send(request);
    //}
}
