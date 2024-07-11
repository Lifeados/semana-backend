namespace Product.Domain.SeedWorks;

public interface IElasticTransaction
{
    void InitCurrentTransaction(string type);
    void InitManualCurrentTransaction(string name = null, string type = null, string distributedTracingDataString = null);
    void EndTransaction();
    void CaptureException(Exception e);
    Task CaptureSpan(string name, Dictionary<string, string> spanLabel, Func<Task> func);
    string GetOutgoingDistributedTracingData();
}