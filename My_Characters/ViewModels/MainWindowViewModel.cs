using Microsoft.EntityFrameworkCore;
using My_Characters.Commands;
using My_Characters.Context;
using My_Characters.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
       
        public MainWindowViewModel()
        {
            GetAllData();
        }
        
        #region // Получить всех персонажей для главной страницы:

        private ObservableCollection<BiographyModel> _charactersView = new ObservableCollection<BiographyModel>();
        public ObservableCollection<BiographyModel> CharactersView
        {
            get => _charactersView;
            set
            {
                _charactersView = value;
                OnPropertyChanged();
            }
        }

        private void GetAllData()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                IEnumerable<BiographyModel> biographyIEnum = context.Biographies.ToList();
                CharactersView = new ObservableCollection<BiographyModel>(biographyIEnum);
            }
        }
        #endregion

        #region // Добавить, удалить и изменить данные в разделе "БИОГРАФИЯ":

        #region // ДОБАВИТЬ БИОГРАФИЮ ПЕРСОНАЖА = СОЗДАТЬ ПЕРСОНАЖА:

        #region // Общие поля:
        private string _name;
        public string NameView
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastNameView
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private int _age;
        public int AgeView
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        private string _biography;
        public string BiographyView
        {
            get => _biography;
            set
            {
                _biography = value;
                OnPropertyChanged();
            }
        }

        private string _skills;
        public string SkillsView
        {
            get => _skills;
            set
            {
                _skills = value;
                OnPropertyChanged();
            }
        }

        private BiographyModel _selectCharacterInView;
        public BiographyModel SelectCharacterInView
        {
            get => _selectCharacterInView;
            set
            {
                _selectCharacterInView = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region // Создать нового персонажа:
        private async Task CreateCharacterAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var createBio = new BiographyModel()
                {
                    Name = NameView,
                    LastName = LastNameView,
                    Age = AgeView,
                    Biography = BiographyView,
                    Skills = SkillsView
                };
                await db.Biographies.AddAsync(createBio);
                await db.SaveChangesAsync();
            }
        }

        private RelayCommand _createCharacter { get; set; }
        public RelayCommand CreateCharacterCommand
        {
            get
            {
                return _createCharacter ?? new RelayCommand(async parameter =>
                {
                    await CreateCharacterAsync();
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var firstCharacter = await db.Biographies.OrderBy(x => x.Id).LastOrDefaultAsync();
                        var newCharacter = await db.Biographies.Where(cr => cr.Id == firstCharacter.Id).ToListAsync();
                        BiographyCharacterView = new ObservableCollection<BiographyModel>(newCharacter);
                    }
                    GetAllData(); 
                });
            }
        }
        #endregion

        #region // Обновить данные о персонаже:
        private async Task UpdateCharacterAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var character = await db.Biographies.Where(ch => ch.Id == SelectCharacterInView.Id).ToListAsync();

                foreach (var item in character)
                {
                    NameView = item.Name!;
                    LastNameView = item.LastName!;
                    AgeView = item.Age;
                    BiographyView = item.Biography!;
                    SkillsView = item.Skills!;
                }
            }
        }
        private RelayCommand _updateCharacter { get; set; }
        public RelayCommand UpdateCharacterCommand
        {
            get
            {
                return _updateCharacter ?? new RelayCommand(async parameter =>
                {
                    await UpdateCharacterAsync();
                });
            }
        }

        private async Task SaveChangesCharacterAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var charecter = await db.Biographies.Where(b => b.Id == SelectCharacterInView.Id).ToListAsync();

                foreach (var item in charecter)
                {
                    item.Name = NameView;
                    item.LastName = LastNameView;
                    item.Age = AgeView;
                    item.Biography = BiographyView;
                    item.Skills = SkillsView;
                }
                await db.SaveChangesAsync();
            }
        }

        private RelayCommand _saveChangesCharacter { get; set; }
        public RelayCommand SaveChangesCharacterCommand
        {
            get
            {
                return _saveChangesCharacter ?? new RelayCommand(async parameter =>
                {
                    await SaveChangesCharacterAsync();
                    await GetBiographyCharacterAsync();
                    GetAllData();
                });
            }
        }
        #endregion

        #region // Удалить персонажа:
        private async Task DeleteCharacterAsync(BiographyModel model)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (model != null)
                {
                    db.Biographies.Remove(model);
                    await db.SaveChangesAsync();
                }
            }
        }

        private RelayCommand _deleteCharacter { get; set; }
        public RelayCommand DeleteCharacterCommand
        {
            get
            {
                return _deleteCharacter ?? new RelayCommand(async parameter =>
                {
                    await DeleteCharacterAsync(SelectCharacterInView);
                    await GetBiographyCharacterAsync();
                    GetAllData();
                });
            }
        }
        #endregion
        #endregion

        #region // ПОЛУЧИТЬ БИОГРАФИЮ ПЕРСОНАЖА:
        private ObservableCollection<BiographyModel> _biographyCharacterView = new ObservableCollection<BiographyModel>();
        public ObservableCollection<BiographyModel> BiographyCharacterView
        {
            get => _biographyCharacterView;
            set
            {
                _biographyCharacterView = value;
                OnPropertyChanged();
            }
        }

        private async Task GetBiographyCharacterAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var bio = await db.Biographies.Where(b => b.Id == SelectCharacterInView.Id).ToListAsync();
                BiographyCharacterView = new ObservableCollection<BiographyModel>(bio);
            }
        }
        
        private RelayCommand _getBioCharacter { get; set; }
        public RelayCommand GetBioCharacterCommand
        {
            get
            {
                return _getBioCharacter ?? new RelayCommand(async parameter =>
                {
                    await GetBiographyCharacterAsync();
                });
            }
        }
        #endregion
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
