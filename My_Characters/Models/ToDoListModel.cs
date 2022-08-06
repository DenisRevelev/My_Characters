using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    // Зависимая сущность от BiographyModel. Связь один ко многим. 
    public class ToDoListModel
    {
        public int Id { get; set; }
        public string? Task { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Finish { get; set; }
        public bool CheckTask { get; set; }

        // Внешний ключ.
        public int BiographyId { get; set; }
        // Навигационное свойство. 
        public BiographyModel? Biography { get; set; }
    }
}
