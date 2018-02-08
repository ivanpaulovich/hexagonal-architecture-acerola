using Acerola.Application.DTO;
using Acerola.Application.Mappers;
using Acerola.Domain.Customers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acerola.Infrastructure.AutoMapper
{
    public class CustomersMapper : ICustomersMapper
    {
        public CustomerData Map(Customer customer)
        {
            CustomerData data = Mapper.Map<CustomerData>(customer);
            return data;
        }
    }
}
