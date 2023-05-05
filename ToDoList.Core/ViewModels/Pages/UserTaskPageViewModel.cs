namespace ToDoList.Core
{
	public class UserTaskPageViewModel
	{
        public List<UserTaskViewModel> UserTaskList { get; set; } = new List<UserTaskViewModel>();

        public string NewUserTaskTitle { get; set; }

        public string NewUserTaskDescription { get; set; }

        public DateTime NewUserTaskDeadline { get; set; }

        public void AddNewTask()
        {
            var newUserTask = new UserTaskViewModel()
            {
                Title = NewUserTaskTitle,
                Description = NewUserTaskDescription,
                Deadline = NewUserTaskDeadline,
            };

            UserTaskList.Add(newUserTask);
        }
    }
}
