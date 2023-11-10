using ArchiverSystem.Model;
using ArchiverSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchiverAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private DAL db;

        public AlbumController()
        {
            //"F:\\Software\\Development\\_Private Repos\\Archiver\\ArchiverSystem\\ArchiverSystem\\bin\\Debug\\Archiver.mdf"
            string dbPath = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = Path.GetFullPath(Path.Combine(dbPath, @"..\..\..\..\"));
            dbPath = Path.Combine(dbPath, "ArchiverSystem\\bin\\Debug\\Archiver.mdf");

            db = new DAL(dbPath);
        }

        //https://localhost:7155/Album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAllAlbums()
        {
            List<Album> albums = await db.SelectAlbumsAsync();
            return albums;
        }

        //https://localhost:7155/Album/id?id=15
        [HttpGet("{id:int}", Name = "GetAlbum")]
        public async Task<ActionResult<Album>> GetAlbumById(int id)
        {
            Album album = await db.SelectAlbumByIdAsync(id);
            return album;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAlbum([FromBody] Album newAlbum)
        {
            if (newAlbum == null)
                return BadRequest();

            if (await db.InsertAlbumAsync(newAlbum))
                return Ok();
            return BadRequest();
        }

        //https://localhost:7155/Album/21
        [HttpDelete("{id:int}", Name = "DeleteAlbum")]
        public async Task<ActionResult> DeleteAlbum(int id)
        {
            if (!(id > 0))
                return NotFound();

            if (await db.DeleteAlbumByIdAsync(id))
                return Ok();
            return BadRequest();
        }
    }
}
