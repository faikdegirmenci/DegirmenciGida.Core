using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Saga
{
    public class SagaOrchestrator<TRequest>
    {
        private readonly List<ISagaStep<TRequest, bool>> _steps = new List<ISagaStep<TRequest, bool>>();

        public void AddStep(ISagaStep<TRequest, bool> step)
        {
            _steps.Add(step);
        }

        public async Task<bool> ExecuteAsync(TRequest request)
        {
            var executedSteps = new Stack<ISagaStep<TRequest, bool>>();

            foreach (var step in _steps)
            {
                var result = await step.ExecuteAsync(request);
                if (!result)
                {
                    // Hata durumunda geri alınan adımlar
                    while (executedSteps.Count > 0)
                    {
                        var previousStep = executedSteps.Pop();
                        await previousStep.CompensateAsync(request);
                    }
                    return false;
                }
                executedSteps.Push(step);
            }
            return true;
        }
    }
}
