using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RedisMongoApp;
using RedisMongoApp.Models;
using Xunit;

namespace RedisMongoIntegrationTest
{
    public class HomeControllerTest
    {
        private readonly HttpClient client;

        public HomeControllerTest()
        {
            client = new WebApplicationFactory<Startup>().CreateClient();
        }

        [Fact]
        public async Task GetAll_Returns_Empty()
        {
            var httpResponse = await client.GetAsync("home");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            //(await httpResponse.Content.ReadAsAsync<List<Menu>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Post_Returns_Ok()
        {

            using (var client = new HomeControllerTest().client)
            {
                var response = await client.PostAsync("home"
                    , new StringContent(
                        JsonConvert.SerializeObject(new Menu()
                        {
                            id = "11",
                            item = "kabab"
                        }),
                        Encoding.UTF8,
                        "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task Post_WithoutBody_Shows_Error()
        {

            using (var client = new HomeControllerTest().client)
            {
                var response = await client.PostAsync("home"
                    , new StringContent(
                        JsonConvert.SerializeObject(new Menu()
                        {
                            id = "11",
                            item = "kabab"
                        }),
                        Encoding.UTF8,
                        "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}
