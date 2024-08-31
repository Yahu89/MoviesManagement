using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MoviesManagementApi.Database;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Title musn\'t be an empty")]
    public string Title { get; set; }
    public string? Director { get; set; }

    [Range(1900, 2200, ErrorMessage = "Production year must be between 1900 and 2200")]
    public int? Year { get; set; }
    public int? Rate { get; set; }
}
