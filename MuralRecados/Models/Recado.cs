using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuralRecados.Models
{
    public class Recado
    {
        public int RecadoId { get; set; }
        public string Texto { get; set; }
        public int UsuarioCadastro { get; set; }
        public string ApelidoUsuario { get; set; }
    }
}
