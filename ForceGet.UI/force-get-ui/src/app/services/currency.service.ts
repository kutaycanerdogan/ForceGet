import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CurrencyService {
  private apiUrl = 'https://localhost:5001/api/currency';

  constructor(private http: HttpClient) {}

  convertToUsd(
    fromCurrency: string,
    amount: number
  ): Observable<{ amount: number }> {
    return this.http.get<{ amount: number }>(
      `${this.apiUrl}/convert/${fromCurrency}/${amount}`
    );
  }
}
