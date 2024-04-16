import { AfterViewChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IAccount } from 'src/app/models/account.model';
import { AccountService } from 'src/app/services/accounts/account.service';

@Component({
  selector: 'app-account-form',
  templateUrl: './account-form.component.html',
  styleUrl: './account-form.component.css'
})
export class AccountFormComponent implements OnInit, AfterViewChecked{
  accountNumber: string = "";
  code: number = 0;
  account!: IAccount;
  isEdit = false;
  selectedOption: string = ''; // Variable to hold the selected option
  options = [
    { value: 'Open', viewValue: 'Open' }
  ];
  accountForm = this.formBuilder.group({
    code: [0, [Validators.required]],
    personCode: [0, [Validators.required]],
    accountNumber: ['', [Validators.required]],
    outstandingBalance: [0, [Validators.required]],
    accountStatus: ['', [Validators.required]]
  });

  constructor(private http: AccountService, private route: ActivatedRoute, private router: Router,
     private readonly changeDetectorRef: ChangeDetectorRef, private formBuilder: FormBuilder, private toaster:ToastrService) {}


  ngOnInit(): void {
    this.accountNumber = this.route.snapshot.params['accountNumber'];
    this.code = this.route.snapshot.params['code'];
    console.log(this.code);
    if (this.accountNumber) {
      this.isEdit = true;
      this.getAccount();
    }
  }
  ngAfterViewChecked(): void {
    this.changeDetectorRef.detectChanges();
  }

  getAccount(){
    this.http.getAccount(this.accountNumber).subscribe((result) => {
      console.log(result);
      this.accountForm.patchValue(result);
      if( this.accountForm.value.outstandingBalance == 0)
        {
          this.options = [
            { value: 'Open', viewValue: 'Open' },
            { value: 'Closed', viewValue: 'Close' },
          ];
        }
      this.accountForm.controls.outstandingBalance.disable();
    });
  }

  goBack(): void  {
    if (this.accountNumber) {
    this.router.navigateByUrl('/accounts/' + this.accountForm.value.personCode);
    }
    else
    {
      this.router.navigateByUrl('/accounts/' + this.code);
    }
  }

  save() {
    console.log(this.accountForm.value);
    const account: IAccount = {
      accountStatus: this.accountForm.value.accountStatus!,
      outstandingBalance: this.accountForm.value.outstandingBalance!,
      accountNumber: this.accountForm.value.accountNumber!,
      personCode: this.accountForm.value.personCode!,
      code: this.accountForm.value.code!,
    };
    if (this.isEdit) {
      this.http
        .updateAccount(account)
        .subscribe(() => {
          console.log('success');
          this.toaster.success("Record updated sucessfully.");
          this.router.navigateByUrl('/accounts/' + this.account.personCode);
        });
    } else {
      this.http.createPersonAccount(account).subscribe(() => {
        console.log('success');
        this.toaster.success("Record added sucessfully.");
        this.router.navigateByUrl('/accounts/' + this.account.personCode);
      });
    }
  }
}
