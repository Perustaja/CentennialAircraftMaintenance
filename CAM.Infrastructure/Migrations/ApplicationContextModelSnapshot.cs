﻿// <auto-generated />
using System;
using CAM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CAM.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("CAM.Core.Entities.Aircraft", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<bool>("IsTwin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<string>("SerialNum")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<int?>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Aircraft");
                });

            modelBuilder.Entity("CAM.Core.Entities.Discrepancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AircraftId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasMaxLength(60);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateFinalized")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resolution")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<int>("WorkOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorkStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.HasIndex("WorkOrderId");

                    b.ToTable("Discrepancies");
                });

            modelBuilder.Entity("CAM.Core.Entities.DiscrepancyPart", b =>
                {
                    b.Property<int>("DiscrepancyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PartId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Qty")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiscrepancyId", "PartId");

                    b.HasIndex("PartId");

                    b.ToTable("DiscrepancyParts");
                });

            modelBuilder.Entity("CAM.Core.Entities.DiscrepancyTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resolution")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int?>("WorkOrderTemplateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WorkOrderTemplateId");

                    b.ToTable("DiscrepancyTemplates");
                });

            modelBuilder.Entity("CAM.Core.Entities.DiscrepancyTemplatePart", b =>
                {
                    b.Property<int>("DiscrepancyTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PartId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Qty")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiscrepancyTemplateId", "PartId");

                    b.HasIndex("PartId");

                    b.ToTable("DiscrepancyTemplateParts");
                });

            modelBuilder.Entity("CAM.Core.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CertificationNum")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CAM.Core.Entities.LaborRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DiscrepancyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("LaborInHours")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DiscrepancyId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("LaborRecords");
                });

            modelBuilder.Entity("CAM.Core.Entities.Part", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CataloguePartNumber")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("CurrentStock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(600);

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageThumbPath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MfrsPartNumber")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int?>("MinimumStock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.Property<int>("PartCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PriceIn")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("PriceOut")
                        .HasColumnType("TEXT");

                    b.Property<string>("Vendor")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("PartCategoryId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("CAM.Core.Entities.PartCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("PartCategories");
                });

            modelBuilder.Entity("CAM.Core.Entities.Times", b =>
                {
                    b.Property<string>("AircraftId")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<int>("AirTime")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("AircraftTotal")
                        .HasColumnType("TEXT");

                    b.Property<int>("Cycles")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Engine1Total")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Engine2Total")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Hobbs")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Prop1")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Prop2")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Tach1")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Tach2")
                        .HasColumnType("TEXT");

                    b.HasKey("AircraftId");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("CAM.Core.Entities.WorkOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AircraftId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasMaxLength(60);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateFinalized")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<int>("WorkStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("WorkOrders");
                });

            modelBuilder.Entity("CAM.Core.Entities.WorkOrderTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WorkOrderTemplates");
                });

            modelBuilder.Entity("CAM.Core.Entities.WorkOrderTemplateDiscrepancyTemplate", b =>
                {
                    b.Property<int>("WorkOrderTemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DiscrepancyTemplateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("WorkOrderTemplateId", "DiscrepancyTemplateId");

                    b.HasIndex("DiscrepancyTemplateId");

                    b.ToTable("WorkOrderTemplateDiscrepancyTemplates");
                });

            modelBuilder.Entity("CAM.Core.Entities.Aircraft", b =>
                {
                    b.HasOne("CAM.Core.Entities.Times", "Times")
                        .WithOne("Aircraft")
                        .HasForeignKey("CAM.Core.Entities.Aircraft", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CAM.Core.Entities.Discrepancy", b =>
                {
                    b.HasOne("CAM.Core.Entities.Aircraft", "Aircraft")
                        .WithMany()
                        .HasForeignKey("AircraftId");

                    b.HasOne("CAM.Core.Entities.WorkOrder", null)
                        .WithMany("Discrepancies")
                        .HasForeignKey("WorkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CAM.Core.Entities.DiscrepancyPart", b =>
                {
                    b.HasOne("CAM.Core.Entities.Discrepancy", "Discrepancy")
                        .WithMany("DiscrepancyParts")
                        .HasForeignKey("DiscrepancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CAM.Core.Entities.Part", "Part")
                        .WithMany("DiscrepancyParts")
                        .HasForeignKey("PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CAM.Core.Entities.DiscrepancyTemplate", b =>
                {
                    b.HasOne("CAM.Core.Entities.WorkOrderTemplate", "WorkOrderTemplate")
                        .WithMany()
                        .HasForeignKey("WorkOrderTemplateId");
                });

            modelBuilder.Entity("CAM.Core.Entities.DiscrepancyTemplatePart", b =>
                {
                    b.HasOne("CAM.Core.Entities.DiscrepancyTemplate", "DiscrepancyTemplate")
                        .WithMany("DiscrepancyTemplateParts")
                        .HasForeignKey("DiscrepancyTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CAM.Core.Entities.Part", "Part")
                        .WithMany("DiscrepancyTemplateParts")
                        .HasForeignKey("PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CAM.Core.Entities.LaborRecord", b =>
                {
                    b.HasOne("CAM.Core.Entities.Discrepancy", null)
                        .WithMany("LaborRecords")
                        .HasForeignKey("DiscrepancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CAM.Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CAM.Core.Entities.Part", b =>
                {
                    b.HasOne("CAM.Core.Entities.PartCategory", "PartCategory")
                        .WithMany()
                        .HasForeignKey("PartCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CAM.Core.Entities.WorkOrderTemplateDiscrepancyTemplate", b =>
                {
                    b.HasOne("CAM.Core.Entities.DiscrepancyTemplate", "DiscrepancyTemplate")
                        .WithMany("WorkOrderTemplateDiscrepancyTemplates")
                        .HasForeignKey("DiscrepancyTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CAM.Core.Entities.WorkOrderTemplate", "WorkOrderTemplate")
                        .WithMany("WorkOrderTemplateDiscrepancyTemplates")
                        .HasForeignKey("WorkOrderTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
