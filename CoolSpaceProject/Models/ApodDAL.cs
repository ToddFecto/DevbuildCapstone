using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper;
using MySql.Data.MySqlClient;
using System.Net.Http;

namespace CoolSpaceProject.Models
{
    public class ApodDAL
    {
        public static MySqlConnection DB;

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

        //READ
        public static async Task<Apod> GetAPOD()
        {
            var response = await GetHttpClient().GetAsync("/planetary/apod?api_key=lm9lBypX85VNhjsPJ9RPaeT6G1S83r2dgdZYk3OZ&thumbs=true");
            Apod apodresponse = await response.Content.ReadAsAsync<Apod>();
            return apodresponse;
        }

        public static async Task<Apod> GetAPODbyDate(string date)
        {
            var response = await GetHttpClient().GetAsync($"/planetary/apod?api_key=lm9lBypX85VNhjsPJ9RPaeT6G1S83r2dgdZYk3OZ&date={date}&thumbs=true");
            Apod apodresponse = await response.Content.ReadAsAsync<Apod>();
            return apodresponse;
        }

        public static async Task<List<Apod>> GetAPODbyRange(string start_date, string end_date)
        {
            var response = await GetHttpClient().GetAsync($"/planetary/apod?api_key=lm9lBypX85VNhjsPJ9RPaeT6G1S83r2dgdZYk3OZ&start_date={start_date}&end_date={end_date}&thumbs=true");
            List<Apod> apodresponse = await response.Content.ReadAsAsync<List<Apod>>();
            return apodresponse;
        }

        public static async Task<List<Apod>> GetFavoriteApodList()
        {
            List<FavoriteApod> favoriteApods = DB.Query<FavoriteApod>("SELECT * FROM favoriteApod WHERE userId = @userId", new { userId = UserDAL.CurrentUserId }).ToList();
            List<Apod> apods = new List<Apod>();
            foreach (FavoriteApod favorite in favoriteApods)
            {
                apods.Add(await GetAPODbyDate(favorite.date));
            }
            return apods;
        }

        public static int GetFavoriteApodId(string date)
        {
            if (UserDAL.CurrentUserId == -1)
            {
                return -1;
            }
            List<int> favoriteId = DB.Query<int>("SELECT id FROM favoriteApod WHERE userId = @userId AND date = @date", new { userId = UserDAL.CurrentUserId, date = date }).ToList();
            if (favoriteId.Count > 0)
            {
                return favoriteId[0];
            }
            else
            {
                return -1;
            }
        }

        //CREATE
        public static long SaveFavAPOD(SaveFavoriteApod date)
        {
            FavoriteApod theapod = new FavoriteApod()
            {
                date = date.date,
                userId = UserDAL.CurrentUserId
            };
            long responseFromDB = DB.Insert(theapod);
            if (responseFromDB < 0)
            {
                return -1;
            }
            else
            {
                return responseFromDB;
            }
        }

        //DELETE
        public static bool DeleteFavApod(int id)
        {
            return DB.Delete<FavoriteApod>(new FavoriteApod() { id = id });
        }

    }
}
