using Microsoft.AspNetCore.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TicketBooking.Infrastructure.Services.ApiClient;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(IWebHostEnvironment webHostEnvironment)
    {
        // Create an instance of HttpClient
        _httpClient = new HttpClient();
        // Optionally configure HttpClient with base URL, headers, etc.
        _httpClient.BaseAddress = new Uri(webHostEnvironment.ContentRootPath);
        //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_ACCESS_TOKEN");
    }

    public async Task<string> GetResourceAsync(string resourcePath)
    {
        // Send a GET request
        HttpResponseMessage response = await _httpClient.GetAsync(resourcePath);

        // Check if the response is successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as string
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            // Handle unsuccessful response
            throw new HttpRequestException($"Failed to fetch resource. Status code: {response.StatusCode}");
        }
    }
}
