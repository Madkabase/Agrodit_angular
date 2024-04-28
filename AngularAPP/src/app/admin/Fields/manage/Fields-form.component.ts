import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { FieldsService } from '../../../service/Fields.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-Fields',
  templateUrl: './Fields-form.component.html',
  styleUrls: ['./Fields-form.component.scss']
})
export class AddEditFieldsComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  Fieldsform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private FieldsService: FieldsService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.Fieldsform = new FormGroup({
    'Name': new FormControl(this.data.Name, [Validators.required]),
'CompanyId': new FormControl(this.data.CompanyId, [Validators.required]),
'Geofence': new FormControl(this.data.Geofence, []),
'ThresholdId': new FormControl(this.data.ThresholdId, []),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const Id= params['Id']

      if (Id && Id!='add') {
        this.FieldsService.getOneFields(Id).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Fields not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.Fieldsform.valid) {
      if (this.isEditMode) {
        //Edit
        this.FieldsService.updateFields(this.data.Id,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Fields");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.FieldsService.addFields(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Fields");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
