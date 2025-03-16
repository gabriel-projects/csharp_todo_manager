using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Interfaces.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        internal DbSet<UserModel> Users { get; set; }
        internal DbSet<UserDetailModel> UsersDetails { get; set; }
        internal DbSet<TaskModel> Tasks { get; set; }
        internal DbSet<CategoryModel> Categories { get; set; }
        internal DbSet<TaskRecurrenceModel> TasksRecurrences { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DefaultModelSetup<UserModel>(modelBuilder);
            modelBuilder.Entity<UserModel>().Property(m => m.Login).IsRequired();
            modelBuilder.Entity<UserModel>().Property(m => m.Password).IsRequired();
            modelBuilder.Entity<UserModel>().HasIndex(m => m.Login).IncludeProperties(p => p.Password).IsUnique(false);
            modelBuilder.Entity<UserModel>().HasIndex(m => m.Login).IsUnique();
            modelBuilder.Entity<UserModel>().Ignore(m => m.UserDetail);
            modelBuilder.Entity<UserModel>().Ignore(m => m.Tasks);

            DefaultModelSetup<UserDetailModel>(modelBuilder);
            modelBuilder.Entity<UserDetailModel>().Ignore(m => m.User);
            modelBuilder.Entity<UserDetailModel>().HasOne(m => m.DbUser).WithOne(x => x.DbUserDetail).HasForeignKey<UserDetailModel>(x => x.UserUid).OnDelete(DeleteBehavior.Cascade);

            DefaultModelSetup<TaskModel>(modelBuilder);
            modelBuilder.Entity<TaskModel>().Ignore(m => m.User);
            modelBuilder.Entity<TaskModel>().Ignore(m => m.Category);
            modelBuilder.Entity<TaskModel>().Ignore(m => m.TaskRecurrence);
            modelBuilder.Entity<TaskModel>().HasOne(m => m.DbUser).WithMany(m => m.DbTasks).HasForeignKey(p => p.UserUid).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TaskModel>().Property(p => p.Status).HasConversion(new EnumToStringConverter<EStatusTask>());
            modelBuilder.Entity<TaskModel>().Property(p => p.Priority).HasConversion(new EnumToStringConverter<EPriorityTask>());

            DefaultModelSetup<CategoryModel>(modelBuilder);
            modelBuilder.Entity<CategoryModel>().Ignore(m => m.Tasks);
            modelBuilder.Entity<CategoryModel>().HasMany(m => m.DbTasks).WithOne(m => m.DbCategory).HasForeignKey(p => p.CategoryUid).OnDelete(DeleteBehavior.NoAction);

            DefaultModelSetup<TaskRecurrenceModel>(modelBuilder);
            modelBuilder.Entity<TaskRecurrenceModel>().Ignore(m => m.Task);
            modelBuilder.Entity<TaskRecurrenceModel>().HasOne(m => m.DbTask).WithOne(m => m.DbTaskRecurrence).HasForeignKey<TaskRecurrenceModel>(p => p.TaskUid).OnDelete(DeleteBehavior.NoAction);
        }

        public override int SaveChanges()
        {
            AdjustChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result;
            try
            {
                AdjustChanges();
                result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                RollBack();
                throw;
            }

            return result;
        }

        private void AdjustChanges()
        {
            var changesv3 = ChangeTracker.Entries<BaseModel>().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added);

            foreach (var entry in changesv3)
            {
                entry.Property(p => p.UpdatedAt).CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
        }

        public void RollBack()
        {
            var changedEntries = ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void DefaultModelSetup<T>(ModelBuilder modelBuilder) where T : BaseModel
        {
            modelBuilder.Entity<T>().Property(m => m.CreatedAt).IsRequired();
            modelBuilder.Entity<T>().Property(m => m.UpdatedAt).IsRequired();

            modelBuilder.Entity<T>().HasKey(m => m.Uid);
            modelBuilder.Entity<T>().Property((m) => m.Uid).IsRequired().HasValueGenerator<GuidValueGenerator>();
        }
    }
}
