using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMVC.Models
{
    public class Department
    {

        public int Id { get; set; }
        public string Name { get; set; }

        // Um departamento possuí varios vendedores. Já instanciando
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        // Construtor vazio
        public Department() {}

        // Construtor com argumentos. Todos os atríbutos, exceto o que for coleção
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Métodos

        // Adicionar vendedor no departamento
        public void AddSeller(Seller seller)
        {
            // Adicionando na lista de vendedores do departamento
            Sellers.Add(seller);
        }

        // Total de vendas do departamento, no intervalo de datas
        public double TotalSales(DateTime initial, DateTime final)
        {
            // Total de vendas do vendador em tal data;
            // E soma é feita para o resultado de todos os vendedores do departamento (Sellers)
            return Sellers
                .Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
