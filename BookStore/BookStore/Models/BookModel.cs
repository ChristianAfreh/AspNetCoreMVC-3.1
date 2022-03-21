using Microsoft.AspNetCore.Http;
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
        
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string Category { get; set; }
        [Display(Name ="Language")]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Display(Name = "Total Pages")]
        [Required(ErrorMessage ="Enter the total pages of book")]
        public int? TotalPages { get; set; }
        
        [Display(Name ="Choose the cover photo for your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverPhotoUrl { get; set; }



    }
}
