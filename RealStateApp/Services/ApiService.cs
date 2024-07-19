﻿using Microsoft.Win32;
using Newtonsoft.Json;
using RealStateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace RealStateApp.Services
{
    public class ApiService
    {
        public static async Task<bool> RegisterUser(string name,string email,string password,string phone)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone,
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(register);

            var content = new StringContent(json,Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Users/Register",content);
            if (!response.IsSuccessStatusCode) 
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> Login(string email,string password)
        {

            var login = new Login()
            {
                Email = email,
                Password = password,
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Users/Login", content);
            
            
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accesstoken", result.AccessToken);
            Preferences.Set("userid", result.UserId);
            Preferences.Set("username", result.UserName);

            return true;
        }

        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",Preferences.Get("accesstoken",string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<List<TrendingProperty>> GetTrendingProperties()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/TrendingProperties");
            return JsonConvert.DeserializeObject<List<TrendingProperty>>(response);
        }


        public static async Task<List<SearchProperty>> FindPreperties(string address)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/Properties/SearchProperties?address={address}");
            return JsonConvert.DeserializeObject<List<SearchProperty>>(response);
        }

        public static async Task<List<PropertyByCategory>> GetPropertyByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/Properties/PropertyList?categoryId={categoryId}");
            return JsonConvert.DeserializeObject<List<PropertyByCategory>>(response);
        }

        public static async Task<PropertyDetail> GetPropertyDetail(int propertyId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync($"{AppSettings.ApiUrl}api/Properties/PropertyDetail?id={propertyId}");
            return JsonConvert.DeserializeObject<PropertyDetail>(response);
        }


        public static async Task<List<BookmarkList>> GetBookmarkList()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Bookmarks");
            return JsonConvert.DeserializeObject<List<BookmarkList>>(response);
        }

        public static async Task<bool> AddBookMard(AddBookmark addbookmark)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var json = JsonConvert.SerializeObject(addbookmark);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Bookmarks",content);

            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> DeleteBookMard(int propertyId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "api/Bookmarks/" + propertyId);

            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

    
    }
}
