using Microsoft.Extensions.Configuration;
using MuralRecados.Models;
using MuralRecados.Scripts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace MuralRecados.Repository
{

    public interface IMuralRecadosRepository {
        Task<IEnumerable<Recado>> GetRecados();
        Task<IEnumerable<int>> PutRecado(Recado recado);
        Task<IEnumerable<int>> PutUsuario(Usuario usuario);
        Task<IEnumerable<int>> DeleteRecado(int recadoId);
    }

    public class MuralRecadosRepository : IMuralRecadosRepository
    {
        private IConfiguration _config;

        public MuralRecadosRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<IEnumerable<int>> DeleteRecado(int recadoId)
        {
            var param = new
            {
                RECADO_ID = recadoId,
            };

            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var sql = MuralRecadosScript.RemoverRecado();

                return await conexao.QueryAsync<int>(sql, param);
            }
        }

        public async Task<IEnumerable<Recado>> GetRecados()
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var sql = MuralRecadosScript.ObterTodosRecados();

                return await conexao.QueryAsync<Recado>(sql);
            }
        }

        public async Task<IEnumerable<int>> PutRecado(Recado recado)
        {
            var param = new
            {
                TEXTO = recado.Texto,
                USUARIO_ID = recado.UsuarioCadastro,
            };
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {



                var sql = MuralRecadosScript.InserirRecado();

                return await conexao.QueryAsync<int>(sql, param);
            }
        }

        public async Task<IEnumerable<int>> PutUsuario(Usuario usuario)
        {
            var param = new
            {
                APELIDO = usuario.Apelido
            };
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var sqlUsuario = MuralRecadosScript.GetUsuario();

                var idUsuario = await conexao.QueryAsync<int>(sqlUsuario, param);

                if(idUsuario == null)
                {
                    var sql = MuralRecadosScript.InserirUsuario();

                    return await conexao.QueryAsync<int>(sql, param);
                } else
                {
                    return idUsuario;
                }

            }
        }
    }
}
