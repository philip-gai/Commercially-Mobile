﻿using System;
namespace Commercially
{
	public class UserDetails
	{
		public User User;
		public Request[] Requests;
		public const double HeaderHeight = 50;
		public const double RowHeight = 88;
		public const double RowAlphaDouble = 0.33;
		public const byte RowAlphaByte = 0x54;
		public readonly static string HeaderTitle = RequestStatusType.New.ToString();
		public readonly static Color TableHeaderColor = GlobalConstants.DefaultColors.Red;

		public string FirstLastText {
			get {
				return User.firstname + " " + User.lastname;
			}
		}

		public string EmailText {
			get {
				return User.username;
			}
		}

		public void GetRequests(Action OnSuccess, Action<Exception> IfException)
		{
			Session.TaskFactory.StartNew(delegate {
				try {
					Requests = RequestApi.GetRequests(User);
					OnSuccess.Invoke();
				} catch (Exception e) {
					IfException.Invoke(e);
				}
			});
		}
	}
}