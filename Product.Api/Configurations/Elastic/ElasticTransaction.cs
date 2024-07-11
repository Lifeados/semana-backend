using Elastic.Apm;
using Elastic.Apm.Api;
using Product.Domain.SeedWorks;

namespace Product.Api.Configurations.Elastic;

public class ElasticTransaction : IElasticTransaction
{
    private ITransaction _transaction;

    public void InitCurrentTransaction(string type)
    {
        _transaction = Agent.Tracer.CurrentTransaction ?? Agent.Tracer.StartTransaction("Default", type);
    }

    public void InitManualCurrentTransaction(string name, string type = null, string distributedTracingDataString = null)
    {
        if (distributedTracingDataString == null)
            _transaction = Agent.Tracer.StartTransaction(name, type);
        else
        {
            var distributedTracingData = DistributedTracingData.TryDeserializeFromString(distributedTracingDataString);
            _transaction = Agent.Tracer.StartTransaction(name, type, distributedTracingData);
        }
    }

    public void EndTransaction()
    {
        _transaction?.End();
    }

    public void CaptureException(Exception e)
    {
        _transaction?.CaptureException(e);
    }

    public async Task CaptureSpan(string name, Dictionary<string, string> spanLabel, Func<Task> func)
    {
        await _transaction.CaptureSpan(name, ApiConstants.TypeMessaging, async (span) =>
        {
            foreach (var (key, value) in spanLabel)
            {
                span.SetLabel(key, value);
            }
            await func();
        });
    }

    public string GetOutgoingDistributedTracingData()
    {
        return _transaction?.OutgoingDistributedTracingData?.SerializeToString();
    }
}
