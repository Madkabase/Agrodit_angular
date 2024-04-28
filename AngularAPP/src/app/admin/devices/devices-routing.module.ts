import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DevicesComponent } from './list/devices.component';
import { AddEditDevicesComponent } from './manage/devices-form.component';

const routes: Routes = [
  {
    path: '',
    component: DevicesComponent
  },
  {
    path: 'add',
    component: AddEditDevicesComponent
  },
  {
    path: ':FieldId',
    component: AddEditDevicesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DevicesRoutingModule { }

