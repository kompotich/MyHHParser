using Microsoft.EntityFrameworkCore;
using MyHHParser.Data.Entities;

namespace MyHHParser.Data;

public sealed class HhParserDbContext : DbContext
{
	public HhParserDbContext(DbContextOptions<HhParserDbContext> options)
		: base(options)
	{
	}

	public DbSet<Vacancy> Vacancies => Set<Vacancy>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var vacancy = modelBuilder.Entity<Vacancy>();

		vacancy.ToTable("Vacancies");
		vacancy.HasKey(x => x.Id);

		vacancy.Property(x => x.ExternalId).HasMaxLength(64).IsRequired();
		vacancy.Property(x => x.Name).HasMaxLength(512).IsRequired();
		vacancy.Property(x => x.EmployerName).HasMaxLength(512);
		vacancy.Property(x => x.AreaName).HasMaxLength(256);
		vacancy.Property(x => x.Url).HasMaxLength(1024);
		vacancy.Property(x => x.SalaryCurrency).HasMaxLength(16);
		vacancy.Property(x => x.Experience).HasMaxLength(128);
		vacancy.Property(x => x.Employment).HasMaxLength(128);
		vacancy.Property(x => x.Schedule).HasMaxLength(128);
		vacancy.Property(x => x.ParsedAt).IsRequired();

		vacancy.HasIndex(x => x.ExternalId).IsUnique();
		vacancy.HasIndex(x => x.ParsedAt);
	}
}
