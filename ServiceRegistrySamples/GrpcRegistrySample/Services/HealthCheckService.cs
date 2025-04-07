using Grpc.Core;
using GrpcRegistrySample.Protos;

namespace GrpcRegistrySample.Services
{
    public class HealthCheckService : Health.HealthBase
    {
        public override Task<HealthCheckResponse> Check(HealthCheckRequest request, ServerCallContext context)
        {
            //return base.Check(request, context);
            //TODO: Implement health check logic

            return Task.FromResult(new HealthCheckResponse
            {
                Status = HealthCheckResponse.Types.ServingStatus.Serving
            });
        }

        public override async Task Watch(HealthCheckRequest request, IServerStreamWriter<HealthCheckResponse> responseStream, ServerCallContext context)
        {
            //return base.Watch(request, responseStream, context);
            //TODO: Implement health check logic

            await responseStream.WriteAsync(new HealthCheckResponse
            {
                Status = HealthCheckResponse.Types.ServingStatus.Serving
            });
        }
    }
}
