import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environments';
import { CurrencyConversion } from '../models/currency-converstaion.model';

@Injectable({
  providedIn: 'root',
})
export class CurrencyService {
  private apiUrl = environment.apiUrl + '/currency';

  constructor(private http: HttpClient) {}

  convertCurrency(
    currencyConversion: CurrencyConversion
  ): Observable<{ amount: number }> {
    return this.http.post<{ amount: number }>(
      `${this.apiUrl}/convert`,
      currencyConversion
    );
  }
}
