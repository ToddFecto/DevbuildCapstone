using Dapper;
using Dapper.Contrib.Extensions;
using System;
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
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://api.nasa.gov");
            }
            return client;
        }

        // READ
        public static async Task<TechTransferResponse> SearchPatents(string search)
        {
            var response = await GetHttpClient().GetAsync($"/techtransfer/patent/?${search}&api_key=lm9lBypX85VNhjsPJ9RPaeT6G1S83r2dgdZYk3OZ");
            TechTransferResponse roverresponse = await response.Content.ReadAsAsync<TechTransferResponse>();
            return roverresponse;
        }

        public static List<Search> GetPreviousSearches()
        {
            return ApodDAL.DB.Query<Search>("SELECT * FROM searchHistory WHERE userId = @userId ORDER BY id DESC LIMIT 5", new { userId = UserDAL.CurrentUserId }).ToList();
        }

        // CREATE
        public static bool SaveSearch(string search)
        {
            Search savedSearch = new Search()
            {
                searchString = search,
                userId = UserDAL.CurrentUserId
            };
            long responseFromDB = ApodDAL.DB.Insert(savedSearch);
            if (responseFromDB < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
