// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// UITabBar controller extensions.
	/// </summary>
	public static class UITabBarControllerExtensions
	{
		/// <summary>
		/// Removes the view controllers.
		/// </summary>
		/// <param name="tabBarController">Tab bar controller.</param>
		/// <param name="controllerTypes">Controller types.</param>
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
