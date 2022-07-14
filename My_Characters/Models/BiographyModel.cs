using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    // Главная сущность.
    public class BiographyModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        
        private string? _name;
        [MaxLength(16)]
        public string? Name 
        { 
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string? _lastName;
        [MaxLength(16)]
        public string? LastName 
        { 
            get => _lastName; 
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        private int _age;
        public int Age 
        { 
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }
        private string? _biography;
        public string? Biography 
        { 
            get => _biography;
            set
            {
                _biography = value;
                OnPropertyChanged();
            }
        }

        private string? _skills;
        public string? Skills 
        {
            get => _skills;
            set
            {
                _skills = value;
                OnPropertyChanged();
            }
        }

        // Навигационные свойства.
        public ProgressModel? ProgressNavigation { get; set; }
        public ICollection<ReferenceModel>? ReferenceNavigation { get; set; }
        public ICollection<RenderModel>? RenderNavigation { get; set; }
        public ICollection<SourceFileModel>? SourceFileNavigation { get; set; }



        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
