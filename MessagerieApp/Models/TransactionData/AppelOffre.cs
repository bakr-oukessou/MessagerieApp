using System.Reflection;

namespace MessagerieApp.Models.TransactionData
{
	public class AppelOffre : Notification
	{
		public int ResponsableId { get; set; }
		public List<AppelOffresItem> Items { get; set; }
	}
}
