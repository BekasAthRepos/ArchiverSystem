using ArchiverSystem.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ArchiverSystem.Service
{
    public class DAL
    {
        private string _conStr = Properties.Settings.Default.ArchiverDBConnectionString;
        private SqlConnection Con;

        public DAL() 
        {
            Con = new SqlConnection(_conStr);
        }

        public async Task<bool> InsertAlbumAsync(Album album)
        {
            using (var con = new SqlConnection(_conStr))
            {
                try
                {

                    var sql = "insert into Album values (@Name, @Description, @InputDate," +
                    " @UpdateDate)";
                    int rowsAffected = await con.ExecuteAsync(sql, album);
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }
    }
}
