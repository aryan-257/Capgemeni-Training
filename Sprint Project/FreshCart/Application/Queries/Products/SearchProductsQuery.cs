using MediatR;
using FreshCart.Application.DTOs;
using FreshCart.Application.Commands.Cart;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace FreshCart.Application.Queries.Products;

public class SearchProductsQuery : IRequest<PagedResult<ProductDto>>
{
    public ProductSearchDto SearchDto { get; set; } = null!;
}

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, PagedResult<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SearchProductsQueryHandler> _logger;

    public SearchProductsQueryHandler(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<SearchProductsQueryHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<ProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var searchDto = request.SearchDto;
        
        _logger.LogInformation("Searching products with query: {Query}", searchDto.Query);

        var products = await _productRepository.SearchAsync(searchDto);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        // Calculate pagination
        var totalCount = products.Count();
        var skip = (searchDto.Page - 1) * searchDto.PageSize;
        var pagedProducts = productDtos.Skip(skip).Take(searchDto.PageSize);

        return new PagedResult<ProductDto>
        {
            Items = pagedProducts,
            TotalCount = totalCount,
            Page = searchDto.Page,
            PageSize = searchDto.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / searchDto.PageSize)
        };
    }
}

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}