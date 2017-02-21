using AzureCoreOne.Models.ProBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace AzureCoreOne.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        public void IndexActionModelIsComplete()
        {
            //var controller = new ProBookController
        }

        [Fact]
        public void FaillingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void FirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        private bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        private int Add(int v1, int v2)
        {
            return v1 + v2;
        }

        private static T ProtoDeserialize<T>(byte[] data) where T : class
        {
            if (null == data) return null;

            try
            {
                using (var stream = new System.IO.MemoryStream(data))
                {
                    return ProtoBuf.Serializer.Deserialize<T>(stream);
                }
            }
            catch
            {
                // Log error
                throw;
            }
        }

        [Fact]
        public void ProtoBufTest()
        {
            var client = new HttpClient { BaseAddress = new Uri("https://localhost:44321/") };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

            HttpResponseMessage response = client.GetAsync("api/books").Result;
            
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                //var p = response.Content.ReadAsAsync<Product>(new[] { new ProtoBufFormatter() }).Result;

                //var byteArray = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                // or
                    var bytes = response.Content.ReadAsByteArrayAsync().Result;
                var products = ProtoDeserialize<List<Product>>(bytes);
                //Console.WriteLine("{0}\t{1};\t{2}", p.Name, p.StringValue, p.Id);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Console.ReadLine();

            //// HTTP POST with Protobuf Request Body
            //var responseForPost = client.PostAsync("api/Values", new Product { Id = 1, Name = "test", StringValue = "todo" }, new ProtoBufFormatter()).Result;

            //if (responseForPost.IsSuccessStatusCode)
            //{
            //    Console.WriteLine("All ok");
            //}
            //else
            //{
            //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //}

            //Console.ReadLine();

        }
    }
}
