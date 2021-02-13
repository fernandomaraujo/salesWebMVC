using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        // Atríbutos
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        // 0 = nome do atríbuto (Name), 1 = 100, 2 = MinimumLength (3)
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(2000.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }
        
        // Um vendedor possuí apenas um departamento
        public Department Department { get; set; }

        // Id do departamento, para ser usado como chave estrangeira na tabela de funcionários
        public int DepartmentId { get; set; }

        // Um vendedor pode possuir várias vendas. Já instanciando
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        // Construtor vazio
        public Seller() {}

        // Construtor com argumentos. Todos os atríbutos, exceto o que for coleção
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        // Métodos
        // sr = sales record

        // Adicionar venda do vendedor, na lista de vendas
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        // Removendo venda do vendedor, da lista de vendas
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        // Total de vendas do vendedor, no intervalo de datas
        public double TotalSales(DateTime initial, DateTime final)
        {
            // Utilizando LINQ
            // Sales é a lista de vendas do vendedor

            return Sales
                .Where(sr => sr.Date >= initial && sr.Date <= final)
                .Sum(sr => sr.Amount);
        }
    }
}
