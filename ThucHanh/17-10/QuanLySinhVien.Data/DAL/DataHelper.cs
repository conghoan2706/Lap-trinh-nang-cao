using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLySinhVien.Data.DAL
{
    public class DataHelper
    {
        private readonly SqlConnection con;

        public DataHelper()
        {
            string connectionString =
                @"Server=LAPTOP-6SN65NCS\HUECTMDB;Database=QLSV;Integrated Security=True;TrustServerCertificate=True";
            con = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        public void ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();
                using SqlCommand command = new SqlCommand(sql, con);
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataReader ExcuteReader(string sql, params SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(sql, con);
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using SqlDataAdapter da = new SqlDataAdapter(sql, con);
                if (parameters != null && parameters.Length > 0 && da.SelectCommand != null)
                    da.SelectCommand.Parameters.AddRange(parameters);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
