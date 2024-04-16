import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IPerson } from '../models/person.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  _http = inject(HttpClient);
  apiUrl = 'https://localhost:7276';
  constructor() {}

  getAllPersons() {
    return this._http.get<IPerson[]>(this.apiUrl + '/api/persons');
  }
  getPerson(id: number):Observable<IPerson>{
    return this._http.get<IPerson>(`${this.apiUrl}/${id}`);
  }
}
