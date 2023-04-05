using store_vegetable.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class Order : IEntity
    {
        public int Id { get; set; }
    }
}
