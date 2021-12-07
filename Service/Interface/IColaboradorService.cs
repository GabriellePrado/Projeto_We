using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using We._Project.Model;

namespace We._Project.Service.Interface
{
    public interface IColaboradorService
    {
        public IEnumerable<Colaborador> GetAll();

        public  Task<string> CreateAsync(Colaborador colaborador);

        public Task<Colaborador> UpdateAsync(Colaborador colaborador);

        public Task DeleteAsync(string cpf);

        public List<Colaborador> GetByCpfAsync(string cpf);
    }
}
