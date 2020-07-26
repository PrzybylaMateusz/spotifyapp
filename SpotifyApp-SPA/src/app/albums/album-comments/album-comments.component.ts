import { Component, Input, OnInit } from '@angular/core';
import { Comment } from 'src/app/_models/comment';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { AlbumService } from 'src/app/_services/album.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-album-comments',
  templateUrl: './album-comments.component.html',
  styleUrls: ['./album-comments.component.css'],
})
export class AlbumCommentsComponent implements OnInit {
  @Input() albumId: string;

  comments: Comment[];
  newComment: any = {};
  pagination: Pagination;
  currentPage: number;
  userId: number;

  constructor(
    private albumService: AlbumService,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.userId = this.authService.decodedToken.nameid;
    this.currentPage = 1;
    this.loadComments();
  }

  loadComments() {
    this.albumService
      .getComments(
        this.authService.decodedToken.nameid,
        this.albumId,
        this.currentPage,
        10
      )
      .subscribe(
        (res: PaginatedResult<Comment[]>) => {
          this.comments = res.results;
          this.pagination = res.pagination;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  pageChanged(event: any): void {
    this.currentPage = event.page;
    this.loadComments();
  }

  addComment() {
    this.newComment.albumId = this.albumId;
    this.albumService
      .addComment(
        this.authService.decodedToken.nameid,
        this.albumId,
        this.newComment
      )
      .subscribe(
        (comment: Comment) => {
          this.comments.unshift(comment);
          this.newComment.commentContent = '';
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  deleteComment(id: number) {
    this.alertify.confirm(
      'Are you sure you want to delete this comment',
      () => {
        this.albumService
          .deleteComment(this.authService.decodedToken.nameid, this.albumId, id)
          .subscribe(
            () => {
              this.comments.splice(
                this.comments.findIndex((c) => c.id === id),
                1
              );
              this.alertify.success('Comment has been deleted');
            },
            (error) => {
              this.alertify.error('Failed to delete the comment');
            }
          );
      }
    );
  }
}
