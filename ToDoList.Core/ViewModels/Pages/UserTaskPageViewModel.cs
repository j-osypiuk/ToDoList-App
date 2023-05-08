using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList;

namespace ToDoList.Core
{
	public class UserTaskPageViewModel : BaseViewModel
	{
        public ObservableCollection<UserTaskViewModel> UserTaskList { get; set; } = new ObservableCollection<UserTaskViewModel>() { 
            new UserTaskViewModel() {Title="Go to shopping", Description="Go to shop for food", Deadline=DateTime.Now},
			new UserTaskViewModel() {Title="Go to school", Description="Go to school for education", Deadline=DateTime.Now}
		};

		private string? _newUserTaskTitle;

		public string? NewUserTaskTitle
		{
			get { return this._newUserTaskTitle; }
			set { this._newUserTaskTitle = value; OnPropertyChanged(nameof(NewUserTaskTitle)); }
		}

		private string? _newUserTaskDescription;
		public string? NewUserTaskDescription
		{
			get { return this._newUserTaskDescription; }
			set { this._newUserTaskDescription = value; OnPropertyChanged(nameof(NewUserTaskDescription)); }
		}

        private DateTime? _newUserTaskDeadline = DateTime.Now;

        public DateTime? NewUserTaskDeadline
        {
            get { return this._newUserTaskDeadline; }
            set { this._newUserTaskDeadline = value; OnPropertyChanged(nameof(NewUserTaskDeadline)); }
        }

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
