import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompaniesComponent } from './list/Companies.component';
import { AddEditCompaniesComponent } from './manage/Companies-form.component';

const routes: Routes = [
  {
    path: '',
    component: CompaniesComponent
  },
  {
    path: 'add',
    component: AddEditCompaniesComponent
  },
  {
    path: ':Id',
    component: AddEditCompaniesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompaniesRoutingModule { }

