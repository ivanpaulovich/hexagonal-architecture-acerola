namespace Acerola.Application.Presenters
{
    using Acerola.Application.DTO;
    using Acerola.Domain.Customers;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IRegisterPresenter
    {
        CustomerData Handle(Customer customer);
    }
}
