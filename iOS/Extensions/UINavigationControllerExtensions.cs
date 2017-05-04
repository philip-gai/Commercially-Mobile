// Created by Philip Gai

using System;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// UINavigation controller extensions.
	/// </summary>
	public static class UINavigationControllerExtensions
	{
		public enum NavigationType { Push, Present };

		/// <summary>
		/// Gets and pushes or presents view controller.
		/// </summary>
		/// <param name="nav">Nav.</param>
		/// <param name="nextController">Next controller.</param>
		/// <param name="type">Type.</param>
		public static void GetAndActOnViewController(this UINavigationController nav, UIViewController nextController, NavigationType type = NavigationType.Push)
		{
			switch (type) {
				case NavigationType.Push:
					nav.PushViewController(nextController, true);
					break;
				case NavigationType.Present:
					nav.PresentViewControllerAsync(nextController, true);
					break;
			}
		}

		/// <summary>
		/// Gets and pushes or presents view controller.
		/// </summary>
		/// <param name="nav">Nav.</param>
		/// <param name="storyboardName">Storyboard name.</param>
		/// <param name="type">Type.</param>
		public static void GetAndActOnViewController(this UINavigationController nav, string storyboardName, NavigationType type = NavigationType.Push)
		{
			var nextController = GetViewController(storyboardName);
			nav.GetAndActOnViewController(nextController, type);
		}

		/// <summary>
		/// Gets and pushes or presents view controller.
		/// </summary>
		/// <param name="nav">Nav.</param>
		/// <param name="notification">Notification.</param>
		/// <param name="type">Type.</param>
		public static void GetAndActOnViewController(this UINavigationController nav, NSNotification notification, NavigationType type = NavigationType.Push)
		{
			var storyboardName = notification.UserInfo[LocalConstants.Notifications.PushViewController.UserInfo].ToString();
			nav.GetAndActOnViewController(storyboardName, type);
		}

		/// <summary>
		/// Gets the view controller.
		/// </summary>
		/// <returns>The view controller.</returns>
		/// <param name="storyboardName">Storyboard name.</param>
		public static UIViewController GetViewController(string storyboardName)
		{
			var nextStoryboard = UIStoryboard.FromName(storyboardName, null);
			var nextController = nextStoryboard.InstantiateInitialViewController();
			return nextController;
		}

		/// <summary>
		/// Pops to view controller.
		/// </summary>
		/// <param name="nav">Nav.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void PopToViewController<T>(this UINavigationController nav)
		{
			int index = -1;
			int length = nav.ViewControllers.Length;
			for (int i = 0; i < nav.ViewControllers.Length; i++) {
				if (nav.ViewControllers[i] is T) {
					index = i;
					break;
				}
			}
			for (; index < length; index++) {
				nav.PopViewController(true);
			}
		}

		/// <summary>
		/// Shows the prompt.
		/// </summary>
		/// <param name="nav">Nav.</param>
		/// <param name="promptInfo">Prompt info.</param>
		/// <param name="length">Length.</param>
		public static void ShowPrompt(this UINavigationController nav, string promptInfo, int length = -1)
		{
			string ellipses = "...";
			var promptController = GetViewController(GlobalConstants.Screens.Prompt) as PromptController;
			promptController.SetPrompt(length <= -1 ? promptInfo : promptInfo.Length <= length ? promptInfo : promptInfo.Substring(0, length - ellipses.Length) + ellipses);
			promptController.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
			nav.PresentModalViewController(promptController, true);
			nav.View.BackgroundColor = UIColor.Black;
			nav.View.Alpha = (nfloat)0.5;
		}

		/// <summary>
		/// Shows the prompt.
		/// </summary>
		/// <param name="nav">Nav.</param>
		/// <param name="notification">Notification.</param>
		public static void ShowPrompt(this UINavigationController nav, NSNotification notification)
		{
			var promptInfo = notification.UserInfo[LocalConstants.Notifications.ShowPrompt.UserInfo].ToString();
			nav.ShowPrompt(promptInfo);
		}
	}
}