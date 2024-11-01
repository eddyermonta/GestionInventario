using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionInventario.src.Modules.Users.Domains.DTOS
{
    public class AddressGetElementDto
        {
        public int ZipCode { get; set; }
        public string? Street { get; set; }
        public string? State { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }
    }
}