import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IPerson } from 'src/app/models/person.model';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  _http = inject(HttpClient);
  apiUrl = 'https://localhost:7276';
  constructor() {}

  getAllPersons(username: string, password: string) {
    const headers = new HttpHeaders({
      'Authorization': 'basic ' + btoa(username + ':' + password)
    });

    return this._http.get<IPerson[]>(this.apiUrl + '/api/persons', { headers });
  }

  getPerson(idNumber: string):Observable<IPerson>{
    return this._http.get<IPerson>(`${this.apiUrl + '/api/persons'}/${idNumber}`);
  }

  createPerson(person: IPerson){
    return this._http.post(this.apiUrl + '/api/persons', person);
  }

  updatePerson(person: IPerson) {
    return this._http.put<IPerson>(
      this.apiUrl + '/api/persons/',
      person
    );
  }

  deleteEmployee(personCode: number) {
    return this._http.delete(this.apiUrl + '/api/persons/' + personCode);
  }
}
