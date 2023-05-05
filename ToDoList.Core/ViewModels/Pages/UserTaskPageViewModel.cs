using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ToDoList.Core
{
	public class UserTaskPageViewModel : BaseViewModel
	{
        public ObservableCollection<UserTaskViewModel> UserTaskList { get; set; } = new ObservableCollection<UserTaskViewModel>();

        public string? NewUserTaskTitle { get; set; }

        public string? NewUserTaskDescription { get; set; }

        public DateTime NewUserTaskDeadline { get; set; } = DateTime.Now;

        public ICommand AddNewUserTaskCommand { get; set; }

        public ICommand RemoveSelectedUserTasksCommand { get; set; }

        public UserTaskPageViewModel()
        {
			AddNewUserTaskCommand = new RelayCommand(AddNewUserTask);
            RemoveSelectedUserTasksCommand = new RelayCommand(RemoveSelectedUserTasks);
        }
        public void AddNewUserTask()
        {
            var newUserTask = new UserTaskViewModel()
            {
                Title = NewUserTaskTitle,
                Description = NewUserTaskDescription,
                Deadline = NewUserTaskDeadline,
             
            };
            UserTaskList.Add(newUserTask);

            NewUserTaskTitle = string.Empty;
            NewUserTaskDescription = string.Empty;
            NewUserTaskDeadline = DateTime.Now;

            OnPropertyChanged(nameof(NewUserTaskTitle));
            OnPropertyChanged(nameof(NewUserTaskDescription));
            OnPropertyChanged(nameof(NewUserTaskDeadline));
        }

        public void RemoveSelectedUserTasks()
        {
            var selectedTasks = UserTaskList.Where(utl => utl.IsSelected).ToList();

			foreach (var userTask in selectedTasks)
            {
                UserTaskList.Remove(userTask);
            }
        }
	}
}
