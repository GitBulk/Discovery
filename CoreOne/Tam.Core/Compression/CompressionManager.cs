using Microsoft.AspNetCore.Http;

namespace Tam.Core.Compression
{
    public static class CompressionManager
    {

        public static bool IsGzipSupported(HttpContext context)
        {
            string acceptEncoding = context.Request.Headers[CompressionInfo.AcceptEncoding];
            if (context != null && context.Request != null && context.Response != null && context.Response.Body != null &&
                !string.IsNullOrEmpty(acceptEncoding) &&
                (acceptEncoding.Contains(CompressionInfo.GzipMode) || acceptEncoding.Contains(CompressionInfo.DeflateMode)))
            {
                return true;
            }
            return false;
        }
    }
}
