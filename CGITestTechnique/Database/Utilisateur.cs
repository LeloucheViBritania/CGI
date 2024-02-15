namespace CGITestTechnique.Database;

public class Utilisateur
{
    public Guid id { get; set; }
    public string nom { get; set; }
    public string prenoms { get; set; }
    public string adresse_geographique { get; set; }
    
    public string? telephone { get; set; }

    public bool est_actif { get; set; }
}