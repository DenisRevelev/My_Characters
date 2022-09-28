using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    public class SourceFileModel
    {
        // Зависимая сущность от BiographyModel. Связь один ко многим.
        public int Id { get; set; }
        public string? Path { get; set; }
        public string? Name { get; set; }

        // Внешний ключ.
        public int BiographyId { get; set; }
        // Навигационное свойство. 
        public BiographyModel Biography { get; set; }
    }
}
