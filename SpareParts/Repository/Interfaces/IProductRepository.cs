using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Repository.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetById(int? id);
        Task<Product> GetByIdAsync(int? id);

        bool Exists(int id);

        IEnumerable<Product> GetAll();
        Task<IEnumerable<Product>> GetAllAsync();

        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);

        void SaveChanges();
        Task SaveChangesAsync();

    }
}