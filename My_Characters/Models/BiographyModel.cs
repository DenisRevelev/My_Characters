using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    // Главная сущность.
    public class BiographyModel
    {
        public int Id { get; set; }
        public byte[]? AvatarImage { get; set; }
        [MaxLength(52)]
        public string? Name { get; set; }
        [MaxLength(52)]
        public string? LastName { get; set;}
        public int Age { get; set; }
        public string? Biography {get; set; }
        public string? Skills {get; set; }

        // Навигационные свойства.
        public ICollection<ToDoListModel>? ProgressNavigation { get; set; }
        public ICollection<ReferenceModel>? ReferenceNavigation { get; set; }
        public ICollection<RenderModel>? RenderNavigation { get; set; }
        public ICollection<SourceFileModel>? SourceFileNavigation { get; set; }
    }
}
