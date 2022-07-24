using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using MyApp.Models;

namespace MyApp.DataStore
{
    public  class DataManager
    {
        public static DataManager network_manager = new DataManager();
        public static string network_url = "http://feeds.sciencedaily.com/sciencedaily?format=xml";
        public DataManager()
        {
        }

        public static DataManager Instance
        {
            get
            {
                return network_manager;
            }
        }

        //public async Task<List<FeedItem>> GetSyncFeedAsync()
        //{
        //    if (this.IsConnected())
        //    {
        //        Uri uri = new Uri(network_url);
        //        HttpClient client = new HttpClient();
        //        HttpResponseMessage response = await client.GetAsync(uri);
        //        String response_string = await response.Content.ReadAsStringAsync();
        //        FeedItemParser parser = new FeedItemParser();
        //        // List<FeedItem> list = await Task.Run(() => parser.ParseFeed(response_string));
        //        List<FeedItem> list = await Task.Run(() => parser.ParseFeed(response_string));
        //        return list;
        //    }
        //    return null;
        //}

        public bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;
            if (current == Xamarin.Essentials.NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }
    }
}