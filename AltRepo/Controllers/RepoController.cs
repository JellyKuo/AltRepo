using AltRepo.Models;
using AltRepo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AltRepo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RepoController : ControllerBase
    {
        private readonly RepoService repoService;

        public RepoController(RepoService repoService)
        {
            this.repoService = repoService;
        }

        [HttpGet("/")]
        [HttpGet("Get")]
        public async Task<Repo> Get()
        {
            return await repoService.GetCachedRepoAsync();
        }
    }
}
