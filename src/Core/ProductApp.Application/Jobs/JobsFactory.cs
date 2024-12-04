using Quartz.Impl;
using Quartz;

namespace ProductApp.Application.Jobs;

public class JobsFactory
{
    public static async Task<IScheduler> CreateAsync()
    {
        var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        await scheduler.Start();

        // Job tanımını oluşturuyoruz
        IJobDetail job = JobBuilder.Create<SendMessageJob>()
            .WithIdentity("sendMessageJob", "group1")  // Job adı ve grubu
            .Build();

        // Job için trigger (tetikleyici) oluşturuyoruz
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")  // Trigger adı ve grubu
            .StartNow()  // Hemen başlasın
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(180)  // 10 saniyede bir çalışsın
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