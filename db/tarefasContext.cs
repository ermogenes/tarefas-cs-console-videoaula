using Microsoft.EntityFrameworkCore;

namespace Tarefas.db
{
    public partial class tarefasContext : DbContext
    {
        public tarefasContext()
        {
        }

        public tarefasContext(DbContextOptions<tarefasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tarefa> Tarefa { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;uid=root;pwd=1234;database=tarefas", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.ToTable("tarefa");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Concluida).HasColumnName("concluida");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(200)
                    .HasColumnName("descricao");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
