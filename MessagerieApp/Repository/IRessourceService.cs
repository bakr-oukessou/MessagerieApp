using MessagerieApp.Models;

namespace MessagerieApp.Repository
{
    public interface IRessourceService
    {
        Task<List<Ressource>> ObtenirToutesLesRessources();
        Task<Ressource> AjouterRessource(Ressource ressource);
        Task<Ressource> ModifierRessource(Ressource ressource);
        Task SupprimerRessource(int id);
        Task<List<Ressource>> RechercherRessources(string critere);
    }
}
