using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrajaComigo.Models;

namespace TrajaComigo.Data
{
    public class TrajaComigoBD : DbContext
    {
        public TrajaComigoBD(DbContextOptions options) : base(options) { }
        //Lista de tabelas da BD:
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Encomendas> Encomendas { get; set; }
        public DbSet<DetalhesEncomendas> DetalhesEncomendas { get; set; }
        public DbSet<EstadoEncomendas> EstadoEncomendas { get; set; }
    }
}
