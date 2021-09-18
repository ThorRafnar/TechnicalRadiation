using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalRadiation.Models.Entities
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfileImgSource { get; set; }
        public string Bio { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<NewsItem> NewsItems { get; set; }
    }
}