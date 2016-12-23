using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Tam.Core.Utilities
{
    public static class ViewExtension
    {
        public static void AddExternalViewComponent<TViewComponent>(this IServiceCollection services, string baseNameSpace) where TViewComponent : ViewComponent
        {
            Guard.ThrowIfNullOrWhiteSpace(baseNameSpace);
            var assembly = typeof(TViewComponent).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly, baseNameSpace);
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.FileProviders.Add(embeddedFileProvider);
            });
        }
    }
}
