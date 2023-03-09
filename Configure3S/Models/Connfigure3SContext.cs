using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Configure3S.Models
{
    public partial class Connfigure3SContext : DbContext
    {
        public Connfigure3SContext()
        {
        }

        public Connfigure3SContext(DbContextOptions<Connfigure3SContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbApiconfigure> TbApiconfigures { get; set; }
        public virtual DbSet<TbCompany> TbCompanies { get; set; }
        public virtual DbSet<TbSchedule> TbSchedules { get; set; }
        public virtual DbSet<TbTableEpicor> TbTableEpicors { get; set; }
        public virtual DbSet<TbTask> TbTasks { get; set; }
        public virtual DbSet<TbTaskDtl> TbTaskDtls { get; set; }
        public virtual DbSet<TbUser> TbUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-82EJ2NL2\\SQLEXPRESS;Initial Catalog=Connfigure3S;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<TbApiconfigure>(entity =>
            {
                entity.HasKey(e => e.SourceId);

                entity.ToTable("tbAPIConfigure");

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.Apikey)
                    .IsUnicode(false)
                    .HasColumnName("APIKey");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CompanyID");

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Urlapi)
                    .IsUnicode(false)
                    .HasColumnName("URLAPI");

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("tbCompany");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CompanyID");

                entity.Property(e => e.CompanyName).HasMaxLength(250);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.ToTable("tbSchedule");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CompanyID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.TimeStart).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TbSchedules)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_tbSchedule_tbTask");
            });

            modelBuilder.Entity<TbTableEpicor>(entity =>
            {
                entity.HasKey(e => e.TableId);

                entity.ToTable("tbTableEpicor");

                entity.Property(e => e.TableId)
                    .ValueGeneratedNever()
                    .HasColumnName("TableID");

                entity.Property(e => e.TableName).IsUnicode(false);
            });

            modelBuilder.Entity<TbTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToTable("tbTask");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CompanyID");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.TbTasks)
                    .HasForeignKey(d => d.SourceId)
                    .HasConstraintName("FK_tbTask_tbAPIConfigure");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.TbTasks)
                    .HasForeignKey(d => new { d.CreateUser, d.CompanyId })
                    .HasConstraintName("FK_tbTask_tbUser");
            });

            modelBuilder.Entity<TbTaskDtl>(entity =>
            {
                entity.HasKey(e => e.TaskDtlId);

                entity.ToTable("tbTaskDtl");

                entity.Property(e => e.TaskDtlId).HasColumnName("TaskDtlID");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.TbTaskDtls)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("FK_tbTaskDtl_tbTableEpicor");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TbTaskDtls)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_tbTaskDtl_tbTask");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CompanyId });

                entity.ToTable("tbUser");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CompanyID");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbUser_tbCompany");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
