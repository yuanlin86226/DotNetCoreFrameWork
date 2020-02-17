using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Context;
using Microsoft.EntityFrameworkCore;

namespace Utils
{
    public class DatabaseReadUtils
    {
        CustomContext _context;

        DbConnection connection;
        public DatabaseReadUtils(CustomContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<DbDataReader> DatabaseRead(string sql, string start_date, string end_date, string product_type, string order_id, string member_number, string product_name)
        {
            this.connection = _context.Database.GetDbConnection();
            await this.connection.OpenAsync();
            var command = this.connection.CreateCommand();
            command.CommandText = sql;

            if (!string.IsNullOrEmpty(start_date))
                command.Parameters.Add(new SqlParameter("@start", start_date));

            if (!string.IsNullOrEmpty(end_date))
            {
                end_date = DateTime.Parse(end_date).AddDays(1).ToString("yyyy-MM-dd");
                command.Parameters.Add(new SqlParameter("@end", end_date));
            }


            if (!string.IsNullOrEmpty(product_type))
                command.Parameters.Add(new SqlParameter("@type", product_type));

            if (!string.IsNullOrEmpty(order_id))
                command.Parameters.Add(new SqlParameter("@order_id", order_id));

            if (!string.IsNullOrEmpty(member_number))
                command.Parameters.Add(new SqlParameter("@member", member_number));

            if (!string.IsNullOrEmpty(product_name))
                command.Parameters.Add(new SqlParameter("@product", product_name));

            return await command.ExecuteReaderAsync();
        }

        public void DatabaseClose()
        {
            this.connection.Close();
        }
    }
}