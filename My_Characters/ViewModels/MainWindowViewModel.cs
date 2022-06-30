using My_Characters.Context;
using My_Characters.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.ViewModels
{
    internal class MainWindowViewModel
    {
        private ObservableCollection<BiographyModel> _biographies;
        public MainWindowViewModel()
        {
            _biographies = new ObservableCollection<BiographyModel>();
            GetAllData();
        }
        public ObservableCollection<BiographyModel> BiographyModelsView
        {
            get => _biographies;
            set
            {
                _biographies = value;
            }
        }

        private void GetAllData()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var result = context.Biographies.ToList();
                BiographyModelsView = new ObservableCollection<BiographyModel>(result);
            }
        }
    }
}
