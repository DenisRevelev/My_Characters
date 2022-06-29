using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    // Зависимая сущность от BiographyModel. Связь один ко одному. 
    public class ProgressModel
    {
        public int Id { get; set; }
        public int Progress { get; set; }
        public bool StatusProgress { get; set; }

        // Внешний ключ.
        public int BiographyId { get; set; }
        // Навигационное свойство. 
        public BiographyModel Biography { get; set; }
    }
}
