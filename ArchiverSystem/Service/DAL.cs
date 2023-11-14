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
using System.Windows.Media.Imaging;
using System.Windows;
using System.Security.Policy;

namespace ArchiverSystem.Service
{
    public class DAL : IDisposable
    {
        private string _conStr;//= ConfigurationManager.ConnectionStrings["ArchiverConnnectionString"].ConnectionString;
        private SqlConnection _con;

        public DAL(string dbPathAPI = "") 
        {
            string dbPath;
            if (String.IsNullOrEmpty(dbPathAPI))
                dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Archiver.mdf");
            else
                dbPath = dbPathAPI;
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
                album.InputDate = DateTime.Now;
                album.UpdateDate = DateTime.Now;
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
                Album album = await _con.QueryFirstAsync<Album>(sql, new { Id = id });
                return album;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Update Album 
        public async Task<bool> UpdateAlbumAsync(Album album)
        {
            try
            {
                album.UpdateDate = DateTime.Now;
                string sql = "update Album set Name = @Name, Description = @Description, UpdateDate = @UpdateDate" +
                    " where Id=@id";
                int affectedRows = await _con.ExecuteAsync(sql, album);
                return affectedRows > 0;
            }catch(Exception ex)
            { 
                Console.WriteLine(ex.Message); 
            }
            return false;
        }

        //Delete Album by Id
        public async Task<bool> DeleteAlbumByIdAsync(int id)
        {
            try
            {
                string sql1 = "delete from Item where AlbumId = @id"; 
                string sql2 = "delete from Album where Id = @id";
                await _con.ExecuteAsync(sql1, new { Id = id });
                int affectedRows = await _con.ExecuteAsync(sql2, new { Id = id });
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //Album exists
        public async Task<bool> AlbumExists(int id)
        {
            try
            {
                string sql = "select count(*) from Album where Id = @id";
                int rows = await _con.ExecuteScalarAsync<int>(sql, new { Id = id });
                if (rows == 1)
                    return true;
            }
            catch (Exception ex)
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
                item.InputDate = DateTime.Now;
                item.UpdateDate = DateTime.Now;
                string sql = "insert into Item values (@AlbumId, @Name, @Description, @Qty, @InputDate," +
                " @UpdateDate, @Image)";
                int rowsAffected = await _con.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //Get All Items
        public async Task<List<Item>> SelectItemsAsync()
        {
            try
            {
                string sql = "select * from Item";
                var albums = await _con.QueryAsync<Item>(sql);
                return albums.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Get All Album Items
        public async Task<List<Item>> SelectAlbumItemsAsync(int albumId)
        {
            try
            {
                string sql = "select * from Item where AlbumId = @AlbumId";
                var albums = await _con.QueryAsync<Item>(sql, new { AlbumId = albumId }); ;
                return albums.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Get Item by Id
        public async Task<Item> SelectItemByIdAsync(int id)
        {
            try
            {
                string sql = "select * from Item where Id = @Id";
                Item item = await _con.QueryFirstAsync<Item>(sql, new { Id = id });
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        //Update Item 
        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                item.UpdateDate = DateTime.Now;
                string sql = "update Item set AlbumId = @AlbumId, Name = @Name, Description = @Description, Qty = @Qty, UpdateDate = @UpdateDate," +
                    " Image = @Image where Id=@id";
                int affectedRows = await _con.ExecuteAsync(sql, item);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //Delete Item by Id
        public async Task<bool> DeleteItemByIdAsync(int id)
        {
            try
            {
                string sql = "delete from Item where id = @id"; 
                int affectedRows = await _con.ExecuteAsync(sql, new { Id = id });
             
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //Item exists
        public async Task<bool> ItemExists(int id)
        {
            try
            {
                string sql = "select count(*) from Item where Id = @id";
                int rows = await _con.ExecuteScalarAsync<int>(sql, new { Id = id });
                if (rows == 1)
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}