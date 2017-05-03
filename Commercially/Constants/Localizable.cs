namespace Commercially
{
	public struct Localizable
	{
		public struct PromptMessages
		{
			public const string Oops = "Oops! ";
			public const string Success = "Success! ";
			public const string InvalidEmail = Oops + "Check that your email is valid.";
			public const string InvalidPassword = Oops + "Check that your password is valid.";
			public const string LoginError = Oops + "Check your email address or password.";
			public const string ButtonsError = Oops + "Buttons were unable to load.";
			public const string RequestsError = Oops + "Requests were unable to load.";
			public const string ChangesSaveError = Oops + "Changes were unable to save.";
			public const string AuthorizeError = Oops + "Unable to authorize this client.";
			public const string AssignError = Oops + "Assiging the request was unsuccessful.";
			public const string UsersError = Oops + "Users were unable to load.";
			public static string ClientsError = Oops + "Clients were unable to load.";
			public static string SaveSuccess = Success + "Your changes were successfully saved.";
			public const string PressAndHoldButton = "Press and hold the button for 10 seconds to pair.";
			public const string CannotCreateUser = Oops + "One or more fields was improperly filled out.";
			public const string UserCreateSuccess = Success + "You have created a new user.";
			public const string DeleteError = Oops + "Deletion failed.";
		}
		public struct ExceptionMessages
		{
			public const string CannotConnectServer = "Cannot connect to server";
			public const string NoConstraint = "No Constraint Matches the given attribute";
		}
		public struct Labels
		{
			public const string ToDo = "To Do";
			public const string Complete = "Complete";
			public const string InProgress = "In Progress";
			public const string MyTasks = "My Tasks";
			public const string NoneOption = "-- None --";
		}
	}
}