using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyMVC.Models
{
    public class Topic
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("Chapter")]
        public int ChapterID { get; set; }
        [ForeignKey("ChapterID")]
        public virtual  Chapter Chapter { get; set; } 

    }
}
