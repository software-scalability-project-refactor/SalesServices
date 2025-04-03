using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesService.Entities;

public class Sale
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid IdSale { get; set; }

    [MaxLength(100)] 
    public string? ClientName { get; set; } = null;
    
    [MaxLength(20)]
    public string? ClientPhone { get; set; } = null;

    [Column(TypeName = "decimal(18,2)")]
    public double Discount { get; set; } = 0;

    public bool IsCash { get; set; } = true;
    public bool IsDelivery { get; set; } = false;
    public bool IsPerMajor { get; set; } = false;
    public bool IsReservation { get; set; } =  false;

    public string Products { get; set; } = "[]";

    public DateTime? ReservationDate { get; set; } = null;
    
    [Required]
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; } = 0;
    
    [NotMapped]
    public List<string> ProductIds
    {
        get => System.Text.Json.JsonSerializer.Deserialize<List<string>>(Products ?? "[]") ?? new List<string>();
        set => Products = System.Text.Json.JsonSerializer.Serialize(value ?? new List<string>());
    }
}  