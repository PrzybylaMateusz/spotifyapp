/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RatesService } from './rates.service';

describe('Service: Rates', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RatesService]
    });
  });

  it('should ...', inject([RatesService], (service: RatesService) => {
    expect(service).toBeTruthy();
  }));
});
