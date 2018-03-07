using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccess
{
    public abstract class CommonDB
    {
        protected readonly string connectionString;
       
        public CommonDB(string connectionString)
        {
            this.connectionString = connectionString;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <remarks>
        /// Was changed from the original string input to an sql command because else sql injection is possible
        /// </remarks>
        /// <returns></returns>
        protected bool ExecuteNonQuery(SqlCommand command)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (command)
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="type"></param>
        /// <remarks>
        /// Was changed from the original string input to an sql command because else sql injection is possible
        /// </remarks>
        /// <returns></returns>
        protected DataSet ExecuteQuery(SqlCommand command,QueryType type)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (command)
            {
                command.Connection = connection;
                switch (type)
                {
                    case QueryType.StoredProcedure:
                        command.CommandType = CommandType.StoredProcedure;
                        break;
                    case QueryType.Query:
                        break;
                    default:
                        break;
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet set = new DataSet();
                adapter.Fill(set);
                return set;
            }

        }
    }
}
