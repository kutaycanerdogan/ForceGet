import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  private apiUrl = 'https://localhost:5001/api/city';

  constructor(private http: HttpClient) {}

  getCitiesByCountry(country: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/country/${country}`);
  }
}
