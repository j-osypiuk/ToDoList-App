namespace ToDoList.Core
{
	public class UserTaskViewModel
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime? Deadline { get; set; }
        public bool IsSelected { get; set; }
    }
}
