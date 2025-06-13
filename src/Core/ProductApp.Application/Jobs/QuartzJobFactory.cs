using Quartz.Impl;
using Quartz;

namespace ProductApp.Application.Jobs;

public class QuartzJobFactory<T> where T : IJob
{
    public static async Task<IScheduler> CreateJobAsync(string jobName, string jobGroup, string triggerName, string triggerGroup, double startAt, int intervalInSeconds)
    {
        var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        await scheduler.Start();

        // Job tanımını oluşturuyoruz
        IJobDetail job = JobBuilder.Create<T>()
            .WithIdentity(jobName, jobGroup)  // Job adı ve grubu
            .Build();

        // Job için trigger (tetikleyici) oluşturuyoruz
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, triggerGroup)  // Trigger adı ve grubu
            .StartAt(DateTimeOffset.Now.AddSeconds(startAt))  // startAt saniye sonra başlasın
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(intervalInSeconds)  // intervalInSeconds saniyede bir çalışsın
                .RepeatForever())  // Sonsuz defa tekrarlansın
            .Build();

        // Job ve Trigger'ı Quartz scheduler'a ekliyoruz
        await scheduler.ScheduleJob(job, trigger);

        Console.WriteLine("Quartz Working ...");

        // Uygulama kapanmadan önce scheduler'ı durduruyoruz
        //await scheduler.Shutdown();

        return scheduler;
    }
}