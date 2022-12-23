using Polly;
using Polly.Retry;

namespace Common.Resilience;
public static class RetryExtensions
{
    public static AsyncRetryPolicy<HttpResponseMessage> CreatePolicy(int retryCount)
    {
        return Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .RetryAsync(retryCount);
            
    }

}
