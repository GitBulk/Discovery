using System.Net.Http;
using System.Threading.Tasks;

namespace AzureCoreOne.Models.ProBook
{
    public class MyAsyncMethods
    {
        //public static Task<long?> GetPageLength()
        //{
        //    HttpClient client = new HttpClient();
        //    var worker = client.GetAsync("http://apress.com");

        //    // we could do other things here while http request is performed
        //    return worker.ContinueWith((Task<HttpResponseMessage> antecedent) =>
        //    {
        //        return antecedent.Result.Content.Headers.ContentLength;
        //    });
        //}
        public async static Task<long?> GetPageLength()
        {
            var client = new HttpClient();
            var message = await client.GetAsync("http://apress.com");
            return message.Content.Headers.ContentLength;
        }
    }
}
