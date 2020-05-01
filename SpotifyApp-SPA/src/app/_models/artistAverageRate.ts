import { Artist } from './artist';

export interface ArtistAverageRate {
  artist: Artist;
  rate: number;
  numberOfRates: number;
}
