using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Models
{
    // Главная сущность.
    public class BiographyModel
    {
        public int Id { get; set; }

        [MaxLength(16)]
        public string? Name { get; set; }

        [MaxLength(16)]
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Biography { get; set; }
        public string? Skills { get; set; }

        // Навигационные свойства.
        public ProgressModel? ProgressNavigation { get; set; }
        public List<ReferenceModel>? ReferenceNavigation { get; set; }
        public List<RenderModel>? RenderNavigation { get; set; }
        public List<SourceFileModel>? SourceFileNavigation { get; set; }
    }
}
