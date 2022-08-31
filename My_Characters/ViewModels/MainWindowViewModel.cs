using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using My_Characters.Commands;
using My_Characters.Context;
using My_Characters.Models;
using My_Characters.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {

        private ImageConvertor _imageConvertor = new ImageConvertor();
        private Bitmap? _image;
        public MainWindowViewModel()
        {
            GetAllData();
            GetFiltersAsync();
            StartView = DateTime.Now;
            FinishView = DateTime.Now;
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
                IEnumerable<BiographyModel> getCharacters = context.Biographies.AsNoTracking().ToList();
                CharactersView = new ObservableCollection<BiographyModel>(getCharacters);
            }
        }

        private RelayCommand _getAllCharacters;
        public RelayCommand GetAllCharactersCommand
        {
            get
            {
                return _getAllCharacters ?? new RelayCommand(parameter =>
                {
                    GetAllData();
                });
            }
        }
        #endregion

        #region // Добавить, удалить и изменить данные в разделе "БИОГРАФИЯ"  = СОЗДАТЬ ПЕРСОНАЖА:

        #region // ДОБАВИТЬ БИОГРАФИЮ ПЕРСОНАЖА

        #region // Общие поля:
        private string? _name;
        public string? NameView
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string? _lastName;
        public string? LastNameView
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
        private RankModel _selectedRankItemInCombobox;

        public RankModel SelectedRankItemInComboboxView
        {
            get => _selectedRankItemInCombobox;
            set
            {
                _selectedRankItemInCombobox = value;
                OnPropertyChanged();
            }
        }

        private string? _biography;
        public string? BiographyView
        {
            get => _biography;
            set
            {
                _biography = value;
                OnPropertyChanged();
            }
        }

        private string? _skills;
        public string? SkillsView
        {
            get => _skills;
            set
            {
                _skills = value;
                OnPropertyChanged();
            }
        }

        private byte[]? _avatarInByte;
        public byte[]? AvararView
        {
            get => _avatarInByte;
            set
            {
                _avatarInByte = value;
                OnPropertyChanged();
            }
        }

        private BiographyModel _selectCharacterInView;
        public BiographyModel SelectCharacterInView
        {
            get => _selectCharacterInView;
            set
            {
                if (value != null)
                {
                    _selectCharacterInView = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region // Создать нового персонажа:
        private async Task CreateCharacterAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                _image = new Bitmap(@"C:\Users\Denis\source\repos\My_Characters\My_Characters\Image\ava.png");
                AvararView = _imageConvertor.ConvertImageToByteArray(_image);

                var createBio = new BiographyModel()
                {
                    Name = NameView,
                    LastName = LastNameView,
                    Age = AgeView,
                    Biography = BiographyView,
                    Skills = SkillsView,
                    AvatarImage = AvararView,
                    Rank = SelectedRankItemInComboboxView.Name
                };

                await db.Biographies.AddAsync(createBio);

                await db.SaveChangesAsync();
            }
        }

        private RelayCommand? _createCharacter { get; set; }
        public RelayCommand CreateCharacterCommand
        {
            get
            {
                return _createCharacter ?? new RelayCommand(async parameter =>
                {
                    await CreateCharacterAsync();
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var idLastCharacter = await db.Biographies.OrderBy(x => x.Id).LastOrDefaultAsync();
                        var newCharacter = await db.Biographies.Where(cr => cr.Id == idLastCharacter!.Id).ToListAsync();
                        BiographyCharacterView = new ObservableCollection<BiographyModel>(newCharacter);
                        SelectCharacterInView = idLastCharacter!;
                    }
                    ToDoListView.Clear();
                    ReferencesView.Clear();
                    SourceFilesView.Clear();
                    RenderView.Clear();
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
        private RelayCommand? _updateCharacter { get; set; }
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
                    item.Rank = SelectedRankItemInComboboxView.Name;
                }
                await db.SaveChangesAsync();
            }
        }

        private RelayCommand? _saveChangesCharacter { get; set; }
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

        #region // Добавить, обновить аватар персонажа:
        private async Task AddAvatarAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                _image = new Bitmap(openFileDialog.FileName);
                AvararView = _imageConvertor.ConvertImageToByteArray(_image);

                using (ApplicationContext db = new ApplicationContext())
                {
                    var prog = await db.Biographies.Where(p => p.Id == SelectCharacterInView.Id).ToListAsync();
                    foreach (var item in prog)
                    {
                        item.AvatarImage = AvararView;
                    }
                    await db.SaveChangesAsync();
                }
            }
        }

        private RelayCommand? _addAvatar { get; set; }
        public RelayCommand AddAvatarCommand
        {
            get
            {
                return _addAvatar ?? new RelayCommand(async parameter =>
                {
                    await AddAvatarAsync();
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

        private RelayCommand? _deleteCharacter { get; set; }
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

        #region // ПОЛУЧИТЬ ИНФОРМАЦИЮ О ПЕРСОНАЖЕ:
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
            await GetToDoListAsync();
            await GetReferensecAsync();
            await GetSourceFilesAsync();
            await GetRenderAsync();
        }

        private RelayCommand? _getBioCharacter { get; set; }
        public RelayCommand GetBioCharacterCommand
        {
            get
            {
                return _getBioCharacter ?? new RelayCommand(async parameter =>
                {
                    var item = (BiographyModel)parameter;
                    SelectCharacterInView = item;
                    await GetBiographyCharacterAsync();
                }, patameter => patameter is BiographyModel);
            }
        }
        #endregion
        #endregion

        #region // Добавить, удалить и изменить данные в разделе "ЗАДАЧИ":

        #region // Общие поля:
        private string? _task;
        public string? TaskView
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _start;
        public DateTime? StartView
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged();
            }
        }
        private DateTime? _finish;
        public DateTime? FinishView
        {
            get => _finish;
            set
            {
                _finish = value;
                OnPropertyChanged();
            }
        }

        private ToDoListModel _selectItemInToDoList;
        public ToDoListModel SelectItemInToDoView
        {
            get => _selectItemInToDoList;
            set
            {
                if (value != null)
                {
                    _selectItemInToDoList = value;
                    OnPropertyChanged();
                    SaveCheckTaskAsync();
                }
            }
        }
        #endregion

        #region // Получить список задач:
        private ObservableCollection<ToDoListModel> _toDoList = new ObservableCollection<ToDoListModel>();
        public ObservableCollection<ToDoListModel> ToDoListView
        {
            get => _toDoList;
            set
            {
                _toDoList = value;
                OnPropertyChanged();
            }
        }

        private async Task GetToDoListAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var toDoList = await db.ToDoLists.Where(p => p.BiographyId == SelectCharacterInView.Id).ToListAsync();
                ToDoListView = new ObservableCollection<ToDoListModel>(toDoList);
            }
        }
        #endregion

        #region // Добавить задачу:
        private async Task AddTaskInAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var newTask = await db.Biographies.Where(p => p.Id == SelectCharacterInView.Id).ToListAsync();
                foreach (var item in newTask)
                {
                    
                    item.ProgressNavigation = new List<ToDoListModel>() { new ToDoListModel()
                    {
                        Task = TaskView,
                        Start = StartView,
                        Finish = FinishView
                    }};
                }
                await db.SaveChangesAsync();
            }
        }
        private RelayCommand? _addTaskinList { get; set; }
        public RelayCommand AddTaskInListCommand
        {
            get
            {
                return _addTaskinList ?? new RelayCommand(async parameter =>
                {
                    await AddTaskInAsync();
                    await GetToDoListAsync();
                });
            }
        }
        #endregion

        #region // Поставить/снять галку к задаче:
        private async Task SaveCheckTaskAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var checkTask = await db.ToDoLists.Where(toDo => toDo.Id == SelectItemInToDoView.Id).ToListAsync();
                foreach (var item in checkTask)
                {
                    if (item.CheckTask == true)
                    {
                        item.CheckTask = false;
                    }
                    else
                    {
                        item.CheckTask = true;
                    }
                }

                await db.SaveChangesAsync();
                await GetToDoListAsync();
            }
        }
        #endregion

        #region// Изменить задачу:
        private async Task UpdateTaskAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var selectTask = await db.ToDoLists.Where(ch => ch.Id == SelectItemInToDoView.Id).ToListAsync();

                foreach (var item in selectTask)
                {
                    TaskView = item.Task!;
                    StartView = item.Start;
                    FinishView = item.Finish;
                }
            }
        }

        private RelayCommand? _updateTask { get; set; }
        public RelayCommand UpdateTaskCommand
        {
            get
            {
                return _updateTask ?? new RelayCommand(async parameter =>
                {
                    var item = (ToDoListModel)parameter;
                    SelectItemInToDoView = item;
                    await UpdateTaskAsync();
                }, parameter => parameter is ToDoListModel);
            }
        }

        private async Task SaveChangesTaskAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var selectTask = await db.ToDoLists.Where(b => b.Id == SelectItemInToDoView.Id).ToListAsync();

                foreach (var item in selectTask)
                {
                    item.Task = TaskView;
                    item.Start = StartView;
                    item.Finish = FinishView;
                }
                await db.SaveChangesAsync();
            }
        }

        private RelayCommand? _saveChangesTask { get; set; }
        public RelayCommand SaveChangesTaskCommand
        {
            get
            {
                return _saveChangesTask ?? new RelayCommand(async parameter =>
                {
                    await SaveChangesTaskAsync();
                    await GetToDoListAsync();
                });
            }
        }
        #endregion

        #region// Удалить задачу:
        private async Task DeleteSelectTaskAsync(ToDoListModel selectTask)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (selectTask != null)
                {
                    db.ToDoLists.Remove(selectTask);
                    await db.SaveChangesAsync();
                }
            }
        }

        private RelayCommand? _deleteSelectTask { get; set; }
        public RelayCommand DeleteSelectTaskCommand
        {
            get
            {
                return _deleteSelectTask ?? new RelayCommand(async parameter =>
                {
                    var item = (ToDoListModel)parameter;
                    SelectItemInToDoView = item;
                    await DeleteSelectTaskAsync(SelectItemInToDoView);
                    await GetToDoListAsync();
                }, parameter => parameter is ToDoListModel);
            }
        }
        #endregion
        #endregion

        #region // Добавить, удалить данные в разделе "РЕФЕРЕНСЫ":

        #region // Общие:
        private ReferenceModel _selectInReferencesView;
        public ReferenceModel SelectInReferencesView
        {
            get => _selectInReferencesView;
            set
            {
                if (value != null)
                {
                    _selectInReferencesView = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region // Получить референсы:
        private ObservableCollection<ReferenceModel> _references = new ObservableCollection<ReferenceModel>();
        public ObservableCollection<ReferenceModel> ReferencesView
        {
            get => _references;
            set
            {
                _references = value;
                OnPropertyChanged();
            }
        }

        private async Task GetReferensecAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var getRef = await db.References.Where(x => x.BiographyId == SelectCharacterInView.Id).ToListAsync();
                ReferencesView = new ObservableCollection<ReferenceModel>(getRef);
            }
        }
        #endregion

        #region // Добавить референсов:
        private byte[]? _referenceInByte { get; set; }
        public byte[]? ReferenceInByte
        {
            get => _referenceInByte;
            set
            {
                if (value != null)
                {
                    _referenceInByte = value;
                    OnPropertyChanged();
                }
            }
        }

        private async Task OpenFileExplorerAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                _image = new Bitmap(openFileDialog.FileName);
                ReferenceInByte = _imageConvertor.ConvertImageToByteArray(_image);

                using (ApplicationContext db = new ApplicationContext())
                {
                    var prog = await db.Biographies.Where(p => p.Id == SelectCharacterInView.Id).ToListAsync();
                    foreach (var item in prog)
                    {
                        item.ReferenceNavigation = new List<ReferenceModel>() { new ReferenceModel()
                        {
                            ReferenceImage = ReferenceInByte
                        }};
                    }
                    await db.SaveChangesAsync();
                }
            }
        }

        private RelayCommand? _openFileDialog { get; set; }
        public RelayCommand OpenFileDialogCommand
        {
            get
            {
                return _openFileDialog ?? new RelayCommand(async parameter =>
                {
                    await OpenFileExplorerAsync();
                    await GetReferensecAsync();
                });
            }
        }
        #endregion

        #region // Удалить референсы:
        private async Task DeleteReferenceAsync(ReferenceModel selectItem)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (selectItem != null)
                {
                    db.References.Remove(selectItem);
                    await db.SaveChangesAsync();
                }
            }
        }

        private RelayCommand? _deleteSelectReferenc { get; set; }
        public RelayCommand DeleteSelectReferencCommand
        {
            get
            {
                return _deleteSelectReferenc ?? new RelayCommand(async parameter =>
                {
                    var item = (ReferenceModel)parameter;
                    SelectInReferencesView = item;
                    await DeleteReferenceAsync(SelectInReferencesView);
                    await GetReferensecAsync();
                }, parameter => parameter is ReferenceModel);
            }
        }
        #endregion
        #endregion

        #region // Добавить, удалить и изменить данные в разделе "ФАЙЛЫ":
        #region // Общие:
        private string? _pathFile;
        public string? PathFileView
        {
            get => _pathFile;
            set
            {
                _pathFile = value;
                OnPropertyChanged();
            }
        }

        private SourceFileModel _selectItemInSourceFile;

        public SourceFileModel SelectItemInSourceFile
        {
            get => _selectItemInSourceFile;
            set
            {
                if (value != null)
                {
                    _selectItemInSourceFile = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region // Получить файлы:
        private ObservableCollection<SourceFileModel> _sourceFiles = new ObservableCollection<SourceFileModel>();

        public ObservableCollection<SourceFileModel> SourceFilesView
        {
            get => _sourceFiles;
            set
            {
                _sourceFiles = value;
                OnPropertyChanged();
            }
        }
        #endregion
        private async Task GetSourceFilesAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var getSurseFiles = await db.SourceFiles.Where(id => id.BiographyId == SelectCharacterInView.Id).ToListAsync();
                SourceFilesView = new ObservableCollection<SourceFileModel>(getSurseFiles);
            }
        }

        #region // Добавить файл:
        private async Task OpenFileDialogAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {

                PathFileView = openFileDialog.FileName;

                using (ApplicationContext db = new ApplicationContext())
                {
                    var getNavigationProperty = await db.Biographies.Where(b => b.Id == SelectCharacterInView.Id).ToListAsync();
                    foreach (var item in getNavigationProperty)
                    {
                        item.SourceFileNavigation = new List<SourceFileModel>() { new SourceFileModel()
                        {
                            Path = PathFileView
                        }};
                    }
                    await db.SaveChangesAsync();
                }
                await GetSourceFilesAsync();
            }
        }

        private RelayCommand? _addSourceFaile { get; set; }
        public RelayCommand AddSourceFileCommand
        {
            get
            {
                return _addSourceFaile ?? new RelayCommand(async parameter =>
                {
                    await OpenFileDialogAsync();
                });
            }
        }
        #endregion

        #region // Удалить файл:
        private async Task DeleteItemInSourceFileAsync(SourceFileModel sourceFile)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (sourceFile != null)
                {
                    db.SourceFiles.Remove(sourceFile);
                    await db.SaveChangesAsync();
                }
            }
        }

        private RelayCommand? _deleteSelectItemInSourceFiles { get; set; } 
        public RelayCommand DeleteItemInSourceFileCommand
        {
            get
            {
                return _deleteSelectItemInSourceFiles ?? new RelayCommand(async parameter =>
                {
                    var item = (SourceFileModel)parameter;
                    SelectItemInSourceFile = item;
                    await DeleteItemInSourceFileAsync(SelectItemInSourceFile);
                    await GetSourceFilesAsync();
                }, parameter => parameter is SourceFileModel);
            }
        }
        #endregion

        #region // Запустить файл:
        private async Task StartProcessAsync()
        {
            await Task.Run(() => Process.Start(SelectItemInSourceFile.Path!));
        }

        private RelayCommand? _startProcess { get; set; }
        
        public RelayCommand StartProcessCommand
        {
            get
            {
                return _startProcess ?? new RelayCommand(async parameter =>
                {
                    var item = (SourceFileModel)parameter;
                    SelectItemInSourceFile = item;
                    await StartProcessAsync();
                }, parameter => parameter is SourceFileModel);
            }
        }
        #endregion
        #endregion

        #region // Добавить, удалить и изменить данные в разделе "Рендер":

        #region // Общие:
        private byte[]? _renderInByte { get; set; }
        public byte[]? RenderInByte
        {
            get => _renderInByte;
            set
            {
                _renderInByte = value;
                OnPropertyChanged();
            }
        }

        private RenderModel _selectItemInRender;
        public RenderModel SelectItemInRender
        {
            get => _selectItemInRender;
            set
            {
                if (value != null)
                {
                    _selectItemInRender = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region // Получить рендеры:
        private ObservableCollection<RenderModel> _getRender = new ObservableCollection<RenderModel>();
        public ObservableCollection<RenderModel> RenderView
        {
            get => _getRender;
            set
            {
                _getRender = value;
                OnPropertyChanged();
            }
        }

        private async Task GetRenderAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var getRender = await db.Renders.Where(x => x.BiographyId == SelectCharacterInView.Id).ToListAsync();
                RenderView = new ObservableCollection<RenderModel>(getRender);
            }
        }
        #endregion

        #region // Добавить рендер:
        private async Task OpenFileExplorerForRenderAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                _image = new Bitmap(openFileDialog.FileName);
                RenderInByte = _imageConvertor.ConvertImageToByteArray(_image);

                using (ApplicationContext db = new ApplicationContext())
                {
                    var prog = await db.Biographies.Where(p => p.Id == SelectCharacterInView.Id).ToListAsync();
                    foreach (var item in prog)
                    {
                        item.RenderNavigation = new List<RenderModel>() { new RenderModel()
                        {
                            RenderImage = RenderInByte
                        }};
                    }
                    await db.SaveChangesAsync();
                }
                await GetReferensecAsync();
            }
        }

        private RelayCommand? _addRender { get; set; }
        public RelayCommand AddRenderCommand
        {
            get
            {
                return _addRender ?? new RelayCommand(async parameneter =>
                {
                    await OpenFileExplorerForRenderAsync();
                    await GetRenderAsync();
                });
            }
        }
        #endregion

        #region // Удалить рендер:
        private async Task DeleteItemInRenderAsync(RenderModel item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (item != null)
                    db.Renders.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        private RelayCommand? _deleteInRender { get; set; }
        public RelayCommand DeleteInRenderCommand
        {
            get
            {
                return _deleteInRender ?? new RelayCommand(async parameter =>
                {
                    var item = (RenderModel)parameter;
                    SelectItemInRender = item;
                    await DeleteItemInRenderAsync(SelectItemInRender);
                    await GetRenderAsync();
                }, parameter => parameter is RenderModel);
            }
        }
        #endregion
        #endregion

        #region // Добавить, удалить фильтры:

        #region // Общие поля:
        private string? _nameRang;
        public string? NameRangView
        {
            get => _nameRang;
            set
            {
                if (value != null)
                {
                    _nameRang = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region // Получить фильтры:
        private ObservableCollection<RankModel> _getRank = new ObservableCollection<RankModel>();
        public ObservableCollection<RankModel> FilterRankView
        {
            get => _getRank;
            set
            {
                if (_getRank != null)
                {
                    _getRank = value;
                    OnPropertyChanged();
                }
            }
        }

        private async Task GetFiltersAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var getFilter = await db.Ranks.ToListAsync();
                FilterRankView = new ObservableCollection<RankModel>(getFilter);
            }
        }
        #endregion

        #region // Добавить фильтр:
        private async Task AddFilterRankAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var createFilterRank = new RankModel()
                { 
                    Name = NameRangView
                };
                await db.AddAsync(createFilterRank);
                await db.SaveChangesAsync();
            }
        }

        private RelayCommand _addFilterRank { get; set; }
        public RelayCommand AddFilterRankCommand
        {
            get
            {
                return _addFilterRank ?? new RelayCommand(async parameter =>
                {
                    await AddFilterRankAsync();
                    await GetFiltersAsync();
                });
            }
        }
        #endregion

        #region // Удалить фильтр:
        private async Task DeleteFiltersAsync(RankModel model)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (model != null)
                {
                    db.Ranks.Remove(model);
                    await db.SaveChangesAsync();
                }
            }
        }

        private RelayCommand _deleteFilter { get; set; }
        public RelayCommand DeleteFilterCommand
        {
            get
            {
                return _deleteFilter ?? new RelayCommand(async parameter =>
                {
                    var item = (RankModel)parameter;
                    SelectedFilterInView = item;
                    await DeleteFiltersAsync(SelectedFilterInView);
                    GetAllData();
                }, parameter => parameter is RankModel);
            }
        }
        #endregion

        #region // Изменить фильтр:
        private string? _nameFilter;
        private void UpdateFilter()
        {
            NameRangView = SelectedFilterInView.Name;
            _nameFilter = SelectedFilterInView.Name;
        }

        private RelayCommand _updateFilter;
        public RelayCommand UpdateFilterCommand
        {
            get
            {
                return _updateFilter ?? new RelayCommand(parameter =>
                {
                    var item = (RankModel)parameter;
                    SelectedFilterInView = item;
                    UpdateFilter();
                }, parameter => parameter is RankModel);
            }
        }

        private async Task SaveUpdateFilterAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var filter = await db.Ranks.Where(f => f.Name == _nameFilter).ToListAsync();
                var charachters = await db.Biographies.Where(c => c.Rank == _nameFilter).ToListAsync();
                foreach (var item in filter)
                {
                    item.Name = NameRangView;
                }

                foreach (var item in charachters)
                {
                    item.Rank = NameRangView;
                }
                await db.SaveChangesAsync();
            }
        }

        private RelayCommand _saveUpdateFilter;
        public RelayCommand SaveUpdateFilterCommand
        {
            get
            {
                return _saveUpdateFilter ?? new RelayCommand(async parameter =>
                {
                    await SaveUpdateFilterAsync();
                    await GetFiltersAsync();
                });
            }
        }
        #endregion

        #region // Применить фильтр:
        private RankModel _selectedFilterInView;
        public RankModel SelectedFilterInView
        {
            get => _selectedFilterInView;
            set
            {
                if (value != null)
                {
                    _selectedFilterInView = value;
                    OnPropertyChanged();
                    ApplyFilter();
                }
            }
        }
        private void ApplyFilter()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                IEnumerable<BiographyModel> applyFilter = db.Biographies.Where(f => f.Rank == SelectedFilterInView.Name).AsNoTracking().ToList();
                CharactersView = new ObservableCollection<BiographyModel>(applyFilter);
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
