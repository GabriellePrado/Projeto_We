using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using We._Project.Model;

namespace We._Project.Repository.Interface
{
    public interface IColaboradorRepository
    {
        public IEnumerable<Colaborador> GetAll();

        public Task<string> CreateAsync(Colaborador colaborador);
        public Task<Colaborador> UpdateAsync(Colaborador colaborador);

        public Task DeleteAsync(string cpf);

        public List<Colaborador> GetByCpfAsync(string cpf);

        //public Task<Colaborador> ExistsByCpfAsync(string colaboradorCpf);

        //public Task<List<Colaborador>> ListByFilterAsync(string colaboradorCpf = null, string name = null);

    }
}
