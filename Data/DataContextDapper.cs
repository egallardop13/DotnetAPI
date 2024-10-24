using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Dapper;


namespace DotnetAPI.Data
{

    class DataContextDapper
    {

        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;

        
    }

        

        public IEnumerable<T> LoadData<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Query<T>(sql);
    }
     public T LoadDataSingle<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.QuerySingle<T>(sql);
    }
    public int ExecuteSqlWithRowCount(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Execute(sql);
    }
    public bool ExecuteSql(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Execute(sql)>0;
    }
     public bool ExecuteSqlWithParameter(string sql, List<SqlParameter> Parameters)
    {
        SqlCommand commandWithParams = new SqlCommand(sql);

        foreach (SqlParameter parameter in Parameters)
        {
            commandWithParams.Parameters.Add(parameter);
        }

        SqlConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        dbConnection.Open();

        commandWithParams.Connection = dbConnection;
        int rowsAffected = commandWithParams.ExecuteNonQuery();
        dbConnection.Close();

        return rowsAffected > 0;
    }
    }
}