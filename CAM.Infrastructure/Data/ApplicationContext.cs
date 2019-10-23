using Microsoft.EntityFrameworkCore;
using CAM.Core.Entities;

namespace CAM.Infrastructure.Data
{
    /// <summary>
    /// Core context containing entities. See root of Web library for ERD.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discrepancy> Discrepancies { get; set; }
        public DbSet<DiscrepancyPart> DiscrepancyParts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LaborRecord> LaborRecords { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Squawk> Squawks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Times> Times { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
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
            // Many-to-many relationship between Aircraft and Owner
            modelBuilder.Entity<AircraftOwner>()
                .HasKey(bc => new { bc.AircraftId, bc.OwnerId });
            modelBuilder.Entity<AircraftOwner>()
                .HasOne(bc => bc.Aircraft)
                .WithMany(b => b.AircraftOwners)
                .HasForeignKey(bc => bc.AircraftId);
            modelBuilder.Entity<AircraftOwner>()
                .HasOne(bc => bc.Owner)
                .WithMany(c => c.AircraftOwners)
                .HasForeignKey(bc => bc.OwnerId);
        }
    }
}