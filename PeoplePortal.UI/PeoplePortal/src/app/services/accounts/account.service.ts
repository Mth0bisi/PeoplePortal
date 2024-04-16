import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IAccount } from 'src/app/models/account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  _http = inject(HttpClient);
  apiUrl = 'https://localhost:7276';
  constructor() {}

  getPersonAccounts(code: number) {
    console.log(code);
    return this._http.get<IAccount[]>(`${this.apiUrl + '/api/Accounts/id'}/${code}`);
  }

  getAccount(accountNumber: string):Observable<IAccount>{
    return this._http.get<IAccount>(`${this.apiUrl + '/api/Accounts/account'}/${accountNumber}`);
  }

  createPersonAccount(account: IAccount){
    return this._http.post(this.apiUrl + '/api/accounts', account);
  }

  updateAccount(account: IAccount) {
    return this._http.put<IAccount>(
      this.apiUrl + '/api/accounts/',
      account
    );
  }

}
