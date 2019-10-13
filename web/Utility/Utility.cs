using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using web.Domain.DataTransferObjects;

namespace web.Utility
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }
    }
    
    public class ApiHelper
    {
        # region Private Helpers
        // Courtesy of: https://johnthiriet.com/efficient-api-calls
        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }

        private static async Task<T> DeserializeFromStreamCallAsync<T>(CancellationToken cancellationToken, string uri, HttpMethod method)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(method, uri))
            using (var response = await client.SendAsync(request, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                    return DeserializeJsonFromStream<T>(stream);

                var content = await StreamToStringAsync(stream);
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }
        }

        private static async Task<bool> PostData<T>(string uri, T entity)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(PostData)}");
            Console.WriteLine($"DEBUG: Updating entity");
            Console.WriteLine($"DEBUG: Entity type: {typeof(T)}");
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);
                Console.WriteLine($"DEBUG: Response: {response}");
                return response.IsSuccessStatusCode;
            }
        }

        private static async Task<bool> PostData(string uri)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(PostData)}");
            Console.WriteLine($"DEBUG: Creating entity");            
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(uri, null);
                Console.WriteLine($"DEBUG: Response: {response}");
                return response.IsSuccessStatusCode;
            }
        }

        # endregion

        public static async Task<List<Product>> GetProducts()
        {
            Console.WriteLine($"DEBUG: Entering {nameof(GetProducts)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await DeserializeFromStreamCallAsync<List<Product>>(token, "http://transaction/api/Products", HttpMethod.Get);
        }

        public static async Task<Product> GetProductById(int id)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(GetProductById)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await DeserializeFromStreamCallAsync<Product>(token, $"http://transaction/api/Products/{id}", HttpMethod.Get);
        }

        public static async Task<bool> UpdateProduct(Product product)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(UpdateProduct)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await PostData("http://transaction/api/Products", product);
        }

        public static async Task DeleteProduct(int id)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(DeleteProduct)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            // TODO: Since the return from delete is void and void cannot be used in generics, must use object to absorb type?
            await DeserializeFromStreamCallAsync<object>(token, $"http://transaction/api/Products/{id}", HttpMethod.Delete);
        }

        public static async Task<List<Transaction>> GetTransactions()
        {
            Console.WriteLine($"DEBUG: Entering {nameof(GetTransactions)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            // Use cancellation token instead of setting timeout on HttpClient
            // This allows customization per call?
            // cts.CancelAfter(2);
            CancellationToken token = cts.Token;
            return await DeserializeFromStreamCallAsync<List<Transaction>>(token, "http://transaction/api/Transactions", HttpMethod.Get);
        }

        public static async Task<Transaction> GetTransactionById(int id)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(GetTransactionById)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await DeserializeFromStreamCallAsync<Transaction>(token, $"http://transaction/api/Transactions/{id}", HttpMethod.Get);
        }

        public static async Task<bool> UpdateTransaction(Transaction transaction)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(UpdateTransaction)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await PostData("http://transaction/api/Transactions", transaction);
        }

        public static async Task DeleteTransaction(int id)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(DeleteTransaction)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            // TODO: Since the return from delete is void and void cannot be used in generics, must use object to absorb type?
            await DeserializeFromStreamCallAsync<object>(token, $"http://transaction/api/Transactions/{id}", HttpMethod.Delete);
        }

        public static async Task<List<Client>> GetClients()
        {
            Console.WriteLine($"DEBUG: Entering {nameof(GetClients)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await DeserializeFromStreamCallAsync<List<Client>>(token, "http://transaction/api/Clients", HttpMethod.Get);
        }

        public static async Task<Client> GetClientById(int id)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(GetClientById)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await DeserializeFromStreamCallAsync<Client>(token, $"http://transaction/api/Clients/{id}", HttpMethod.Get);
        }

        public static async Task<bool> UpdateClient(Client client)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(UpdateClient)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await PostData("http://transaction/api/Clients", client);
        }

        public static async Task DeleteClient(int id)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(DeleteClient)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            // TODO: Since the return from delete is void and void cannot be used in generics, must use object to absorb type?
            await DeserializeFromStreamCallAsync<object>(token, $"http://transaction/api/Clients/{id}", HttpMethod.Delete);
        }

        public static async Task<bool> QueueDeliveryOptimization()
        {
            Console.WriteLine($"DEBUG: Entering {nameof(QueueDeliveryOptimization)}");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            return await PostData("http://delivery/api/Deliveries");
        }
    }
}
