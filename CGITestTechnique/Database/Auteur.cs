namespace CGITestTechnique.Database;

public class Auteur
{
    public Guid id { get; set; }

    public string? code { get; set; }

    public string? libelle { get; set; }

    public string? description { get; set; }

    public bool  est_actif { get; set; }
   
}