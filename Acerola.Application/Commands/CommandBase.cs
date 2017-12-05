using MyAccountAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAccountAPI.Producer.Application.Commands
{
    public class CommandBase
    {
        public Header Header { get; set; }
    }
}
