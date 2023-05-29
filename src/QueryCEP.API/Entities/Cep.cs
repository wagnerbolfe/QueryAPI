using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryCEP.API.Entities
{
    public class Cep
    {
        public string CepNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Ibge { get; set; } = string.Empty;
        public string Gia { get; set; } = string.Empty;
        public string DDD { get; set; } = string.Empty;
        public string Siafi { get; set; } = string.Empty;
    }
}