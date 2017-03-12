namespace Commercially {
	public struct Localizable {
		public struct PromptMessages {
			public const string AnAccountFor = "An account for ";
			public const string AlreadyExists = " already exists";
			public const string MustActivateAccount = "You must activate your account before you login!";
			public const string UserWithEmail = "A user with the email ";
			public const string DoesNotExist = " does not exist";
			public const string PasswordReset = "Password successfully reset!";
			public const string EmailSent = "Email successfully sent!";
			public const string PasswordRequirements = "Password Requirements:\n- At least 8 characters long\n- At least one lowercase letter\n- At least one uppercase letter\n- At least one number\n- No spaces at beginning or end\n- One special character";
			public const string InvalidEmail = "Check that your email is valid.";
			public const string InvalidPassword = "Check that your password is valid.";
		}
		public struct ExceptionMessages {
			public const string CannotConnectServer = "Cannot connect to server";
			public const string AnEmailTo = "An email to ";
			public const string AlreadySent = " has already been sent";
			public const string UserExists = "User already exists";
			public const string NoConstraint = "No Constraint Matches the given attribute";
		}
		public struct EmailMessages {
			public const string Hello = "Hello ";
			public const string WelcomeToPT = "Welcome to Commercially.";
			public const string ActivationCode = " Your activation code is: ";
			public const string CopyPaste = "Please copy this code and paste it into the Commercially App.";
			public const string Sincerely = "Sincerely,";
			public const string Signature = "Philip Gai, CTO Commercially";
			public const string ActivationEmailSubject = "Commercially Account Activation Code";
			public const string ResetPasswordSubject = "Commercially: Reset Password";
			public const string ResetCode = "Your reset password code for your Commercially account is: ";
		}
		public struct Labels {
			public const string ToDo = "To Do";
			public const string Complete = "Complete";
			public const string InProgress = "In Progress";
		}
	}
}
