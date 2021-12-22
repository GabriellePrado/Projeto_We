using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using We._Project.Model;
using We._Project.Repository;
using We._Project.Repository.Interface;
using We._Project.Service.Interface;

namespace We._Project.Service
{
    public class ColaboradorService : IColaboradorService
    {
        public IColaboradorRepository _repository;

        public ColaboradorService(IColaboradorRepository colaboradorRepository)
        {
            colaboradorRepository = _repository;
        }
        public IEnumerable<Colaborador> GetAll()
        {
            return _repository.GetAll();
        }
        public async Task<string> CreateAsync(Colaborador colaborador)
        {
            var create = await _repository.CreateAsync(colaborador);
            return create.ToString();
        }

        public async Task<Colaborador> UpdateAsync(Colaborador colaborador)
        {
            var update = await _repository.UpdateAsync(colaborador);
            return update;
        }
        public async Task DeleteAsync(string cpf)
        {
            await _repository.DeleteAsync(cpf);
        }

        public List<Colaborador> GetByCpfAsync(string cpf)
        {
            return _repository.GetByCpfAsync(cpf);
        }
    }
}
