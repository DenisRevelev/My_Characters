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

        public MainWindowViewModel()
        {
            GetAllData();
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
                var createBio = new BiographyModel()
                {
                    Name = NameView,
                    LastName = LastNameView,
                    Age = AgeView,
                    Biography = BiographyView,
                    Skills = SkillsView
                };
                await db.Biographies.AddAsync(createBio);

                var createProg = new ProgressModel()
                {
                    Progress = ProgressView,
                    Biography = createBio
                };
                await db.AddAsync(createProg);

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
                        var idLastCharacter = await db.Biographies.OrderBy(x => x.Id).LastOrDefaultAsync();
                        var newCharacter = await db.Biographies.Where(cr => cr.Id == idLastCharacter.Id).ToListAsync();
                        BiographyCharacterView = new ObservableCollection<BiographyModel>(newCharacter);
                        SelectCharacterInView = idLastCharacter;
                    }
                    ToDoListView.Clear();
                    ReferencesView.Clear();
                    SourceFilesView.Clear();
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
            await GetToDoListInProgressAsync();
            await GetReferensecAsync();
            await GetSourceFilesAsync();
            await GetRenderAsync();
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

        #region // Добавить, удалить и изменить данные в разделе "ПРОГРЕСС":
        #region // Общие поля:

        private int _progress;
        public int ProgressView
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }
        private bool _statusProgeress;

        public bool StatusProgeressView
        {
            get => _statusProgeress;
            set
            {
                _statusProgeress = value;
                OnPropertyChanged();
            }
        }

        private string _task;
        public string TaskView
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

        private async Task GetToDoListInProgressAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int v = 0;
                if (await db.ToDoLists.CountAsync() != 0)
                    v = 100 / await db.ToDoLists.CountAsync();

                var toDo = await db.ToDoLists.Where(p => p.Progress.BiographyId == SelectCharacterInView.Id).ToListAsync();
                ToDoListView = new ObservableCollection<ToDoListModel>(toDo);

                if (toDo.Where(x => x.CheckTask == true).Count() != 0)
                    ProgressView = v * toDo.Where(x => x.CheckTask == true).Count();
            }
        }
        #endregion

        #region // Добавить задачу:
        private async Task AddTaskInProgress()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var prog = await db.Progresses.Where(p => p.BiographyId == SelectCharacterInView.Id).ToListAsync();
                foreach (var item in prog)
                {
                    
                    item.ToDoListNavigation = new List<ToDoListModel>() { new ToDoListModel()
                    {
                        Task = TaskView,
                        Start = StartView,
                        Finish = FinishView
                    }};
                }
                await db.SaveChangesAsync();
            }
        }
        private RelayCommand _addTaskinProgress;
        public RelayCommand AddTaskInProgressCommand
        {
            get
            {
                return _addTaskinProgress ?? new RelayCommand(async parameter =>
                {
                    await AddTaskInProgress();
                    await GetToDoListInProgressAsync();
                });
            }
        }
        #endregion

        private ToDoListModel _selectItemInToDo;
        public ToDoListModel SelectItemInToDoView
        {
            get => _selectItemInToDo;
            set
            {
                if (value != null)
                {
                    _selectItemInToDo = value;
                    OnPropertyChanged();
                    SaveCheckTask();
                }
            }
        }

        private async Task SaveCheckTask()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int v = 0;
                if (await db.ToDoLists.CountAsync() != 0)
                    v = 100 / await db.ToDoLists.CountAsync();
                var checkTask = await db.ToDoLists.Where(toDo => toDo.Id == SelectItemInToDoView.Id).ToListAsync();
                foreach (var item in checkTask)
                {
                    if (item.CheckTask == true)
                    {
                        item.CheckTask = false;
                        ProgressView -= v;
                    }
                    else
                    {
                        item.CheckTask = true;
                        ProgressView += v;
                    }
                }

                await db.SaveChangesAsync();
                await GetToDoListInProgressAsync();

                if (await db.ToDoLists.Where(c => c.CheckTask == false).CountAsync() == 0)
                    ProgressView = 100;
            }
        }

        #region// Изменить задачу:
        private async Task UpdateTaskAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var selectTask = await db.ToDoLists.Where(ch => ch.Id == SelectItemInToDoView.Id).ToListAsync();

                foreach (var item in selectTask)
                {
                    TaskView = item.Task;
                    StartView = item.Start;
                    FinishView = item.Finish;
                }
            }
        }

        private RelayCommand _updateTask { get; set; }
        public RelayCommand UpdateTaskCommand
        {
            get
            {
                return _updateTask ?? new RelayCommand(async parameter =>
                {
                    await UpdateTaskAsync();
                });
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

        private RelayCommand _saveChangesTask { get; set; }
        public RelayCommand SaveChangesTaskCommand
        {
            get
            {
                return _saveChangesTask ?? new RelayCommand(async parameter =>
                {
                    await SaveChangesTaskAsync();
                    await GetToDoListInProgressAsync();
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
        private RelayCommand _deleteSelectTask { get; set; }
        public RelayCommand DeleteSelectTaskCommand
        {
            get
            {
                return _deleteSelectTask ?? new RelayCommand(async parameter =>
                {
                    await DeleteSelectTaskAsync(SelectItemInToDoView);
                    await GetToDoListInProgressAsync();
                });
            }
        }
        #endregion
        #endregion

        #region // Добавить, удалить данные в разделе "РЕФЕРЕНСЫ":
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
        private ImageConvertor _imageConvertor = new ImageConvertor();
        private Bitmap _image;

        private byte[] _ReferenceInByte { get; set; }
        public byte[] ReferenceInByte
        {
            get => _ReferenceInByte;
            set
            {
                _ReferenceInByte = value;
                OnPropertyChanged();
            }
        }

        private async Task OpenFileExplorer()
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

        private RelayCommand _openFileDialog { get; set; }
        public RelayCommand OpenFileDialogCommand
        {
            get
            {
                return _openFileDialog ?? new RelayCommand(async parameter =>
                {
                    await OpenFileExplorer();
                    await GetReferensecAsync();
                });
            }
        }
        #endregion

        #region // Удалить референсы:
        private ReferenceModel _selectInReferencesView;
        public ReferenceModel SelectInReferencesView
        {
            get => _selectInReferencesView;
            set
            {
                _selectInReferencesView = value;
                OnPropertyChanged();
            }
        }

        private async Task DeleteReference(ReferenceModel selectItem)
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

        private RelayCommand _deleteSelectReferenc { get; set; }
        public RelayCommand DeleteSelectReferencCommand
        {
            get
            {
                return _deleteSelectReferenc ?? new RelayCommand(async parameter =>
                {
                    await DeleteReference(SelectInReferencesView);
                    await GetReferensecAsync();
                });
            }
        }
        #endregion
        #endregion

        #region // Добавить, удалить и изменить данные в разделе "ФАЙЛЫ":
        private string _pathFile;
        public string PathFileView
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
        private async Task OpenFileDialog()
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

        private RelayCommand _addSourceFaile { get; set; }
        public RelayCommand AddSourceFileCommand
        {
            get
            {
                return _addSourceFaile ?? new RelayCommand(async parameter =>
                {
                    await OpenFileDialog();
                });
            }
        }
        #endregion

        #region // Изменить файл:
        #endregion

        #region // Удалить файл:
        private async Task DeleteItemInSourceFile(SourceFileModel sourceFile)
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

        private RelayCommand _deleteItemInSourceFiles { get; set; } 
        public RelayCommand DeleteItemInSourceFileCommand
        {
            get
            {
                return _deleteItemInSourceFiles ?? new RelayCommand(async parameter =>
                {
                    await DeleteItemInSourceFile(SelectItemInSourceFile);
                    await GetSourceFilesAsync();
                });
            }
        }
        #endregion

        #region // Запустить файл:
        private async Task StartProcess()
        {
            Process.Start(SelectItemInSourceFile.Path!);
        }

        private RelayCommand _startProcess { get; set; }
        
        public RelayCommand StartProcessCommand
        {
            get
            {
                return _startProcess ?? new RelayCommand(async parameter =>
                {
                    await StartProcess();
                });
            }
        }
        #endregion

        #endregion

        #region // Добавить, удалить и изменить данные в разделе "Рендер":
        private byte[] _renderInByte { get; set; }
        public byte[] RenderInByte
        {
            get => _renderInByte;
            set
            {
                _renderInByte = value;
                OnPropertyChanged();
            }
        }

        #region // Получить рендеры:
        private ObservableCollection<RenderModel> _getRender = new ObservableCollection<RenderModel>();
        public ObservableCollection<RenderModel> GetRenderView
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
                GetRenderView = new ObservableCollection<RenderModel>(getRender);
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

        private RelayCommand _addRender { get; set; }
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

        private async Task DeleteItemInRenderAsync(RenderModel item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (item != null)
                    db.Renders.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        private RelayCommand _deleteInRender { get; set; }
        public RelayCommand DeleteInRenderCommand
        {
            get
            {
                return _deleteInRender ?? new RelayCommand(async parameter =>
                {
                    await DeleteItemInRenderAsync(SelectItemInRender);
                    await GetRenderAsync();
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
