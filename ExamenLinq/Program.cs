using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ExamenLinq
{
    class Program
    {
        static void Main(string[] args)
        {

            var ingredientes = new List<Ingrediente>()
            {
                new Ingrediente() { Id=1, Name="Queso", Cost=1 },
                new Ingrediente() { Id=2, Name="Tomate", Cost=2.3M},
                new Ingrediente() { Id=3, Name="Nata", Cost=1.5M}
            };

            var pizzas = new List<Pizza>()
            {
                new Pizza() {Id=1, Name="Margarita", ingrediente = new List<Ingrediente>()
                    {
                        ingredientes [0],
                        ingredientes [1],
                    }
                },

                new Pizza() {Id=2, Name="Carbonara", ingrediente = new List<Ingrediente>()
                    {
                        ingredientes [0],
                        ingredientes [1],
                        ingredientes [2],
                    }
                },
                new Pizza() { Id=3, Name="Sin Ingredientes", ingrediente = new List<Ingrediente>()
                    {
                        
                    }
                    
                }
            };
            var precio =
                from p in pizzas
                from i in ingredientes
                group i by new { p.Id, p.Name, i.Cost } into n
                select new
                {
                    Id = n.Key.Id,
                    Nombre = n.Key.Name,
                    Precio = n.Sum(i => i.Cost)*1.2M
                };
            var noingredientes = from p in pizzas
                from i in p.ingrediente.DefaultIfEmpty()
                select new
                {
                    Id = p.Id,
                    Nombre = ingredientes == null ? p.Name +
                    "(No ingredientes)" : p.Name
                };


               
        }
        public class Ingrediente
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Decimal Cost { get; set; }
        }
        public class Pizza
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<Ingrediente> ingrediente = new List<Ingrediente>();

            public decimal Suma()
            {
                Decimal Total = 0;
                foreach (var Actual in ingrediente)
                {
                    Total = Actual.Cost + Total;

                    Console.WriteLine(Total);
                }
                return Total * 1.2M;
            }
        }
    }
}