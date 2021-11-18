﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoolSpaceProject.Models
{
    public class TechTransferDAL
    {
        private static HttpClient client = null;

        private static HttpClient GetHttpClient()
        {
            // Building a **SINGLETON** object of type HttpClient - avoids a lot extra traffic

            if (client == null)
            {
                // client instance hasn't been made yet, make it and initialize it
                client = new HttpClient();
                client.BaseAddress = new Uri("https://api.nasa.gov");
            }
            return client;
        }

        public static async Task<TechTransferResponse> SearchPatents(string search)
        {
            var response = await GetHttpClient().GetAsync($"/techtransfer/patent/?${search}&api_key=AhkPJXB4fyYZfBLWbVJBv5HxNDZVUUb5ceAaC88r");
            TechTransferResponse roverresponse = await response.Content.ReadAsAsync<TechTransferResponse>();
            return roverresponse;
        }
    }
}