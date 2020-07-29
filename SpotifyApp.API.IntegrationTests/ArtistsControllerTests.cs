using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SpotifyApp.API.Dtos;
using Xunit;

namespace SpotifyApp.API.IntegrationTests
{
    public class ArtistsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient Client { get; }

        public ArtistsControllerTests(WebApplicationFactory<Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Retrieve_Artist_When_Id_Is_Correct()
        {
            // Arrange
            const string sampleArtistId = "1uUQlxPYutoy5FSHa5jbWo";
            var expectedResult = new ArtistWithAlbumsDto
            {
                Id = sampleArtistId,
                Name = "asdfhg.",
                PhotoUrl = "https://i.scdn.co/image/22b21d5129275eed1b9ddddb9749a75fb96e1d73",
                Albums = new List<AlbumDto>()
                {
                    new AlbumDto()
                    {
                        Id = "4gkivR7J6yF9bD7Fy9CRZq",
                        Artist = "asdfhg.",
                        ArtistId = null,
                        CoverUrl =
                            "https://i.scdn.co/image/ab67616d0000b2732a63fa43ed3193e0dce63555",
                        Name =
                            "Örvæntið ekki!",
                        UserId = 1,
                        Year =
                        "2018",
                    },
                    new AlbumDto()
                    {
                        Id = "0j8c7IwUFotpv4czVhJPNY",
                        Artist = "asdfhg.",
                        ArtistId = null,
                        CoverUrl =
                            "https://i.scdn.co/image/ab67616d0000b2735e50223b42867c0b3602f5d1",
                        Name =
                            "Kliður",
                        UserId = 1,
                        Year =
                            "2016",
                    }
                }
            };

            // Act
            var response = await Client.GetAsync("api/artists/" + sampleArtistId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var actual = JsonConvert.DeserializeObject<ArtistWithAlbumsDto>(await response.Content.ReadAsStringAsync());
            actual.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Get_Should_Return_Bad_Request_When_Id_Is_Incorrect()
        {
            // Arrange
            const string incorrectSampleArtistId = "incorrectArtistId";

            // Act
            var response = await Client.GetAsync("api/artists/" + incorrectSampleArtistId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
