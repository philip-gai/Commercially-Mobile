namespace Commercially {
	public struct Localizable {
		public struct PromptMessages {
			public const string InvalidEmail = "Check that your email is valid.";
			public const string InvalidPassword = "Check that your password is valid.";
			public const string LoginError = "Incorrect email address or password.";
			public const string ButtonsError = "Error loading buttons.";
			public const string RequestsError = "Error loading requests.";
		}
		public struct ExceptionMessages {
			public const string CannotConnectServer = "Cannot connect to server";
			public const string NoConstraint = "No Constraint Matches the given attribute";
		}
		public struct Labels {
			public const string ToDo = "To Do";
			public const string Complete = "Complete";
			public const string InProgress = "In Progress";
			public const string MyTasks = "My Tasks";
		}
	}
}