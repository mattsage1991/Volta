using Autofac;
using Quartz;
using Quartz.Spi;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.Quartz
{
    public class JobFactory : IJobFactory
    {
        private readonly ILifetimeScope container;

        public JobFactory(ILifetimeScope container)
        {
            this.container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var job = container.Resolve(bundle.JobDetail.JobType);

            return job as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
