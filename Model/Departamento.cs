using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace We._Project.Model
{
    public class Departamento
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("cargo")]
        public string Cargo { get; set; }
    }
}
