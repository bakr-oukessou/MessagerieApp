using MessagerieApp.Models;

namespace MessagerieApp.Business
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public async Task<User> UpdateProfile(int userId, User updatedUser)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            user.Nom = updatedUser.Nom;
            user.Prenom = updatedUser.Prenom;
            user.DateNaissance = updatedUser.Date_naissance;
            user.Niveau = updatedUser.Niveau;
            user.Filiere = updatedUser.Filiere;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Contact> AddContact(int userId, int contactUserId, string groupeName = null)
        {
            var contact = new Contact
            {
                UserId = userId,
                ContactUserId = contactUserId,
                GroupeName = groupeName ?? "Défaut"
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<List<Contact>> GetUserContacts(int userId)
        {
            return await _context.Contacts
                .Where(c => c.UserId == userId)
                .Include(c => c.ContactUser)
                .ToListAsync();
        }

        public async Task<List<Contact>> GetContactsByGroup(int userId, string groupeName)
        {
            return await _context.Contacts
                .Where(c => c.UserId == userId && c.GroupeName == groupeName)
                .Include(c => c.ContactUser)
                .ToListAsync();
        }
    }
}
