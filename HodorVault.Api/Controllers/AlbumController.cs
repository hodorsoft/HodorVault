using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HodorVault.Data.Models;
using HodorVault.Data.Repository;
using System;

namespace HodorVault.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/albums")]
    public class AlbumController : Controller
    {
        private readonly IRepository<Album> _repository;

        public AlbumController(IRepository<Album> repository)
        {
            _repository = repository;
        }

        [HttpDelete("{id}")]
        [HttpDelete]
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
