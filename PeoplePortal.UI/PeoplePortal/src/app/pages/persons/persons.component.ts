import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IPerson } from 'src/app/models/person.model';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-persons',
  templateUrl: './persons.component.html',
  styleUrl: './persons.component.css'
})
export class PersonsComponent implements OnInit{
  personsList: IPerson[] = [];
  p: number = 1;
  total: number = 0;
  pageSize: number = 10;
  code: number = 0;
  username: string = '';

  constructor(private http: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.username = localStorage.getItem('username') ?? '';
    this.getPersons();

      console.log( this.username);
  }

 getPersons(){
    this.http.getAllPersons().subscribe((result) => {
      this.personsList = result;
      console.log(this.personsList);
    });
  }

  navigateDetails(idNumber: string): void {
    console.log(idNumber);
    this.router.navigateByUrl('/person/' + idNumber);
  }

  navigateAccounts(code: number): void {
    console.log(code);
    this.router.navigateByUrl('/accounts/' + code);
  }

  navigateLogin(): void {
    this.router.navigateByUrl('/login');
  }
  pageChangeEvent(event: number){
    this.p = event;
    this.getPersons();
}
}
