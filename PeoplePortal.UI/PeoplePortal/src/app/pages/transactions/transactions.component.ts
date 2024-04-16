import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ITransaction } from 'src/app/models/transaction.model';
import { TransactionServiceService } from 'src/app/services/transactions/transaction-service.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements OnInit{
  transactionsList: ITransaction[] = [];
  p: number = 1;
  total: number = 0;
  pageSize: number = 10;
  code: number = 0;

  constructor(private service: TransactionServiceService, private route: ActivatedRoute,  private router: Router) {}

  ngOnInit(): void {
    this.code = this.route.snapshot.params['code'];
    console.log(this.code);
    this.getTransactions();
  }

  getTransactions(){
    this.service.getAccountTransactions(this.code).subscribe((result) => {
      this.transactionsList = result;
      console.log(this.transactionsList);
    });
  }

  pageChangeEvent(event: number){
    this.p = event;
    this.getTransactions();
}

navigateDetails(code: number): void {
  console.log(code);
  this.router.navigateByUrl('/transaction/' + code);
}

navigateCreate(): void {
  console.log(this.code);
  this.router.navigateByUrl('/create-transaction/' + this.code);
}

goBack(): void  {
  this.router.navigateByUrl('/accounts/' + this.code);
}
}
