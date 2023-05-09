using System.Collections.ObjectModel;
using System.Numerics;
using System.Windows.Input;
using System.Xml.Linq;
using ToDoList;
using ToDoList.Database;

namespace ToDoList.Core
{
	public class UserTaskPageViewModel : BaseViewModel
	{
        public ObservableCollection<UserTaskViewModel> UserTaskList { get; set; } = new ObservableCollection<UserTaskViewModel>();

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

        private DateTime _newUserTaskDeadline = DateTime.Now;

        public DateTime NewUserTaskDeadline
        {
            get { return this._newUserTaskDeadline; }
            set { this._newUserTaskDeadline = value; OnPropertyChanged(nameof(NewUserTaskDeadline)); }
        }

        public ICommand AddNewUserTaskCommand { get; set; }

        public ICommand RemoveSelectedUserTasksCommand { get; set; }

        public ICommand SortByDateUserTasksCommand { get; set; }
        public UserTaskPageViewModel()
        {
			AddNewUserTaskCommand = new RelayCommand(AddNewUserTask);
            RemoveSelectedUserTasksCommand = new RelayCommand(RemoveSelectedUserTasks);
            SortByDateUserTasksCommand = new RelayCommand(SortByDateUserTasks);

            foreach(var userTask in DatabaseConnector.database.UserTasks.ToList())
            {
                UserTaskList.Add(new UserTaskViewModel
                {
                    Id = userTask.Id,
                    Title = userTask.Title,
                    Description = userTask.Description,
                    Deadline = userTask.Deadline,
                });
            }
        }
        public void AddNewUserTask()
        {
            var newUserTask = new UserTaskViewModel
            {
                Title = NewUserTaskTitle,
                Description = NewUserTaskDescription,
                Deadline = NewUserTaskDeadline,

            };
            UserTaskList.Add(newUserTask);

            DatabaseConnector.database.UserTasks.Add(new UserTask
            {
                Title = newUserTask.Title,
                Description = newUserTask.Description,
                Deadline = newUserTask.Deadline,
            });

            DatabaseConnector.database.SaveChanges();

            GetUserTasks();

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

                var task = DatabaseConnector.database.UserTasks.FirstOrDefault(ut => ut.Id == userTask.Id);
                if (task != null)
                {
                    DatabaseConnector.database.UserTasks.Remove(task);
                }
            }

            DatabaseConnector.database.SaveChanges();
        }

        public void SortByDateUserTasks()
        {
            var sortedList = new List<UserTaskViewModel>(UserTaskList);
            sortedList.Sort((x1, x2) => DateTime.Compare(x1.Deadline, x2.Deadline));

            UserTaskList = new ObservableCollection<UserTaskViewModel>(sortedList);
            OnPropertyChanged(nameof(UserTaskList));
		}

        public void GetUserTasks()
        {
			int i = 0;
			foreach (var userTask in DatabaseConnector.database.UserTasks.ToList())
			{
				UserTaskList[i] = new UserTaskViewModel
				{
					Id = userTask.Id,
					Title = userTask.Title,
					Description = userTask.Description,
					Deadline = userTask.Deadline,
				};
				i++;
			}
		}
	}
}
