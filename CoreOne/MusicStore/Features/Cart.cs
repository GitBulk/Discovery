using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Controllers;
using MusicStore.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStore.Features
{
    public class AddToCartRequest: IRequest<Unit>
    {
        public string CartId { get; }
        public int AlbumId { get; }

        public AddToCartRequest(string cartId, int albumId)
        {
            this.CartId = cartId;
            this.AlbumId = albumId;
        }
    }

    public class AddtoCartHandler : ICancellableAsyncRequestHandler<AddToCartRequest, Unit>
    {
        private readonly MusicStoreContext dbContext;
        private readonly ILogger<ShoppingCartController> logger;
        public AddtoCartHandler(MusicStoreContext dbContext, ILogger<ShoppingCartController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Unit> Handle(AddToCartRequest message, CancellationToken cancellationToken)
        {
            // Retrieve the album from the database
            var addedAlbum = await dbContext.Albums
                .SingleAsync(album => album.AlbumId == message.AlbumId, cancellationToken);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(dbContext, message.CartId);

            await cart.AddToCart(addedAlbum);

            await dbContext.SaveChangesAsync(cancellationToken);
            this.logger.LogInformation("Album {albumId} was added to the cart.", addedAlbum.AlbumId);


            return Unit.Value;
        }
    }
}
