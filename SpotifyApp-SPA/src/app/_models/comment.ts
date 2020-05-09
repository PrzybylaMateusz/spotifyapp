export interface Comment {
  id: number;
  commenterId: number;
  commenterUsername: string;
  commenterPhotoUrl: string;
  albumId: string;
  commentContent: string;
  commentSent: Date;
}
