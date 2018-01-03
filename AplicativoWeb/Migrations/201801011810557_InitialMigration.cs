namespace AplicativoWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemPedido",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ProdutoId = c.Guid(nullable: false),
                        PedidoId = c.Guid(nullable: false),
                        Quantidade = c.Double(nullable: false),
                        PrecoUnitario = c.Double(nullable: false),
                        PercentualDesconto = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedido", t => t.PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.PedidoId);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Numero = c.Int(nullable: false, identity: true),
                        PessoaId = c.Guid(nullable: false),
                        Emissao = c.DateTime(nullable: false, precision: 0),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId, cascadeDelete: true)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        DataDeNascimento = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Codigo = c.String(unicode: false),
                        Nome = c.String(unicode: false),
                        PrecoUnitario = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemPedido", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Pedido", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.ItemPedido", "PedidoId", "dbo.Pedido");
            DropIndex("dbo.Pedido", new[] { "PessoaId" });
            DropIndex("dbo.ItemPedido", new[] { "PedidoId" });
            DropIndex("dbo.ItemPedido", new[] { "ProdutoId" });
            DropTable("dbo.Produto");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Pedido");
            DropTable("dbo.ItemPedido");
        }
    }
}
