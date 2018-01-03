namespace AplicativoWeb.Models
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class ContextoBanco : DbContext
    {

        public ContextoBanco()
            : base("name=ContextoBanco")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pessoa> Pessoa { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ItemPedido> ItensPedidos { get; set; }




        //public IEnumerable Pessoas { get; internal set; }
        //public object ItensPedidos { get; internal set; }
    }

}