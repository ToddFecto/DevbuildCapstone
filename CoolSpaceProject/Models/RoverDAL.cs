using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System.Net.Http;
using CoolSpaceProject.Models;
using Dapper;

namespace CoolSpaceProject.Models
{
    public class RoverDAL
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
        public static List<FavoriteRover> GetFavoriteMarsRovers()
        {
            return ApodDAL.DB.Query<FavoriteRover>("SELECT * FROM favoriteRover WHERE userId = @userId", new { userId = UserDAL.CurrentUserId }).ToList();
        }

        public static async Task<MarsRoverResponse> GetAllRoverPhotosbyEarthDate(string earth_date, string roverName)
        {
            var response = await GetHttpClient().GetAsync($"/mars-photos/api/v1/rovers/{roverName}/photos?api_key=lm9lBypX85VNhjsPJ9RPaeT6G1S83r2dgdZYk3OZ&camera=FHAZ&page=1&earth_date={earth_date}");
            MarsRoverResponse roverresponse = await response.Content.ReadAsAsync<MarsRoverResponse>();
            return roverresponse;
        }

        // CREATE
        public static long SaveFavoriteRover(SaveFavoriteRover saveFavoriteRover)
        {
            FavoriteRover favoriteRover = new FavoriteRover()
            {
                name = saveFavoriteRover.name,
                cameraName = saveFavoriteRover.cameraName,
                image = saveFavoriteRover.image,
                date = saveFavoriteRover.date,
                userId = UserDAL.CurrentUserId
            };
            long responseFromDB = ApodDAL.DB.Insert(favoriteRover);
            if (responseFromDB < 0)
            {
                return -1;
            }
            else
            {
                return responseFromDB;
            }
        }

        // DELETE
        public static bool DeleteFavoriteRover(int favoriteRoverId)
        {
            return ApodDAL.DB.Delete<FavoriteRover>(new FavoriteRover { id = favoriteRoverId });
        }
    }
}
