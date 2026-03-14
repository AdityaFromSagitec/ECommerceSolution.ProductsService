using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.DataAccessLayer.Entites;

public class Product
{
    [Key]
    public Guid ProductID { get; set; }
    public string? ProductName { get; set; }
    public string? Category { get; set; }
    public double? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }
}
