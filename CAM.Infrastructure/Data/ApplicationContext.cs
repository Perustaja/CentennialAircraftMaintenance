using Microsoft.EntityFrameworkCore;
using CAM.Core.Entities;
using CAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace CAM.Infrastructure.Data
{
    /// <summary>
    /// Core context containing entities. See root for ERD.
    /// </summary>
    public class ApplicationContext : DbContext, IUnitOfWork
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<Discrepancy> Discrepancies { get; set; }
        public DbSet<DiscrepancyPart> DiscrepancyParts { get; set; }
        public DbSet<DiscrepancyTemplate> DiscrepancyTemplates { get; set; }
        public DbSet<DiscrepancyTemplatePart> DiscrepancyTemplateParts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LaborRecord> LaborRecords { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartCategory> PartCategories { get; set; }
        public DbSet<Times> Times { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderTemplate> WorkOrderTemplates { get; set; }
        public DbSet<WorkOrderTemplateDiscrepancyTemplate> WorkOrderTemplateDiscrepancyTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many relationship between Discrepancy and Part
            modelBuilder.Entity<DiscrepancyPart>()
                .HasKey(bc => new { bc.DiscrepancyId, bc.PartId });
            modelBuilder.Entity<DiscrepancyPart>()
                .HasOne(bc => bc.Discrepancy)
                .WithMany(b => b.DiscrepancyParts)
                .HasForeignKey(bc => bc.DiscrepancyId);
            modelBuilder.Entity<DiscrepancyPart>()
                .HasOne(bc => bc.Part)
                .WithMany(c => c.DiscrepancyParts)
                .HasForeignKey(bc => bc.PartId);
            // Many-to-many relationship between DiscrepancyTemplate and Part
            modelBuilder.Entity<DiscrepancyTemplatePart>()
                .HasKey(bc => new { bc.DiscrepancyTemplateId, bc.PartId });
            modelBuilder.Entity<DiscrepancyTemplatePart>()
                .HasOne(bc => bc.DiscrepancyTemplate)
                .WithMany(b => b.DiscrepancyTemplateParts)
                .HasForeignKey(bc => bc.DiscrepancyTemplateId);
            modelBuilder.Entity<DiscrepancyTemplatePart>()
                .HasOne(bc => bc.Part)
                .WithMany(c => c.DiscrepancyTemplateParts)
                .HasForeignKey(bc => bc.PartId);
            // Many-to-many relationship between WorkOrderTemplate and DiscrepancyTemplate
            modelBuilder.Entity<WorkOrderTemplateDiscrepancyTemplate>()
                .HasKey(bc => new { bc.WorkOrderTemplateId, bc.DiscrepancyTemplateId });
            modelBuilder.Entity<WorkOrderTemplateDiscrepancyTemplate>()
                .HasOne(bc => bc.WorkOrderTemplate)
                .WithMany(b => b.WorkOrderTemplateDiscrepancyTemplates)
                .HasForeignKey(bc => bc.WorkOrderTemplateId);
            modelBuilder.Entity<WorkOrderTemplateDiscrepancyTemplate>()
                .HasOne(bc => bc.DiscrepancyTemplate)
                .WithMany(c => c.WorkOrderTemplateDiscrepancyTemplates)
                .HasForeignKey(bc => bc.DiscrepancyTemplateId);
            // Part soft delete filter
            modelBuilder.Entity<Part>().HasQueryFilter(p => !p.IsDeleted);
        }

        private IDbContextTransaction _transaction;

        public async Task BeginTransaction()
        {
            // Commented out during development as SQLite does not support this.
            // _transaction = await Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            try
            {
                await SaveChangesAsync();
            }
            finally
            {
                // Commented out during development as SQLite does not support this.
                // await _transaction.DisposeAsync();
            }
        }

        public async Task Rollback()
        {
            // Commented out during development as SQLite does not support this.
            // await _transaction.RollbackAsync();
            // await _transaction.DisposeAsync();
        }
    }
}