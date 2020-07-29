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
    public class AlbumsControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient Client { get; }

        public AlbumsControllerTests(WebApplicationFactory<Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Retrieve_Album_When_Id_Is_Correct()
        {
            // Arrange
            const string sampleAlbumId = "6b1HPtDuYioXwmw5xLLFQ9";
            var expectedResult = new AlbumDto
            {
                Artist = "Rhye",
                ArtistId = "2AcUPzkVWo81vumdzeLLRN",
                CoverUrl = "https://i.scdn.co/image/ab67616d0000b273a13907cf58fb1bd99f90a543",
                Id = sampleAlbumId,
                Name = "Woman",
                UserId = 1,
                Year = "2013"
            };

            // Act
            var response = await Client.GetAsync("api/albums/" + sampleAlbumId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var actual = JsonConvert.DeserializeObject<AlbumDto>(await response.Content.ReadAsStringAsync());
            actual.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Get_Should_Return_Bad_Request_When_Id_Is_Incorrect()
        {
            // Arrange
            const string incorrectSampleAlbumId = "incorrectAlbumId";

            // Act
            var response = await Client.GetAsync("api/albums/" + incorrectSampleAlbumId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
