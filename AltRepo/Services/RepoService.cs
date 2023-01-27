using AltRepo.Models;
using AltRepo.Models.Config;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AltRepo.Services
{
    public class RepoService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public RepoService(IMemoryCache memoryCache, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.memoryCache = memoryCache;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<Repo> GetCachedRepoAsync()
        {
            return await memoryCache.GetOrCreateAsync(
                $"AltRepo",
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(int.Parse(configuration["AltRepo:CacheSeconds"]));
                    return GetRepoAsync();
                });
        }

        public async Task<Repo> GetRepoAsync()
        {
            Repo repo = new Repo()
            {
                Name = configuration["AltRepo:Name"],
                Identifier = configuration["AltRepo:Identifier"],
                SourceURL = configuration["AltRepo:SourceURL"],
                Apps = new List<RepoApp>(),
                News = new List<RepoNews>(),
                UserInfo = new()
            };

            var aggregateRepos = configuration.GetSection("AltRepo:AggregateRepos").Get<List<AggregateRepo>>();
            Dictionary<AggregateRepo, Task<Repo>> aggregateRepoTasks = new();
            foreach (var aggRepo in aggregateRepos)
            {
                if (aggRepo.Url == null)
                {
                    continue;
                }
                aggregateRepoTasks.Add(aggRepo, GetRemoteRepoAsync(aggRepo.Url));
            }
            await Task.WhenAll(aggregateRepoTasks.Values);
            foreach (var aggRepoTaskKvp in aggregateRepoTasks)
            {
                var aggRepo = aggRepoTaskKvp.Key;
                var remoteRepo = aggRepoTaskKvp.Value.Result;

                if (aggRepo.IncludeApps && remoteRepo?.Apps != null)
                {
                    repo.Apps.AddRange(remoteRepo.Apps);
                }
                if (aggRepo.IncludeNews && remoteRepo?.News != null)
                {
                    repo.News.AddRange(remoteRepo.News);
                }
            }

            return repo;
        }

        private async Task<Repo> GetRemoteRepoAsync(Uri url)
        {
            var httpClient = httpClientFactory.CreateClient(nameof(RepoService));

            HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, url);
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpRequestMessage.Headers.UserAgent.Add(new ProductInfoHeaderValue("AltRepo", "1.0"));

            var response = await httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Repo>(responseString)!;
        }
    }
}
