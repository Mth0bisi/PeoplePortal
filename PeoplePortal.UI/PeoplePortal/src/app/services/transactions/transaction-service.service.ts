import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ITransaction } from 'src/app/models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionServiceService {
  _http = inject(HttpClient);
  apiUrl = 'https://localhost:7276';
  constructor() {}

  getAccountTransactions(code: number) {
    return this._http.get<ITransaction[]>(`${this.apiUrl + '/api/Transactions/id'}/${code}`);
  }

  getTransaction(code: number):Observable<ITransaction>{
    return this._http.get<ITransaction>(`${this.apiUrl + '/api/Transactions/account'}/${code}`);
  }

  createTransaction(transaction: ITransaction){
    return this._http.post(this.apiUrl + '/api/Transactions', transaction);
  }

  updateTransaction(transaction: ITransaction) {
    return this._http.put<ITransaction>(
      this.apiUrl + '/api/Transactions/',
      transaction
    );
  }
}
