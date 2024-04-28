import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ThresholdpresetsComponent } from './list/ThresholdPresets.component';
import { AddEditThresholdpresetsComponent } from './manage/ThresholdPresets-form.component';

const routes: Routes = [
  {
    path: '',
    component: ThresholdpresetsComponent
  },
  {
    path: 'add',
    component: AddEditThresholdpresetsComponent
  },
  {
    path: ':Id',
    component: AddEditThresholdpresetsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ThresholdpresetsRoutingModule { }

