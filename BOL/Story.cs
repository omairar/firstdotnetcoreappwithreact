
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    [Table("Story")]
    public class Story
    {
        [Key]
        public int SSid { get; set; }

        [Required]
        public string? SSTitle { get; set; }
        [Required]
        public string? SSDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsApproved { get; set; }

        public int Like { get; set; }
        public int Dislike { get; set; }

        [ForeignKey("UserNav")]

        public string? Id { get; set; }
        public SSUser? UserNav { get; set; }

        [ForeignKey("CategoryNav")]
        public int? CategoryId { get; set; }

        public Category? CategoryNav { get; set; }

    }
}
