using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpotifyApp.API.Data;
using SpotifyApp.API.Dtos;
using SpotifyApp.API.Helpers;
using SpotifyApp.API.Models;

namespace SpotifyApp.API.Controllers
{
    [Route("api/[controller]/{userId}/{albumId}")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAppRepository repo;
        public CommentsController(IAppRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetComment")]
        public async Task<IActionResult> GetComment(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var commentFromRepo = await this.repo.GetComment(id);

            if(commentFromRepo == null)
                return NotFound();

            return Ok(commentFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsForAlbum(int userId, string albumId, [FromQuery]CommentParams commentParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            commentParams.AlbumId = albumId;

            var commentsFromRepo = await this.repo.GetCommentsForAlbum(commentParams);

            var comments = this.mapper.Map<IEnumerable<CommentToReturnDto>>(commentsFromRepo);

            Response.AddPagination(commentsFromRepo.CurrentPage, commentsFromRepo.PageSize, commentsFromRepo.TotalCount, commentsFromRepo.TotalPages);

            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int userId, string albumId, CommentForCreationDto commentForCreationDto)
        {
            var commenter = await this.repo.GetUser(userId);

            if (commenter.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            commentForCreationDto.AlbumId = albumId;
            commentForCreationDto.CommenterId = userId;

            var album = await this.repo.GetAlbum(commentForCreationDto.AlbumId);

             if(album == null)
            {
                repo.AddAlbum(new Album {Id = commentForCreationDto.AlbumId});
            }              

            var comment = this.mapper.Map<Comment>(commentForCreationDto);
            this.repo.Add(comment);

            if (!await this.repo.SaveAll())
            {
                throw new Exception("Creating the comment failed on save");
            }
            var commentToReturn = this.mapper.Map<CommentToReturnDto>(comment);
            return CreatedAtRoute("GetComment", new { userId, albumId, id = comment.Id}, commentToReturn);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int userId, int id)
        {            
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var commentFromRepo = await this.repo.GetComment(id);

            if(commentFromRepo.CommenterId != userId)
            {
                return BadRequest("You cannot delete not your comment");
            }

            this.repo.Delete(commentFromRepo);

             if(await this.repo.SaveAll())
                return NoContent();

            throw new Exception("Error deleting the comment");
        }
    }
}