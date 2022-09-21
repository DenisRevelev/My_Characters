using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column(TypeName = "date")]
        public DateTime? Start { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Finish { get; set; }
        public bool CheckTask { get; set; }

        // Внешний ключ.
        public int BiographyId { get; set; }
        // Навигационное свойство. 
        public BiographyModel? Biography { get; set; }
    }
}
