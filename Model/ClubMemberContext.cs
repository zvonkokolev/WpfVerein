using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfVerein.Model
{
	public class ClubMemberContext : DbContext
	{
		public DbSet<Member> Members { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite("Data Source=administration.db");
	}
}
