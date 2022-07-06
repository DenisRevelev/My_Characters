using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    // Зависимая сущность от ProgressModel. Связь один ко многим. 
    public class ToDoListModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string? _task;
        public string? Task 
        { 
            get => _task;
            set
            { 
                _task = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _start;
        public DateTime? Start
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged();
            }
        }
        private DateTime? _finish;
        public DateTime? Finish 
        { 
            get => _finish;
            set 
            {
                _finish = value;
                OnPropertyChanged();
            }
        }
        public bool _checkTask;
        public bool CheckTask 
        { 
            get => _checkTask;
            set
            {
                _checkTask = value;
                OnPropertyChanged();
            }
        }

        // Внешний ключ.
        public int ProgressId { get; set; }

        // Навигационное свойство.
        public ProgressModel Progress { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
