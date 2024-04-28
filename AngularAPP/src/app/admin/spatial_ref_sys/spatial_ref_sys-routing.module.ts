import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Spatial_Ref_SysComponent } from './list/spatial_ref_sys.component';
import { AddEditSpatial_Ref_SysComponent } from './manage/spatial_ref_sys-form.component';

const routes: Routes = [
  {
    path: '',
    component: Spatial_Ref_SysComponent
  },
  {
    path: 'add',
    component: AddEditSpatial_Ref_SysComponent
  },
  {
    path: ':srid',
    component: AddEditSpatial_Ref_SysComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class Spatial_Ref_SysRoutingModule { }

