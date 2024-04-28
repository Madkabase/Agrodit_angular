import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GlobalthresholdpresetsComponent } from './list/GlobalThresholdPresets.component';
import { AddEditGlobalthresholdpresetsComponent } from './manage/GlobalThresholdPresets-form.component';

const routes: Routes = [
  {
    path: '',
    component: GlobalthresholdpresetsComponent
  },
  {
    path: 'add',
    component: AddEditGlobalthresholdpresetsComponent
  },
  {
    path: ':Id',
    component: AddEditGlobalthresholdpresetsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GlobalthresholdpresetsRoutingModule { }

