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

        //GET all albums
        //https://localhost:7155/Album/getAll
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAllAlbums()
        {
            List<Album> albums = await db.SelectAlbumsAsync();
            if(albums.Count > 0)
                return albums;
            return NotFound();
        }

        //GET an album
        //https://localhost:7155/Album/get/1
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<Album>> GetAlbumById([FromRoute]int id)
        {
            if (!(id > 0))
                return BadRequest();
            Album album = await db.SelectAlbumByIdAsync(id);
            if (album != null)
                return album;
            return NotFound();
        }

        //POST an album
        //https://localhost:7155/Album/newAlbum
        [HttpPost("newAlbum")]
        public async Task<ActionResult> CreateAlbum([FromBody] Album newAlbum)
        {
            if (newAlbum == null)
                return BadRequest();

            if (await db.InsertAlbumAsync(newAlbum))
                return Ok();
            return BadRequest();
        }

        //DELETE an album
        //https://localhost:7155/Album/delete?id=1
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAlbum([FromQuery]int id)
        {
            if (!(id > 0))
                return BadRequest();

            if (await db.DeleteAlbumByIdAsync(id))
                return Ok();
            return NotFound();
        }

        //PUT (update) an album
        //https://localhost:7155/Album/update
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAlbum([FromBody] Album album)
        {
            if(!(album.Id > 0))
                return NotFound();
            if(await db.UpdateAlbumAsync(album))
                return Ok();
            return BadRequest();
        }
    }
}
