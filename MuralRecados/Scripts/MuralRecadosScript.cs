using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuralRecados.Scripts
{
    public static class MuralRecadosScript
    {

        public static string ObterTodosRecados()
        {
            return $@" SELECT 
                        R.RECADO_ID RecadoId,
                        R.TEXTO,
                        U.USUARIO_ID UsuarioCadastro,
                        U.APELIDO ApelidoUsuario
                        FROM RECADO R 
                        INNER JOIN USUARIO U 
                            ON U.USUARIO_ID = R.USUARIO_ID";
        }

        public static string InserirRecado()
        {
            return $@"INSERT INTO RECADO(TEXTO, USUARIO_ID) OUTPUT INSERTED.RECADO_ID VALUES(@TEXTO, @USUARIO_ID)";
        }

        public static string InserirUsuario()
        {
            return $@"INSERT INTO USUARIO(APELIDO)  OUTPUT INSERTED.USUARIO_ID VALUES(@APELIDO)";
        }

        public static string RemoverRecado()
        {
            return $@"DELETE RECADO OUTPUT DELETED.RECADO_ID WHERE RECADO_ID = @RECADO_ID ";
        }

        public static string GetUsuario()
        {
            return $@"select top 1 USUARIO_ID FROM USUARIO WHERE APELIDO = @APELIDO";
        }

    }
}
