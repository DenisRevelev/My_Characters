using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    // Зависимая сущность от BiographyModel. Связь один ко одному. 
    // Главная сущность для ToDoListModel.
    public class ProgressModel
    {
        public int Id { get; set; }
        public int Progress { get; set; }
        public bool StatusProgress { get; set; }

        // Навигационное свойство зависимой сущности ToDoListModel.
        public ICollection<ToDoListModel>? ToDoListNavigation { get; set; }

        // Внешний ключ.
        public int BiographyId { get; set; }
        // Навигационное свойство. 
        public BiographyModel Biography { get; set; }
    }
}
