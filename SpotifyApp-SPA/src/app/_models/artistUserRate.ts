import { Artist } from './artist';

export interface ArtistUserRate {
  artist: Artist;
  rate: number;
  dateOfRate: Date;
}
