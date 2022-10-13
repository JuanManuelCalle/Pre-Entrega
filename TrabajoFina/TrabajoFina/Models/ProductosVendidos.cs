using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoFina.Models
{
    public class ProductosVendidos
    {
        public int Id { get; set; }
        public int IdUsuarioVenta { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

        public ProductosVendidos()
        {
            Id = 0;
            IdUsuarioVenta = 0;
            IdProducto = 0;
            IdVenta = 0;
        }
    }
}
