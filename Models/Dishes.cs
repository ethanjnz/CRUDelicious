#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace crudelicious.Models;

public class Dish
{
    [Key]
    public int DishesId { get; set; }

    [Required]
    [DisplayName("Name of Dish")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters!")]
    public string Name { get; set; }

    [Required]
    [DisplayName("Chef's Name")]
    [MinLength(2, ErrorMessage = "Chef must be at least 2 characters!")]
    public string Chef { get; set; }

    [Required]
    [Range(1, 6)]
    public int Tastiness { get; set; }

    [Required]
    [DisplayName("# of Calories")]
    [Range(0, int.MaxValue)]
    public int Calories { get; set; }

    [Required]
    [MinLength(20, ErrorMessage = "Description must be at least 20 characters!")]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdateAt { get; set; } = DateTime.Now;

}