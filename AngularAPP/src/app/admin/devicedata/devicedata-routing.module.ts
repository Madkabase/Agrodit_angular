import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DevicedataComponent } from './list/devicedata.component';
import { AddEditDevicedataComponent } from './manage/devicedata-form.component';

const routes: Routes = [
  {
    path: '',
    component: DevicedataComponent
  },
  {
    path: 'add',
    component: AddEditDevicedataComponent
  },
  {
    path: ':Id',
    component: AddEditDevicedataComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DevicedataRoutingModule { }

