import { Album } from './album';

export interface AlbumAverageRate {
  album: Album;
  rate: number;
  numberOfRates: number;
}
