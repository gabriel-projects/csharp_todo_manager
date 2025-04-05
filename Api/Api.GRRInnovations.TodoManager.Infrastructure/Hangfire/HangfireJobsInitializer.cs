using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Hangfire;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Hangfire
{
    public class HangfireJobsInitializer
    {
        public static void ConfigureRecurringJobs()
        {
            RecurringJob.AddOrUpdate<ITaskCleanupService>(
                recurringJobId: "delete-old-completed-tasks",
                methodCall: service => service.DeleteOldCompletedTasksAsync(),
                cronExpression: Cron.Daily
            );
        }
    }
}
