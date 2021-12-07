using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using We._Project.Model.Enums;

namespace We._Project.Model
{
    public class Colaborador
    {
        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("matricula")]
        public int Matricula { get; set; }

        [Column("nome_completo")]
        public string Nome_completo { get; set; }

        [Column("data_admissao")]
        public DateTime Data_admissao { get; set; }

        [Column("status_contrato")]
        public Status_ContratoEnum Status_contrato { get; set; }

        [Column("departamento_colaborador")]
        public int departamento_colaborador { get; set; }
    }
}
