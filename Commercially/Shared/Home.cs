using System;
namespace Commercially
{
	public static class Home
	{
		public static void PrefetchData() {
			Session.TaskFactory.StartNew(delegate {
				Session.Clients = ClientApi.GetClients();
			});
		}
	}
}
