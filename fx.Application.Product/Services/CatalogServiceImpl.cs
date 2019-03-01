using fx.Domain.core;
using fx.Domain.ProductContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Application.Product
{
    public class CatalogServiceImpl : ICatalogService
    {
        private readonly ICatalogRepository _repository;
        private readonly IMemoryBus _bus;
        public CatalogServiceImpl(ICatalogRepository repository, IMemoryBus bus)
        {
            _repository = repository??throw new ArgumentNullException(nameof(ICatalogRepository));
            _bus = bus?? throw new ArgumentNullException(nameof(IMemoryBus));
        }

        public void AddCatalog(string catalogName)
        {
            var createEvent = new ParentCatalogCreated(catalogName);
            _bus.RaiseEvent(createEvent);
        }

        public IEnumerable<ProductCatalog> GetAllProductCatalogs()
        {
            return _repository.GetAll();
        }
    }
}
