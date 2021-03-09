using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);

    }
}