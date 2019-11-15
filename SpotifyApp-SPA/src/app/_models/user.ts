import { Album } from "./album";

export interface User {
  id: number;
  username: string;
  created: Date;
  photoUrl: string;
  about?: string;
  albums?: Album[];
}
