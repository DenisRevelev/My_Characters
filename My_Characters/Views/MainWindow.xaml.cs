using My_Characters.Context;
using My_Characters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace My_Characters.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (infoChatacter.Visibility == Visibility.Collapsed)
                infoChatacter.Visibility = Visibility.Visible;
            else infoChatacter.Visibility = Visibility.Collapsed;

            // Активность кнопок в разеделе "БИОГРАФИЯ"
            CreateButton.IsEnabled = true;
            ChangedButton.IsEnabled = false;
            SaveButton.IsEnabled = false;
            DeleteButtun.IsEnabled = false;

            ChangedPhotoButton.IsEnabled = false;

            // Активность кладок табменю
            TaskTab.IsEnabled = false;
            ReferencesTab.IsEnabled = false;
            FilesTab.IsEnabled = false;
            RendersTab.IsEnabled = false;

            // Активность кнопок для раздела "ЗАДАЧИ"
            AddTaskButton.IsEnabled = false;
            SaveTaskButton.IsEnabled = false;

            // Активность кнопок раздела "РЕФЕРЕНСЫ"
            AddReferenceButton.IsEnabled = false;

            // Активность кнопок раздела "ФАЙЛЫ"
            AddFileButton.IsEnabled = false;

            // Активность кнопок раздела "РЕНДЕР"
            AddRenderButton.IsEnabled = false;

            // Очистить поля ввода, когда создается персонаж
            NameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            AgeTextBox.Text = string.Empty;
            BiographyTextBox.Text = string.Empty;
            SkillsTextBox.Text = string.Empty;
        }

        private void OpenPersen_Click(object sender, RoutedEventArgs e)
        {
            if (infoChatacter.Visibility == Visibility.Collapsed)
                infoChatacter.Visibility = Visibility.Visible;
            else infoChatacter.Visibility = Visibility.Collapsed;

            // Активность кнопок в разеделе "БИОГРАФИЯ"
            CreateButton.IsEnabled = false;
            ChangedButton.IsEnabled = true;
            SaveButton.IsEnabled = true;
            DeleteButtun.IsEnabled = true;

            ChangedPhotoButton.IsEnabled = true;

            // Активность кладок табменю
            TaskTab.IsEnabled = true;
            ReferencesTab.IsEnabled = true;
            FilesTab.IsEnabled = true;
            RendersTab.IsEnabled = true;

            // Активность кнопок для раздела "ЗАДАЧИ"
            AddTaskButton.IsEnabled = true;
            SaveTaskButton.IsEnabled = true;

            // Активность кнопок раздела "РЕФЕРЕНСЫ"
            AddReferenceButton.IsEnabled = true;

            // Активность кнопок раздела "ФАЙЛЫ"
            AddFileButton.IsEnabled = true;

            // Активность кнопок раздела "РЕНДЕР"
            AddRenderButton.IsEnabled = true;
        }

        private void ChangedButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChangedButton.Visibility == Visibility.Collapsed)
            {
                ChangedButton.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                ChangedButton.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Visible;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateButton.IsEnabled = false;
            ChangedButton.IsEnabled = true;
            SaveButton.IsEnabled = true;
            DeleteButtun.IsEnabled = true;

            ChangedPhotoButton.IsEnabled = true;

            TaskTab.IsEnabled = true;
            ReferencesTab.IsEnabled = true;
            FilesTab.IsEnabled = true;
            RendersTab.IsEnabled = true;

            AddTaskButton.IsEnabled = true;
            SaveTaskButton.IsEnabled = true;
            AddReferenceButton.IsEnabled = true;
            AddRenderButton.IsEnabled = true;
            AddFileButton.IsEnabled = true;
        }
    }
}
