using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class LoginModel
	{
		public bool BadData { get; set; }

		[Required(ErrorMessage = "Введите имя пользователя")]
		public string Login
		{
			set;
			get;
		}

		[Required(ErrorMessage = "Введите пароль")]
		public string Password
		{
			set;
			get;
		}
	}
}