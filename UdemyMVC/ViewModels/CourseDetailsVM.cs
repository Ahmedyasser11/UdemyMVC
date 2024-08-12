using UdemyMVC.Models;

namespace UdemyMVC.ViewModels
{
    public class CourseDetailsVM
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string CourseImg { get; set; }
        public string Instructor { get; set; }
        public List<string> Category { get; set; }
        public int NumberOfStudent { get; set; }
        public string Rating { get; set; }
        public List<Chapter> chapters { get; set; }

    }
}
