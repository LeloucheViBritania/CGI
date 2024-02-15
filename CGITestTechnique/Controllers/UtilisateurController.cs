using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using CGITestTechnique.Database;
using CGITestTechnique.Models.Authentification;
using CGITestTechnique.Models.Utilisateur;
using CGI.API.Messages;
using Microsoft.EntityFrameworkCore;

namespace CGI.API.Controllers
{
    
    [Route("api/utilisateur", Name = "Utilisateur")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly CGIAppDbContext _cgiAppDbContext;

        public UtilisateurController(CGIAppDbContext cgiAppDbContext)
        {
            _cgiAppDbContext = cgiAppDbContext;
        }
        
        

        [HttpPost("/api/utilisateur/save", Name = "save")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UtilisateurResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> PostUtilisateur(UtilisateurRequest request)
        {

            if (request  == null || string.IsNullOrEmpty(request.Nom) == null || string.IsNullOrEmpty(request.Prenom) == null)return  BadRequest();

            try
            {
                var result = _cgiAppDbContext.utilisateurs.Add(new Utilisateur()
                {
                    est_actif = true,
                    adresse_geographique = request.AdressGeographique,
                    nom = request.Nom,
                    prenoms = request.Prenom
                });

                await _cgiAppDbContext.SaveChangesAsync();
                
                return Ok(new UtilisateurResponse()
                {
                    Id = result.Entity.id,
                    Nom =   result.Entity.nom,
                    Prenom = result.Entity.prenoms,
                    AdressGeographique = result.Entity.adresse_geographique    

                });
            }
            catch (Exception e)
            {
                return BadRequest("Erreur lors de l'enregistrement de l'utilisateur");
            }

          
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(Guid id, UtilisateurPuRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _cgiAppDbContext.Entry(request).State = EntityState.Modified;

            try
            {
                await _cgiAppDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurResponse>> GetUtilisateur(Guid id)
        {
            var utilisateur = await _cgiAppDbContext.utilisateurs.FindAsync(id);

            if (utilisateur == null || !utilisateur.est_actif)
            {
                return NotFound();
            }
            
            return new UtilisateurResponse
            {
                Nom = utilisateur.nom,
                Prenom = utilisateur.prenoms,
                AdressGeographique = utilisateur.adresse_geographique
                
            };
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(Guid id)
        {
            var utilisateur = await _cgiAppDbContext.utilisateurs.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            utilisateur.est_actif = false;
            
            _cgiAppDbContext.Entry(utilisateur).State = EntityState.Modified;

           
            await _cgiAppDbContext.SaveChangesAsync();

            return NoContent();
        }
        
        
        
        private bool UtilisateurExists(Guid id)
        {
            return _cgiAppDbContext.utilisateurs.Any(e => e.id == id);
        }
        
    }
}
