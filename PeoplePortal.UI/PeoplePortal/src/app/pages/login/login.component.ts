import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IUser } from 'src/app/models/user.model';
import { PersonService } from 'src/app/services/persons/person.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  user!: IUser;
  loginForm = this.builder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });
  constructor(private service: PersonService, private router: Router, private builder: FormBuilder) {}

  login() {
    const username = this.loginForm.value.username!;
    const password = this.loginForm.value.password!;

    this.service.getAllPersons(username, password).subscribe((result) => {
      console.log(result);
      localStorage.setItem('username', this.user.username);
      this.router.navigateByUrl('/persons');
    });
}
}
