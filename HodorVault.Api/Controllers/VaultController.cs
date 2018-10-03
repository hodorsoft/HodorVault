using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HodorVault.Data.Models;
using HodorVault.Api.Models;
using HodorVault.Data.Services;
using System.Collections.Generic;

namespace HodorVault.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VaultController : Controller
    {
        private readonly IVaultService _vaultService;

        public VaultController(IVaultService vaultService)
        {
            _vaultService = vaultService;
        }
        
        [HttpGet]
        public async Task<IList<Artist>> Get()
        {
            return await _vaultService.GetAllEntries();
        }

        // POST: api/vault - create
        [HttpPost]
        public async Task<EntryResult> Post([FromBody]VaultEntryJson vaultEntry)
        {
            var artist = new Artist
            {
                Id = vaultEntry.Artist.id,
                Name = vaultEntry.Artist.name,
                Country = vaultEntry.Artist.country
            };
            var album = new Album
            {
                Id = vaultEntry.Album.id,
                Name = vaultEntry.Album.name,
                Type = vaultEntry.Album.type,
                Year = vaultEntry.Album.year,
                ArtistId  = vaultEntry.Artist.id
            };

            EntryResult result = await _vaultService.InsertOrUpdateEntry(artist, album);

            return result;
        }

        [HttpPut]
        public async Task<EntryResult> Put([FromBody]VaultEntryJson vaultEntry)
        {
            var artist = new Artist
            {
                Id = vaultEntry.Artist.id,
                Name = vaultEntry.Artist.name,
                Country = vaultEntry.Artist.country
            };
            var album = new Album
            {
                Id = vaultEntry.Album.id,
                Name = vaultEntry.Album.name,
                Type = vaultEntry.Album.type,
                Year = vaultEntry.Album.year,
                ArtistId = vaultEntry.Artist.id
            };

            EntryResult result = await _vaultService.InsertOrUpdateEntry(artist, album);

            return result;
        }
    }
}
