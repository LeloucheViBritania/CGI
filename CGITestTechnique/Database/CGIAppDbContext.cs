using Microsoft.EntityFrameworkCore;

namespace CGITestTechnique.Database;

public class CGIAppDbContext : DbContext
{
    public CGIAppDbContext(DbContextOptions<CGIAppDbContext> options) : base(options) { }
    
    public DbSet<Utilisateur> utilisateurs { get; set; }
    public DbSet<Auteur> auteurs { get; set; }
    
    
}