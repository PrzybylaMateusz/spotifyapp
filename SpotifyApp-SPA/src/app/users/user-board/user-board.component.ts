import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { RatesService } from 'src/app/_services/rates.service';
import { AlertifyService } from '../../_services/alertify.service';
import { AlbumUserRate } from 'src/app/_models/albumUserRate';

@Component({
  selector: 'app-user-board',
  templateUrl: './user-board.component.html',
  styleUrls: ['./user-board.component.css'],
})
export class UserBoardComponent {}
