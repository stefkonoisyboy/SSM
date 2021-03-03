namespace StudentsSocialMedia.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Data.Common.Models;
    using StudentsSocialMedia.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<MemberGroup> MemberGroups { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestParticipant> TestParticipants { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<UserSubject> UserSubjects { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<Hobby> Hobbies { get; set; }

        public DbSet<UserHobby> UsersHobbies { get; set; }

        public DbSet<Follower> Followers { get; set; }

        public DbSet<ForumCategory> ForumCategories { get; set; }

        public DbSet<ForumPost> ForumPosts { get; set; }

        public DbSet<ForumComment> ForumComments { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Choice> Choices { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Message> Messages { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.Followers)
                .WithOne(x => x.Followers)
                .HasForeignKey(x => x.FollowersId);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.Followings)
                .WithOne(x => x.Following)
                .HasForeignKey(x => x.FollowingId);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
