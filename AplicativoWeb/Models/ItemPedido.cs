using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicativoWeb.Models
{
    public class ItemPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Produto")]
        public Guid ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }

        public Guid PedidoId { get; set; }

        public virtual Pedido Pedido { get; set; }

        public Double Quantidade { get; set; }

        [Display(Name = "Preço Unitário")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public Double PrecoUnitario { get; set; }

        [Display(Name = "% Desconto")]
        public Double PercentualDesconto { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public Double Total { get; set; }
    }
}