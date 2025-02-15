using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock.Comment
{
    public class UpdateCommentRequestDto
    {

        [Required]
        [MinLength(5, ErrorMessage = "Title must be of min 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot bet greater than 280 characters")]
        public string Title { get; set; } = string.Empty;


        [Required]
        [MinLength(5, ErrorMessage = "Content must be of min 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot bet greater than 280 characters")]
        public string Content { get; set; } = string.Empty;

    }
}