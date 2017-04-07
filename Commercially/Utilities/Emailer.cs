using System;
//using MimeKit;
//using MailKit.Net.Smtp;

namespace Commercially {
	public static class Emailer {
		//public enum UniqueCodeEmailType { ActivateAccount, ResetPassword };

		//const string fromName = "Commercially LLC";
		//const string fromEmail = "PhilipGai714@gmail.com";
		//const string fromPassword = "SolaScriptura";
		//const string plainText = "plain";
		//const string host = "smtp.gmail.com";
		//const int port = 587;
		//const bool useSsl = false;

		//static MimeMessage previousMessage = null;

		//public static void SendEmail(User toUser, string subject, TextPart body, string fromName = fromName, string fromEmail = fromEmail, string fromPassword = fromPassword) {
		//	var message = new MimeMessage();
		//	message.From.Add(new MailboxAddress(fromName, fromEmail));
		//	message.To.Add(new MailboxAddress(toUser.firstName + " " + toUser.lastName, toUser.email));
		//	message.Subject = subject;
		//	message.Body = body;

		//	if (message == previousMessage) {
		//		throw new EmailAlreadySentException(Localizable.ExceptionMessages.AnEmailTo + toUser.email + Localizable.ExceptionMessages.AlreadySent);
		//	}
		//	using (var client = new SmtpClient()) {
		//		client.Connect(host, port, useSsl);
		//		client.Authenticate(fromEmail, fromPassword);
		//		client.Send(message);
		//		client.Disconnect(true);
		//	}
		//	previousMessage = message;
		//}

		//public static void SendUniqueCodeEmail(User user, UniqueCodeEmailType type) {
		//	string subject = "";
		//	TextPart body = null;
		//	string uniqueCode = Guid.NewGuid().ToString().Substring(0, 6);
		//	user.SetUniqueCode(uniqueCode);
		//	switch (type) {
		//		case UniqueCodeEmailType.ActivateAccount:
		//			subject = Localizable.EmailMessage.ActivationEmailSubject;
		//			body = new TextPart(plainText) {
		//				Text = Localizable.EmailMessage.Hello + user.firstName + "!\n\n" + Localizable.EmailMessage.WelcomeToPT + Localizable.EmailMessage.ActivationCode + user.uniqueCode +
		//								  "\n\n" + Localizable.EmailMessage.CopyPaste + "\n\n" + Localizable.EmailMessage.Sincerely + "\n\n" + Localizable.EmailMessage.Signature
		//			};
		//			break;
		//		case UniqueCodeEmailType.ResetPassword:
		//			subject = Localizable.EmailMessage.ResetPasswordSubject;
		//			body = new TextPart(plainText) {
		//				Text = Localizable.EmailMessage.Hello + user.firstName + "!\n\n" + Localizable.EmailMessage.ResetCode + user.uniqueCode +
		//								  "\n\n" + Localizable.EmailMessage.CopyPaste + "\n\n" + Localizable.EmailMessage.Sincerely + "\n\n" + Localizable.EmailMessage.Signature
		//			};
		//			break;
		//	}
		//	SendEmail(user, subject, body);
		//}
	}
}
