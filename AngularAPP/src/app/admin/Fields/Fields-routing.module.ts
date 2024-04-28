import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FieldsComponent } from './list/Fields.component';
import { AddEditFieldsComponent } from './manage/Fields-form.component';

const routes: Routes = [
  {
    path: '',
    component: FieldsComponent
  },
  {
    path: 'add',
    component: AddEditFieldsComponent
  },
  {
    path: ':Id',
    component: AddEditFieldsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FieldsRoutingModule { }

