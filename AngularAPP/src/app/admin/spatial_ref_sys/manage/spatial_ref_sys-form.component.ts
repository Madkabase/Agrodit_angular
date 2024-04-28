import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { Spatial_Ref_SysService } from '../../../service/spatial_ref_sys.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-spatial_ref_sys',
  templateUrl: './spatial_ref_sys-form.component.html',
  styleUrls: ['./spatial_ref_sys-form.component.scss']
})
export class AddEditSpatial_Ref_SysComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  spatial_ref_sysform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private spatial_ref_sysService: Spatial_Ref_SysService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.spatial_ref_sysform = new FormGroup({
    'auth_name': new FormControl(this.data.auth_name, []),
'auth_srid': new FormControl(this.data.auth_srid, []),
'srtext': new FormControl(this.data.srtext, []),
'proj4text': new FormControl(this.data.proj4text, []),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const srid= params['srid']

      if (srid && srid!='add') {
        this.spatial_ref_sysService.getOneSpatial_Ref_Sys(srid).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Spatial_Ref_Sys not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.spatial_ref_sysform.valid) {
      if (this.isEditMode) {
        //Edit
        this.spatial_ref_sysService.updateSpatial_Ref_Sys(this.data.srid,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("spatial_ref_sys");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.spatial_ref_sysService.addSpatial_Ref_Sys(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("spatial_ref_sys");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
