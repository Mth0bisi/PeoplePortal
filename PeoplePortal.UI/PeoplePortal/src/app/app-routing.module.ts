import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './pages/about/about.component';
import { AccountFormComponent } from './pages/accounts/account-form/account-form.component';
import { AccountsComponent } from './pages/accounts/accounts.component';
import { ContactComponent } from './pages/contact/contact.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { PersonFormComponent } from './pages/persons/person-form/person-form.component';
import { PersonsComponent } from './pages/persons/persons.component';
import { TransactionFormComponent } from './pages/transactions/transaction-form/transaction-form.component';
import { TransactionsComponent } from './pages/transactions/transactions.component';

const routes: Routes = [
  { path: '', component: HomeComponent },

  { path: 'home', component: HomeComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'about', component: AboutComponent },
  { path: 'accounts/:code', component: AccountsComponent },
  { path: 'create-account/:code', component: AccountFormComponent},
  { path: 'account/:accountNumber', component: AccountFormComponent},
  { path: 'transactions/:code', component: TransactionsComponent},
  { path: 'create-transaction/:accountCode', component: TransactionFormComponent},
  { path: 'transaction/:transactionCode', component: TransactionFormComponent},
  { path: 'create-person', component: PersonFormComponent },
  { path: 'person/:id', component: PersonFormComponent },
  { path: 'persons',
  children:[
      {path: '', component: PersonsComponent}
  ]},
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
