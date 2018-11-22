using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fx.Domain.ProductContext
{
    public class CatalogCommandExecutor : IRequestHandler<AddSubCatalogCommand, object>,
        IRequestHandler<AddParentCatalogCommand, object>
    {
        protected ICatalogRepository _catalogRepository;
        public CatalogCommandExecutor(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(ICatalogRepository));
        }

        public Task<object> Handle(AddSubCatalogCommand request, CancellationToken cancellationToken)
        {
            var newCatalog = new ProductCatalog
            {
                ParentId = request.ParentId,
                CatalogName = request.CatalogName
            };

            _catalogRepository.Add(newCatalog);
            return Task.FromResult((object)newCatalog);
        }

        public Task<object> Handle(AddParentCatalogCommand request, CancellationToken cancellationToken)
        {
            var newCatalog = new ProductCatalog
            {
                ParentId = request.ParentId,
                CatalogName = request.CatalogName
            };

            _catalogRepository.Add(newCatalog);

            return Task.FromResult((object)newCatalog);
        }
    }
}
