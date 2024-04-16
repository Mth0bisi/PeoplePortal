import { AfterViewChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DateAdapter } from '@angular/material/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ITransaction } from 'src/app/models/transaction.model';
import { TransactionServiceService } from 'src/app/services/transactions/transaction-service.service';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrl: './transaction-form.component.css'
})
export class TransactionFormComponent implements OnInit, AfterViewChecked{
  accountCode!: number;
  transactionCode!: number;
  transaction!: ITransaction;
  isEdit = false;
  maxDate: Date;
  transactionForm = this.formBuilder.group({
    code: [0, [Validators.required]],
    accountCode: [0, [Validators.required]],
    transactionDate: ['', [Validators.required]],
    amount: [0, [Validators.required]],
    captureDate: ['', []],
    description: ['', [Validators.required]],
    type: ['', [Validators.required]]
  });
  selectedOption: string = ''; // Variable to hold the selected option
  options = [
    { value: 'Debit', viewValue: 'Debit' },
    { value: 'Credit', viewValue: 'Credit' }
  ];

  constructor(private http: TransactionServiceService, private route: ActivatedRoute, private router: Router,
    private readonly changeDetectorRef: ChangeDetectorRef, private formBuilder: FormBuilder, private toaster:ToastrService,
    private dateAdapter: DateAdapter<Date>)
    {
      this.maxDate = new Date();
      this.dateAdapter.setLocale('en');
    }

  ngAfterViewChecked(): void {
    this.changeDetectorRef.detectChanges();
  }

  ngOnInit(): void {
    this.transactionCode = this.route.snapshot.params['transactionCode'];
    this.accountCode = this.route.snapshot.params['accountCode'];
    if (this.transactionCode) {
      this.isEdit = true;
      this.gettTransaction();
    }
  }

  gettTransaction(){
    this.http.getTransaction(this.transactionCode).subscribe((result) => {
      console.log(result);
      this.transactionForm.patchValue(result);
      this.transactionForm.controls.captureDate.disable();
    });
  }

  goBack(): void  {
    if (this.transactionCode) {
      this.router.navigateByUrl('/transactions/' + this.transactionCode);
      }
      else
      {
        this.router.navigateByUrl('/transactions/' + this.accountCode);
      }
  }

  save() {
    console.log(this.transactionForm.value);
    const transaction: ITransaction = {
      amount: this.transactionForm.value.amount!,
      transactionDate: this.transactionForm.value.transactionDate!,
      captureDate: this.transactionForm.value.captureDate!,
      accountCode: this.accountCode,
      code: this.transactionForm.value.code!,
      description: this.transactionForm.value.description!,
      type: this.transactionForm.value.type!
    };

    if (this.isEdit) {
      this.http
        .updateTransaction(transaction)
        .subscribe(() => {
          console.log('success');
          this.toaster.success("Record updated sucessfully.");
          this.router.navigateByUrl('/transactions/' + this.transactionCode);
        });
    } else {
      console.log(this.accountCode);
      this.http.createTransaction(transaction).subscribe(() => {
        console.log('success');
        this.toaster.success("Record added sucessfully.");
        this.router.navigateByUrl('/transactions/' + this.accountCode);
      });
    }
  }
}
