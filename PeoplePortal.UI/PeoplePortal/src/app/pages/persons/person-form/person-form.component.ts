import { AfterViewChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IPerson } from 'src/app/models/person.model';
import { PersonService } from 'src/app/services/persons/person.service';

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html',
  styleUrl: './person-form.component.css'
})
export class PersonFormComponent implements OnInit, AfterViewChecked{
  idNumber!: string;
  person!: IPerson;
  isEdit = false;
  personForm = this.formBuilder.group({
    code: [0, [Validators.required]],
    name: ['', []],
    surname: ['', []],
    idNumber: ['', [Validators.required]]
  });
  constructor(private http: PersonService, private route: ActivatedRoute, private router: Router,
     private readonly changeDetectorRef: ChangeDetectorRef, private formBuilder: FormBuilder, private toaster:ToastrService) {}

  ngAfterViewChecked(): void {
    this.changeDetectorRef.detectChanges();
  }
  ngOnInit(): void {

  this.idNumber = this.route.snapshot.params['id'];

  if (this.idNumber) {
    this.isEdit = true;
    this.getPerson();
  }
  }

  getPerson(){
    this.http.getPerson(this.idNumber).subscribe((result) => {
      console.log(result);
      this.personForm.patchValue(result);
    });
  }

  save() {
    console.log(this.personForm.value);
    const person: IPerson = {
      name: this.personForm.value.name!,
      surname: this.personForm.value.surname!,
      idNumber: this.personForm.value.idNumber!,
      code: this.personForm.value.code!,
    };

    if (this.isEdit) {
      this.http
        .updatePerson(person)
        .subscribe(() => {
          console.log('success');
          this.toaster.success("Record updated sucessfully.");
          this.router.navigateByUrl('/persons');
        });
    } else {
      this.http.createPerson(person).subscribe(() => {
        console.log('success');
        this.toaster.success("Record added sucessfully.");
        this.router.navigateByUrl('/persons');
      });
    }
  }
}
