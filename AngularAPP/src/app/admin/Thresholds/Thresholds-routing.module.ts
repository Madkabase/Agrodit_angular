import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ThresholdsComponent } from './list/Thresholds.component';
import { AddEditThresholdsComponent } from './manage/Thresholds-form.component';

const routes: Routes = [
  {
    path: '',
    component: ThresholdsComponent
  },
  {
    path: 'add',
    component: AddEditThresholdsComponent
  },
  {
    path: ':Id',
    component: AddEditThresholdsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ThresholdsRoutingModule { }

