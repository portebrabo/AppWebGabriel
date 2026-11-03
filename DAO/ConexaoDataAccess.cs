using Microsoft.Data.Sqlite;
using System.Data;

namespace AppWebGabriel.DAO;

public class ConexaoDataAccess
{
    private readonly string _conexaoString = "Data Source=processos.db";

    public IDbConnection GetConnection()
    {
        var conexao = new SqliteConnection(_conexaoString);
        conexao.Open();

        // Cria a tabela processos automaticamente se ela não existir
        using var comando = conexao.CreateCommand();
        comando.CommandText = @"
            CREATE TABLE IF NOT EXISTS processos (
                id_pro INTEGER PRIMARY KEY AUTOINCREMENT,
                numero_pro TEXT,
                data_pro TEXT,
                interessado_pro TEXT,
                assunto_pro TEXT,
                descricao_pro TEXT,
                situacao_pro TEXT
            );";
        comando.ExecuteNonQuery();

        return conexao;
    }
}