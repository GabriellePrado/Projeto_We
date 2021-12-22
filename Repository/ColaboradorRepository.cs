using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using We._Project.ConnectionFactory.Interface;
using We._Project.Model;
using We._Project.Repository.Interface;

namespace We._Project.Repository
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly IDBConnector _dbConnector;
        
        public ColaboradorRepository(IDBConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        const string baseSql = @"SELECT [cpf]
                                   ,[matricula]
                                   ,[nome_completo]
                                   ,[data_admissao]
                                   ,[status_contrato]
                                   ,[departamento_colaborador]
                                  FROM [dbo].[Colaborador]
                                  WHERE 1 = 1 ";

        public IEnumerable<Colaborador> GetAll()
        {
            try
            {
                const string sql = @"SELECT  C.cpf, C.matricula, C.nome_completo, C.data_admissao, 
                                    C.status_contrato, C.departamento_colaborador, D.cargo 
                        FROM dbo.colaborador C 
                        INNER JOIN dbo.departamento D ON D.id = C.departamento_colaborador
";

                return _dbConnector.dbConnection.Query<Colaborador>(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar" + e.Message);
            }
        }

        public async Task<string> CreateAsync(Colaborador colaborador)
        {
            string retorno = "Usuario inserido com sucesso";
            try
            {
                //validar se matricula e cpf ja existe na base
                if (GetByCpfAsync(colaborador.Cpf) != null || GetByMatriculaAsync(colaborador.Matricula) != null)
                {
                    return "Cpf já existente";
                }

                _dbConnector.dbConnection.Open();
                string sql = @"INSERT INTO [dbo].[Colaborador]
                                    ([cpf]
                                    ,[matricula]
                                    ,[nome_completo]
                                    ,[data_admissao]
                                    ,[status_contrato]
                                    ,[departamento_colaborador]
                                    )
                              VALUES
                                    (@Cpf
                                    ,@Matricula
                                    ,@Nome_completo
                                    ,@Data_admissao
                                    ,@Status_Contrato
                                    ,@Departamento_colaborador)";

                await _dbConnector.dbConnection.ExecuteAsync(sql, new
                {
                    Cpf = colaborador.Cpf,
                    Matricula = colaborador.Matricula,
                    Nome_completo = colaborador.Nome_completo,
                    Data_admissao = colaborador.Data_admissao,
                    Status_contrato = colaborador.Status_contrato,
                    Departamento_colaborador = colaborador.Departamento_colaborador
                });


                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar" + e.Message);
            }
            finally
            {
                _dbConnector.dbConnection.Close();
            }

        }
        public async Task<Colaborador> UpdateAsync(Colaborador colaborador)
        {
            try
            {
                //se colaborador ja esta na base e esta desligado, reativar o 
                if (GetByCpfAsync(colaborador.Cpf) != null || GetByMatriculaAsync(colaborador.Matricula) != null)
                {
                    _dbConnector.dbConnection.Open();
                    string sql = @"UPDATE [dbo].[Colaborador]
                                  SET [cpf] = @CPF
                                    ,[matricula] = @Matricula
                                    ,[nome_completo] = @Nome_completo
                                    ,[data_admissao] = @Data_admissao
                                    ,[status_contrato] = @Status_contrato
                                    ,[departamento_colaborador] = @Departamento_Colaborador
                                   
                                WHERE Cpf = @Cpf";

                    await _dbConnector.dbConnection.ExecuteAsync(sql, new
                    {
                        Cpf = colaborador.Cpf,
                        Matricula = colaborador.Matricula,
                        Nome_completo = colaborador.Nome_completo,
                        Data_admissao = colaborador.Data_admissao,
                        Status_contrato = colaborador.Status_contrato,
                        Departamento_colaborador = colaborador.Departamento_colaborador
                    });

                    var colaboradorAtualizado = GetByCpfAsync(colaborador.Cpf);
                    return colaborador;
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar" + e.Message);
            }
            finally
            {
                _dbConnector.dbConnection.Close();
            }
        }

        public async Task DeleteAsync(string cpf)
        {
            string sql = string.Empty;
            try
            {
                sql = $"DELETE FROM [dbo].[Colaborador] WHERE cpf = @Cpf";
                await _dbConnector.dbConnection.ExecuteAsync(sql, new { Cpf = cpf });

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar" + e.Message);
            }
        }

        public List<Colaborador> GetByCpfAsync(string cpf)
        {
            try
            {

                var sql = $"SELECT * FROM [dbo].[Colaborador] WHERE cpf = @Cpf";
                var colaborador = _dbConnector.dbConnection.Query<Colaborador>(sql, new { cpf = cpf }).ToList();
                if (colaborador != null)
                {
                    return colaborador;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Erro: " + e.Message);
            }

        }
        public List<Colaborador> GetByMatriculaAsync(int matricula)
        {
            try
            {
                var sql = $"SELECT * FROM [dbo].[Colaborador] WHERE matricula = @Matricula";
                var colaborador = _dbConnector.dbConnection.Query<Colaborador>(sql, new { matricula = matricula }).ToList();
                if (colaborador != null)
                {
                    return colaborador;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Erro: " + e.Message);
            }


        }
        public async Task<Colaborador> ExistsByCpfAsync(string cpf)
        {
            try
            {
                string sql = $"{baseSql} AND cpf = @Cpf";

                var colaboradors = await _dbConnector.dbConnection.QueryAsync<Colaborador>(sql, new { Cpf = cpf });

                return colaboradors.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar colaborador " + e.Message);
            }
        }



        //public async Task<List<Colaborador>> ListByFilterAsync(string colaboradorCpf = null, string name = null)
        //{
        //    string sql = $"{baseSql} ";

        //    if (!string.IsNullOrWhiteSpace(colaboradorCpf))
        //        sql += "AND Cpf = @cpf";

        //    if (!string.IsNullOrWhiteSpace(name))
        //        sql += "AND Nome_completo like @nome_completo";

        //    var colaboradors = await _dbConnector.dbConnection.QueryAsync<Colaborador>(sql, new { Cpf = colaboradorCpf, Nome_completo = "%" + name + "%" });

        //    return colaboradors.ToList();
        //}
    }
}
