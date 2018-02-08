using Acerola.Application.DTO;
using Acerola.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acerola.Application.Mappers
{
    public interface ICustomersMapper
    {
        CustomerData Map(Customer customer);
    }
}
