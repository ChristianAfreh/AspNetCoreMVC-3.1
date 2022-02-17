using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(500,MinimumLength =5)]
        [Required(ErrorMessage="Enter the title of the book") ]
        public string Title { get; set; }
        [StringLength(500, MinimumLength = 5)]
        [Required(ErrorMessage = "Enter name of author")]
        public string Author { get; set; }
        [StringLength(500, MinimumLength = 10)]
        [Required(ErrorMessage = "Enter description of book")]
        public string Description { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        [Display(Name = "Total Pages")]
        [Required(ErrorMessage ="Enter the total pages of book")]
        public int? TotalPages { get; set; }
    }
}
