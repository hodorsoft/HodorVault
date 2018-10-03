using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HodorVault.Data.Models;
using HodorVault.Data.Repository;
using System;

namespace HodorVault.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/artists")]
    public class ArtistController : Controller
    {
        private readonly IRepository<Artist> _repository;

        public ArtistController(IRepository<Artist> repository)
        {
            _repository = repository;
        }

        [HttpDelete("{id}")]
        public async Task<dynamic> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                return new { success = false, error = e.Message };
            }

            return new { success = true };
        }
    }
}
