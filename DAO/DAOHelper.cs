using System.Data;

namespace AppWebGabriel.DAO;

public static class DAOHelper
{
    public static string? GetString(IDataReader leitor, string coluna)
    {
        int indice = leitor.GetOrdinal(coluna);
        return leitor.IsDBNull(indice) ? null : leitor.GetString(indice);
    }
}