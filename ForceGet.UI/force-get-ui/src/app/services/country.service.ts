import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environments';
import { Country } from '../models/country.model';

@Injectable({
  providedIn: 'root',
})
export class CountryService {
  private apiUrl = environment.apiUrl + '/country';

  constructor(private http: HttpClient) {}

  getCountries(country: string): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.apiUrl}/${country}`);
  }
}
