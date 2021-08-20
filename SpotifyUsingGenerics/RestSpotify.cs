using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RestSharp;


namespace SpotifyUsingGenerics
{
    [TestClass]
    public class RestSpotify
    {
        public string token = "";
        public string userID = "";

        public string playlistName = "";
        public string playlistID = "";

        public string updatedplaylistName = "";

        private static IRestResponse restResponse { get; set; }

        [TestInitialize]

        public void setup()
        {
            token = "Bearer BQDYU2EwS1ULCWXZ83Rg41jQ68G69X1s6_kedH4tdnr8zZQjkcjxPg8Sqvqhi9xvje-DJxy7SO8khUJLM0h_tIwmCg0jKIJopDLtMteT1DpKy2_ns-Eln-FtEKk00nqc48iQiZDo_-Z6O35fItgt2imd967Gut7PQIJXQzm9wENaQvopZ6dYo5cTFJaxTrVnSWic0lMMQBfQOtoSn7RGuSpJmZC4a5k6dT6xFFHcZD9LKCla0F_HyLD8Op1b-Nlvse2kDqdq3aVeIrRYr0FQ9lh_mljYXFKp3EiKJqU5";
        }

        [TestMethod]

        public void get_CurrentUser()
        {
            
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/me");
            //to check statuscode and content 
            restRequest.AddHeader("Authorization", "token" + token);
            IRestResponse restResponse = restClient.Get(restRequest);

            IRestResponse<List<JsonObjects>> restResponse1 = restClient.Get<List<JsonObjects>>(restRequest);
            var dataobjects = restResponse1.Data;
            foreach (var d in dataobjects)
            {
                userID = d.id;
            }
            Console.WriteLine(userID);

        }

        [TestMethod]

        public void CreatePlaylist()
        {
            get_CurrentUser();
            

            string JsonData = "{" +
                                    "\"name\": \"vedhashni's Music Zone song's list\"," +
                                    "\"description\": \"New playlist created\"," +
                                    "\"public\" : \"false\""+
                                    "}";
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/users/"+userID+"/playlists");

            restRequest.AddHeader("Authorization", "token" + token);

            //restRequest.AddHeader("content-Type", "application/json");
            restRequest.AddJsonBody(JsonData);
            restRequest.AddHeader("content-Type", "application/json");
            //Assert.AreEqual(201, 202, (int)restResponse.StatusCode);
            IRestResponse<List<JsonObjects>> restResponse2 = restClient.Post<List<JsonObjects>>(restRequest);
            Console.WriteLine((int)restResponse2.StatusCode);
            var dataobjects1 = restResponse2.Data;
            foreach (var d in dataobjects1)
            {
                playlistName = d.name;
                playlistID = d.id;

            }
            Console.WriteLine(playlistID);
            Console.WriteLine(playlistName);
        }

        [TestMethod]
        public void ChangePlaylistDescription()
        {
            CreatePlaylist();

            string json = "{" +
                            "\"name\": \"Updated Music Zone song's list\"," +
                            "\"description\": \"My playlist Created\"," +
                            "\"public\" : false" +
                            "}";

        

        
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest("https://api.spotify.com/v1/playlists/"+playlistID+"/");

            restRequest.AddHeader("Authorization", "token" + token);

            restRequest.AddJsonBody(json);
            
            restRequest.AddHeader("content-Type", "application/json");

            restResponse = restClient.Put(restRequest);
            Console.WriteLine((int)restResponse.StatusCode);
            
            
            
        }
    }
}
