﻿namespace ToDoList.Database
{
	public class UserTask
	{
        public int Id { get; set; }
        public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime Deadline { get; set; }
	}
}
