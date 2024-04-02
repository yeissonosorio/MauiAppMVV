using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppMVVM.Models
{
    [Table("Productos")]
    public class Productsmodel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? nombre { get; set; }
        public double Precio { get; set; }
        public string? foto { get; set; }
    }
}
