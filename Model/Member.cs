﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace WpfVerein.Model
{
	public class Member
	{
		[Key]
		public int Id { get; set; }
		public int? Index { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public DateTime? BirthDay { get; set; }

		public string Name => Firstname + " " + Lastname;
		public string Birthday => BirthDay.Value.ToShortDateString();
	}
}
