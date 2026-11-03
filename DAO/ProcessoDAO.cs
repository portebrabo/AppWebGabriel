using System.Data;
using AppWebGabriel.Model;

namespace AppWebGabriel.DAO;

public class ProcessoDAO
{
    private readonly ConexaoDataAccess _conexao = new();

    public List<Processo> Listar()
    {
        var lista = new List<Processo>();
        using var con = _conexao.GetConnection();
        const string sql = @"SELECT id_pro, numero_pro, data_pro, interessado_pro, assunto_pro, descricao_pro, situacao_pro FROM processos";
        using var comando = con.CreateCommand();
        comando.CommandText = sql;
        using var leitor = comando.ExecuteReader();
        while (leitor.Read())
        {
            lista.Add(new Processo
            {
                Id = leitor.GetInt32(leitor.GetOrdinal("id_pro")),
                Numero = leitor.GetString(leitor.GetOrdinal("numero_pro")),
                Data = DateOnly.FromDateTime(leitor.GetDateTime(leitor.GetOrdinal("data_pro"))),
                Interessado = leitor.GetString(leitor.GetOrdinal("interessado_pro")),
                Assunto = leitor.GetString(leitor.GetOrdinal("assunto_pro")),
                Descricao = DAOHelper.GetString(leitor, "descricao_pro"),
                Situacao = leitor.GetString(leitor.GetOrdinal("situacao_pro"))
            });
        }
        return lista;
    }

    public Processo? ObterPorId(int id)
    {
        using var con = _conexao.GetConnection();
        const string sql = @"SELECT id_pro, numero_pro, data_pro, interessado_pro, assunto_pro, descricao_pro, situacao_pro FROM processos WHERE id_pro = @id";
        using var comando = con.CreateCommand();
        comando.CommandText = sql;

        var param = comando.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        comando.Parameters.Add(param);

        using var leitor = comando.ExecuteReader();
        if (leitor.Read())
        {
            return new Processo
            {
                Id = leitor.GetInt32(leitor.GetOrdinal("id_pro")),
                Numero = leitor.GetString(leitor.GetOrdinal("numero_pro")),
                Data = DateOnly.FromDateTime(leitor.GetDateTime(leitor.GetOrdinal("data_pro"))),
                Interessado = leitor.GetString(leitor.GetOrdinal("interessado_pro")),
                Assunto = leitor.GetString(leitor.GetOrdinal("assunto_pro")),
                Descricao = DAOHelper.GetString(leitor, "descricao_pro"),
                Situacao = leitor.GetString(leitor.GetOrdinal("situacao_pro"))
            };
        }
        return null;
    }

    public void Inserir(Processo processo)
    {
        using var con = _conexao.GetConnection();
        const string sql = @"INSERT INTO processos (numero_pro, data_pro, interessado_pro, assunto_pro, descricao_pro, situacao_pro) VALUES (@numero, @data, @interessado, @assunto, @descricao, @situacao)";
        using var comando = con.CreateCommand();
        comando.CommandText = sql;

        AdicionarParametros(comando, processo);
        comando.ExecuteNonQuery();
    }

    public void Atualizar(Processo processo)
    {
        using var con = _conexao.GetConnection();
        const string sql = @"UPDATE processos SET numero_pro = @numero, data_pro = @data, interessado_pro = @interessado, assunto_pro = @assunto, descricao_pro = @descricao, situacao_pro = @situacao WHERE id_pro = @id";
        using var comando = con.CreateCommand();
        comando.CommandText = sql;

        AdicionarParametros(comando, processo);
        var paramId = comando.CreateParameter();
        paramId.ParameterName = "@id";
        paramId.Value = processo.Id;
        comando.Parameters.Add(paramId);

        comando.ExecuteNonQuery();
    }

    private void AdicionarParametros(IDbCommand comando, Processo processo)
    {
        var pNumero = comando.CreateParameter();
        pNumero.ParameterName = "@numero";
        pNumero.Value = processo.Numero ?? (object)DBNull.Value;
        comando.Parameters.Add(pNumero);

        var pData = comando.CreateParameter();
        pData.ParameterName = "@data";
        pData.Value = processo.Data.HasValue ? processo.Data.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value;
        comando.Parameters.Add(pData);

        var pInteressado = comando.CreateParameter();
        pInteressado.ParameterName = "@interessado";
        pInteressado.Value = processo.Interessado ?? (object)DBNull.Value;
        comando.Parameters.Add(pInteressado);

        var pAssunto = comando.CreateParameter();
        pAssunto.ParameterName = "@assunto";
        pAssunto.Value = processo.Assunto ?? (object)DBNull.Value;
        comando.Parameters.Add(pAssunto);

        var pDescricao = comando.CreateParameter();
        pDescricao.ParameterName = "@descricao";
        pDescricao.Value = processo.Descricao ?? (object)DBNull.Value;
        comando.Parameters.Add(pDescricao);

        var pSituacao = comando.CreateParameter();
        pSituacao.ParameterName = "@situacao";
        pSituacao.Value = processo.Situacao ?? (object)DBNull.Value;
        comando.Parameters.Add(pSituacao);
    }
}