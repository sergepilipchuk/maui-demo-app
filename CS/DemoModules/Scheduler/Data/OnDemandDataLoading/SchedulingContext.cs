using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.Maui.Storage;

namespace DemoCenter.Maui;

public class AppointmentEntity {
    public int Id { get; set; }
    public bool AllDay { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime QueryStart { get; set; }
    public DateTime QueryEnd { get; set; }
    public int AppointmentType { get; set; }
    public string RecurrenceInfo { get; set; }
    public string ReminderInfo { get; set; }
    public int Label { get; set; }
    public int Status { get; set; }
    public string TimeZoneId { get; set; }
}
public class SchedulingContext : DbContext {
    const string FileName = @"scheduling.db";
    static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, FileName);

    public static bool IsExist() {
        return File.Exists(DatabasePath);
    }
    static DbContextOptions<SchedulingContext> CreateOptions() {
        var connectionString = new SqliteConnectionStringBuilder { DataSource = FileName }.ConnectionString;
        var options = new DbContextOptionsBuilder<SchedulingContext>().UseSqlite(connectionString).Options;
        return options;
    }

    public SchedulingContext() : base(CreateOptions()) { }

    public DbSet<AppointmentEntity> AppointmentEntities { get; set; }

    public void SetAutoDetectChangesEnabled(bool value) {
        ChangeTracker.AutoDetectChangesEnabled = value;
    }
    public void ExecuteSql(string sql) {
        Database.ExecuteSqlRaw(sql);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Filename={DatabasePath}");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<AppointmentEntity>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });
    }
}

public class AppointmentEntityHelper {
    public static void CopyProperties(AppointmentEntity source, AppointmentEntity target) {
        target.AllDay = source.AllDay;
        target.AppointmentType = source.AppointmentType;
        target.Description = source.Description;
        target.End = source.End;
        target.Label = source.Label;
        target.Location = source.Location;
        target.QueryEnd = source.QueryEnd;
        target.QueryStart = source.QueryStart;
        target.RecurrenceInfo = source.RecurrenceInfo;
        target.ReminderInfo = source.ReminderInfo;
        target.Start = source.Start;
        target.Status = source.Status;
        target.Subject = source.Subject;
    }
}