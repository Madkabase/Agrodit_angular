import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AlertsComponent } from './list/Alerts.component';
import { AddEditAlertsComponent } from './manage/Alerts-form.component';

const routes: Routes = [
  {
    path: '',
    component: AlertsComponent
  },
  {
    path: 'add',
    component: AddEditAlertsComponent
  },
  {
    path: ':Id',
    component: AddEditAlertsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AlertsRoutingModule { }

