using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public abstract class OperationBase<TRequest, TResponse,TService>
    {
        protected readonly TService _service;

        public OperationBase(TService service)
        {
            _service = service;
        }
        public abstract Task<TResponse> Execute(TRequest request);
    }
}
