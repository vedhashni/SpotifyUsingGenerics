using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;



namespace SpotifyUsingGenerics
{
    [TestClass]
    public class RestSpotify
    {
        public string token = "";
        public static string userID = "";
        public static string playlistID = "";
        static string uri = "spotify:track:7mtYsNBYTDPa8Mscf166hg";
        private static IRestClient restClient = new RestClient();
        private static IRestResponse restResponse { get; set; }

        [TestInitialize]

        public void setup()
        {
            token = "Bearer BQDqx83kPb_yZPy56QbRFZ_yaDjurzjbxyPwjBSFsk0Mg82VXMpio9lPk0Ml2ugVw2rAQRTY2ygQswVrtSEup2ANwnp9J222XV5Jdv5Ws9Pq--U4zhPPyDgSYkrXsGDuQmmLIuJSLNAboVtOjKFyEIC3D4muhK8MmvEzSPyb2ERcyl-PIx5piFh5TqccuJ_uiOpTqH9VxZFLIIZ1E2v0vKgskp_uk-S-oMD2UveDAQxyM1684bWS-1jM-P-P_BI0ZJiHZrPBb85HaZI9vgbCFATBFnXesOAaD_gm1qum";
        }

        
        [Priority(1)]
        
        [TestMethod]

        public void CurrentUser()
        {
            
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/me");
            //to check statuscode and content 
            restRequest.AddHeader("Authorization", "token" + token);
            IRestResponse restResponse = restClient.Get(restRequest);
            var obj = JsonConvert.DeserializeObject<dynamic>(restResponse.Content);
            userID = obj.id;
            Console.WriteLine(userID);
            Console.WriteLine(restResponse.Content);
        }

        [Priority(2)]

        [TestMethod]

        public void PlaylistCreated()
        {
            //get_CurrentUser();
            

            string JsonData = "{" +
                                    "\"name\": \"vedhashni's Music Zone song's list\"," +
                                    "\"description\": \"New playlist created\"," +
                                    "\"public\" : \"false\""+
                                    "}";
            
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/users/"+ userID +"/playlists");
            restRequest.AddHeader("Authorization", "token" + token);
            restRequest.AddJsonBody(JsonData);
            restRequest.AddHeader("content-Type", "application/json");
            restResponse = restClient.Post(restRequest);
            var obj1 = JsonConvert.DeserializeObject<dynamic>(restResponse.Content);
            playlistID= obj1.id;
            Console.WriteLine(playlistID);
            Console.WriteLine(restResponse.Content);
        }

        [Priority(3)]

        [TestMethod]
        public void PlaylistDescriptionChanging()
        {
            //CreatePlaylist();

            string json = "{" +
                            "\"name\": \"Updated Music Zone song's list\"," +
                            "\"description\": \"My playlist Created\"," +
                            "\"public\" : false" +
                            "}";

        
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/playlists/" +playlistID+ "/");
            restRequest.AddHeader("Authorization", "token" + token);
            restRequest.AddJsonBody(json);
            restRequest.AddHeader("content-Type", "application/json");
            restResponse = restClient.Put(restRequest);
            Console.WriteLine((int)restResponse.StatusCode);
        }

        [Priority(4)]

        [TestMethod]

        public void TrackAdded()
        {
            //CreatePlaylist();

            
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/playlists/" + playlistID + "/tracks?uris=" + uri);
            restRequest.AddHeader("Authorization", "token" + token);
            restRequest.AddParameter("uris", uri);
            restResponse = restClient.Post(restRequest);
            Console.WriteLine((int)restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);


        }

        [Priority(5)]

        [TestMethod]

        public void TrackDeletion()
        {
            string json = "{ \"tracks\":" +
                         "[{ \"uri\": \"spotify:track:7mtYsNBYTDPa8Mscf166hg\"}]}";

            
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/playlists/" + playlistID + "/tracks");
            restRequest.AddHeader("Authorization", "token" + token);
            restRequest.AddJsonBody(json);
            restResponse = restClient.Delete(restRequest);
            Console.WriteLine((int)restResponse.StatusCode);
        }
    }
}