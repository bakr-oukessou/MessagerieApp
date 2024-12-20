namespace MessagerieApp.Models
{
    public class Contact
    {
        private int id;
        private int userId;
        private User user;
        private int contactUserId;
        private User contactUser;
        private string groupe;

        public Contact(int id, int userId, User user, int contactUserId, User contactUser, string groupeName)
        {
            Id = id;
            UserId = userId;
            User = user;
            ContactUserId = contactUserId;
            ContactUser = contactUser;
            GroupeName = groupeName;
        }
        public Contact() { }

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public User User { get => user; set => user = value; }
        public int ContactUserId { get => contactUserId; set => contactUserId = value; }
        public User ContactUser { get => contactUser; set => contactUser = value; }
        public string GroupeName { get => groupe; set => groupe = value; }
    }
}
