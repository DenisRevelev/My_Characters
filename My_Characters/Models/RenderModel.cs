using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    public class RenderModel
    {
        // Зависимая сущность от BiographyModel. Связь один ко многим. 
        public int Id { get; set; }
        public byte[]? RenderImage { get; set; }

        // Внешний ключ.
        public int BiographyId { get; set; }
        // Наивгационное свойство. 
        public BiographyModel Biography { get; set; }
    }
}
