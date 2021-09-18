using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalRadiation.Models.Entities
{
    public class NewsItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgSource { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime PublishDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}