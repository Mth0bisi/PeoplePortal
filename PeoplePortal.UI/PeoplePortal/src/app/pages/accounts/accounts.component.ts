import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IAccount } from 'src/app/models/account.model';
import { AccountService } from 'src/app/services/accounts/account.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrl: './accounts.component.css'
})

export class AccountsComponent implements OnInit{
  accountsList: IAccount[] = [];
  p: number = 1;
  total: number = 0;
  pageSize: number = 10;
  code: number = 0;

  constructor(private service: AccountService, private route: ActivatedRoute,  private router: Router) {}

  ngOnInit(): void {
    this.code = this.route.snapshot.params['code'];
    console.log(this.code);
    this.getPersonAccounts();
  }

  getPersonAccounts(){
    this.service.getPersonAccounts(this.code).subscribe((result) => {
      this.accountsList = result;
      console.log(this.accountsList);
    });
  }

  pageChangeEvent(event: number){
    this.p = event;
    this.getPersonAccounts();
}

navigateDetails(accountNumber: string): void {
  console.log(accountNumber);
  this.router.navigateByUrl('/account/' + accountNumber);
}

navigateTransactions(code: number): void {
  console.log(code);
  this.router.navigateByUrl('/transactions/' + code);
}

navigateCreate(): void {
  console.log(this.code);
  this.router.navigateByUrl('/create-account/' + this.code);
}

goBack(): void  {
  this.router.navigateByUrl('/persons');
}
}
