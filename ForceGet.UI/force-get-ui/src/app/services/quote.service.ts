import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Quote } from '../models/quote.model';
import { environment } from '../../environments/environments';

@Injectable({
  providedIn: 'root',
})
export class QuoteService {
  private apiUrl = environment.apiUrl + '/quote';

  constructor(private http: HttpClient) {}

  createQuote(quote: Quote): Observable<Quote> {
    return this.http.post<Quote>(this.apiUrl, quote);
  }

  getQuotes(): Observable<Quote[]> {
    return this.http.get<Quote[]>(this.apiUrl);
  }
}
