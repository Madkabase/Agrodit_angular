import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyusersComponent } from './list/CompanyUsers.component';
import { AddEditCompanyusersComponent } from './manage/CompanyUsers-form.component';

const routes: Routes = [
  {
    path: '',
    component: CompanyusersComponent
  },
  {
    path: 'add',
    component: AddEditCompanyusersComponent
  },
  {
    path: ':Id',
    component: AddEditCompanyusersComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyusersRoutingModule { }

