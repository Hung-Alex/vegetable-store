﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public string Password { get; set; }
        public string Role { get; set; } 
        
    }
}
