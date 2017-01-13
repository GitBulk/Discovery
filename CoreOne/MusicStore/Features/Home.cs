using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Features
{
    public class HomeIndexRequest : IRequest<List<Album>>
    {
    }

    public class HomeIndexHandler : IAsyncRequestHandler<HomeIndexRequest, List<Album>>
    {
        private readonly MusicStoreContext dbContext;
        private readonly IMemoryCache cache;
        private readonly IOptions<AppSettings> options;
        public HomeIndexHandler(MusicStoreContext dbContext, IMemoryCache cache,
            IOptions<AppSettings> options)
        {
            this.dbContext = dbContext;
            this.cache = cache;
            this.options = options;
        }

        public async Task<List<Album>> Handle(HomeIndexRequest message)
        {
            // Get most popular albums
            var cacheKey = "topselling";
            List<Album> albums;
            if (!cache.TryGetValue(cacheKey, out albums))
            {
                albums = await GetTopSellingAlbumsAsync(12);

                if (albums != null && albums.Count > 0)
                {
                    AppSettings appSettings = this.options.Value;
                    if (appSettings.CacheDbResults)
                    {
                        // Refresh it every 10 minutes.
                        // Let this be the last item to be removed by cache if cache GC kicks in.
                        cache.Set(
                            cacheKey,
                            albums,
                            new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                            .SetPriority(CacheItemPriority.High));
                    }
                }
            }
            return albums;
        }

        private Task<List<Album>> GetTopSellingAlbumsAsync(int count = 10)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return dbContext.Albums
                .OrderByDescending(a => a.OrderDetails.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}
