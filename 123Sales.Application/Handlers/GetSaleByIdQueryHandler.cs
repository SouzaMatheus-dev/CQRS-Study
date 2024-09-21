using _123Sales.Application.Dtos;
using _123Sales.Application.Queries;
using _123Sales.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace _123Sales.Application.Handlers
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleDto>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(request.SaleId);
            return _mapper.Map<SaleDto>(sale);
        }
    }
}