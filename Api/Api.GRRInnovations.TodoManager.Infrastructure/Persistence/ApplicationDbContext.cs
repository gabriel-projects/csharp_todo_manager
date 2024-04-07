using Api.GRRInnovations.TodoManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        internal DbSet<UserModel> Users { get; set; }
        internal DbSet<UserDetailModel> UsersDetails { get; set; }

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

            DefaultModelSetup<UserDetailModel>(modelBuilder);
            modelBuilder.Entity<UserDetailModel>().Ignore(m => m.Parent);
            modelBuilder.Entity<UserDetailModel>().HasOne(m => m.DbParent).WithOne(x => x.DbUserDetail).HasForeignKey<UserDetailModel>(x => x.ParentUid).OnDelete(DeleteBehavior.Cascade);
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
