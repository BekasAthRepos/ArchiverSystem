using ArchiverSystem.Model;
using ArchiverSystem.Service;
using Microsoft.AspNetCore.Mvc;


namespace ArchiverAPI.Controllers
{
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private DAL db;

        public ItemController()
        {
            //"F:\\Software\\Development\\_Private Repos\\Archiver\\ArchiverSystem\\ArchiverSystem\\bin\\Debug\\Archiver.mdf"
            string dbPath = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = Path.GetFullPath(Path.Combine(dbPath, @"..\..\..\..\"));
            dbPath = Path.Combine(dbPath, "ArchiverSystem\\bin\\Debug\\Archiver.mdf");

            db = new DAL(dbPath);
        }


        //GET all items
        //https://localhost:7155/Item/getAll
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            List<Item> items = await db.SelectItemsAsync();
            foreach (Item item in items)
            {
                if(item.Image != null)
                    item.ImageB64 = Convert.ToBase64String(item.Image);
            }

            if (items.Count > 0)
                return items;
            return NotFound();
        }

        //GET all items of album
        //https://localhost:7155/Item/getAll/1
        [HttpGet("getAll/{albumId:int}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllAlbumItems([FromRoute] int albumId)
        {
            List<Item> items = await db.SelectAlbumItemsAsync(albumId);
            foreach (Item item in items) 
            { 
                if(item.Image != null)
                    item.ImageB64 = Convert.ToBase64String(item.Image);
            }

            if(await db.AlbumExists(albumId))
            {
                return items;
            }
            return NotFound();
        }

        //GET an item
        //https://localhost:7155/Item/get/1
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<Item>> GetItemById([FromRoute] int id)
        {
            if (!(id > 0))
                return BadRequest();
            Item item = await db.SelectItemByIdAsync(id);
            if (item != null)
            {
                item.ImageB64 = Convert.ToBase64String(item.Image);
                return item;
            }
            return NotFound();
        }

        //POST an Item
        //https://localhost:7155/Item/newItem
        [HttpPost("newItem")]
        public async Task<ActionResult> CreateItem([FromBody] Item newItem)
        {
            if (newItem == null)
                return BadRequest();
            if(!(await db.AlbumExists(newItem.AlbumId)))
                return NotFound("Album not found");
            
            if(newItem.ImageB64 != null)
                newItem.Image = Convert.FromBase64String(newItem.ImageB64);
            int newId = await db.InsertItemAsync(newItem);
            if (newId > 0)
                return Ok(newId);
            return BadRequest();
        }

        //DELETE an item
        //https://localhost:7155/Item/delete?id=1
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteItem([FromQuery] int id)
        {
            if (!(id > 0))
                return BadRequest();

            if (await db.DeleteItemByIdAsync(id))
                return Ok();
            return NotFound();
        }

        //PUT (update) an item
        //https://localhost:7155/Item/update
        [HttpPut("update")]
        public async Task<ActionResult> UpdateItem([FromBody] Item item)
        {
            if (!(item.Id > 0))
                return NotFound();
            if(item.ImageB64 != null)
                item.Image = Convert.FromBase64String(item.ImageB64);
            if (await db.UpdateItemAsync(item))
                return Ok();
            return BadRequest();
        }
    }
}
