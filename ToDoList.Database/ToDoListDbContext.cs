using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Database
{
	public class ToDoListDbContext : DbContext
	{
		public DbSet<UserTask> UserTasks { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer("Server=KUBA;Database=ToDoListApp;Trusted_Connection=True;TrustServerCertificate=True;");
		}
	}
}
