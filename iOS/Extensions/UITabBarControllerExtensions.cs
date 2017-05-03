using System;
using UIKit;

namespace Commercially.iOS
{
	public static class UITabBarControllerExtensions
	{
		public static void RemoveViewControllers(this UITabBarController tabBarController, Type[] controllerTypes)
		{
			var viewControllers = tabBarController.ViewControllers;
			foreach (var controller in viewControllers) {
				if (Array.Exists(controllerTypes, (Type type) => { return type == controller.GetType(); })) {
					controller.RemoveFromParentViewController();
				}
			}
		}
	}
}
