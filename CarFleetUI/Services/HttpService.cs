using Blazored.LocalStorage;
using CarFleetAPI.Models;
using CarFleetModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarFleetUI.Services
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task<T> Post<T>(string uri, object value);
        Task Put(string uri, object value);
        Task<List<T>> GetAll<T>(string uri);
    }

    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private IConfiguration _configuration;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IConfiguration configuration
        ) {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task<List<T>> GetAll<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequestAll<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task Put(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            await sendRequestPut(request);
        }

        // helper methods

        private async Task<T> sendRequest<T>(HttpRequestMessage request)
        {
          
            var user = await _localStorageService.GetItemAsync<UserModel>("user");
            //var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (user != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.AccessToken);

            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return default;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task<List<T>> sendRequestAll<T>(HttpRequestMessage request)
        {

            var user = await _localStorageService.GetItemAsync<UserModel>("user");
            //var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (user != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.AccessToken);

            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            return await response.Content.ReadFromJsonAsync<List<T>>();
        }
        private async Task sendRequestPut(HttpRequestMessage request)
        {

            var user = await _localStorageService.GetItemAsync<UserModel>("user");
            //var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (user != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.AccessToken);

            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                //return false;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                //return false;
            }
            if (response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                //return false;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            //return true;
        }
    }
}