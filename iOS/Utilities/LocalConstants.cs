using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS {
	public struct LocalConstants {
		public struct Notifications {
			public struct PushViewController {
				public const string Name = "PushViewController";
				public const string UserInfo = "storyboardName";
			}
			public struct ShowPrompt {
				public const string Name = "ShowPrompt";
				public const string UserInfo = "PromptInfo";
			}
			public static string PopViewController = "PopViewController";
			public static string HidePrompt = "HidePrompt";
		}
		public struct Selectors {
			public const string PopAction = "PopAction";
		}
		public static nfloat LineHeight = 2;
		public static UIColor LineIncompleteColor = GlobalConstants.DefaultColors.Red.GetUIColor();
	}
}
