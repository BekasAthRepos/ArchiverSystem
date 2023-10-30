using ArchiverSystem.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Net;
using System.Configuration;
using System.IO;
using System.Windows.Markup;
using System.Data;
using System.Windows.Media.Animation;

namespace ArchiverSystem.Service
{
    public class DAL : IDisposable
    {
        private string _conStr;//= ConfigurationManager.ConnectionStrings["ArchiverConnnectionString"].ConnectionString;
        private SqlConnection _con;

        public DAL() 
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Archiver.mdf");
            _conStr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + dbPath + "; Integrated Security = True";
            _con = new SqlConnection(_conStr);
            _con.Open();
        }

        public void Dispose()
        {
            if(_con != null && _con.State != ConnectionState.Closed)
            {
                _con.Close();
                _con.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        // -- Album functions --

        //Insert Album
        public async Task<bool> InsertAlbumAsync(Album album)
        {
            try
            {
                string sql = "insert into Album values (@Name, @Description, @InputDate," +
                " @UpdateDate)";
                int rowsAffected = await _con.ExecuteAsync(sql, album);
                return rowsAffected > 0;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }  
            return false;
        }

        //Get All Ablums
        public async Task<List<Album>> SelectAlbumsAsync()
        {            
            try
            {
                string sql = "select * from Album";
                var albums = await _con.QueryAsync<Album>(sql);
                return albums.ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Get Ablum by Id
        public async Task<Album> SelectAlbumByIdAsync(int id)
        {
            try
            {
                string sql = "select * from Album where Id = @Id";
                Album album = await _con.QueryFirstAsync<Album>(sql, new { Id = id});
                return album;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Delete Album by Id
        public async Task<bool> DeleteAlbumByIdAsync(int id)
        {
            try
            {
                //string sql = "delete from Item where "; 
               // sql = "delete from Album where Id = @id";
                //int affectedRows = await _con.ExecuteAsync(sql, new { Id = id });
                //if
                //return affectedRows > 0;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        // -- Item functions --

        //Insert Item
        public async Task<bool> InsertItemAsync(Item item)
        {
            try
            {
                string sql = "insert into Item values (@Name, @Description, @InputDate," +
                " @UpdateDate)";
                int rowsAffected = await _con.ExecuteAsync(sql, album);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //Get All Ablums
        public async Task<List<Album>> SelectAlbumsAsync()
        {
            try
            {
                string sql = "select * from Album";
                var albums = await _con.QueryAsync<Album>(sql);
                return albums.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Get Ablum by Id
        public async Task<Album> SelectAlbumByIdAsync(int id)
        {
            try
            {
                string sql = "select * from Album where Id = @Id";
                Album album = await _con.QueryFirstAsync<Album>(sql, new { Id = id });
                return album;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Delete Album by Id
        public async Task<bool> DeleteAlbumByIdAsync(int id)
        {
            try
            {
                //string sql = "delete from Item where "; 
                // sql = "delete from Album where Id = @id";
                //int affectedRows = await _con.ExecuteAsync(sql, new { Id = id });
                //if
                //return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
