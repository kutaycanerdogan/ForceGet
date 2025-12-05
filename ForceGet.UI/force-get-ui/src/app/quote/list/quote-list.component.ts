import { Component, OnInit } from '@angular/core';
import { QuoteService } from '../../services/quote.service';
import { Quote } from '../../models/quote.model';
import {
  Mode,
  MovementType,
  Incoterm,
  PackageType,
  CurrencyType,
} from '../../models/enums.model';
@Component({
  selector: 'app-quote-list',
  templateUrl: './quote-list.component.html',
  styleUrls: ['./quote-list.component.scss'],
})
export class QuoteListComponent implements OnInit {
  quotes: Quote[] = [];

  Mode = Mode;
  MovementType = MovementType;
  Incoterm = Incoterm;
  PackageType = PackageType;
  CurrencyType = CurrencyType;

  constructor(private quoteService: QuoteService) {}

  ngOnInit(): void {
    this.quoteService.getQuotes().subscribe({
      next: (quotes) => {
        this.quotes = quotes;
      },
      error: (err) => {
        console.error('Error fetching quotes', err);
      },
    });
  }
}
