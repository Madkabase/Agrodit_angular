import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { DevicedataService } from '../../../service/devicedata.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-devicedata',
  templateUrl: './devicedata-form.component.html',
  styleUrls: ['./devicedata-form.component.scss']
})
export class AddEditDevicedataComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  devicedataform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private devicedataService: DevicedataService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.devicedataform = new FormGroup({
    'Moisture1': new FormControl(this.data.Moisture1, [Validators.required]),
'Moisture2': new FormControl(this.data.Moisture2, [Validators.required]),
'BatteryLevel': new FormControl(this.data.BatteryLevel, []),
'Temperature1': new FormControl(this.data.Temperature1, [Validators.required]),
'TimeStamp': new FormControl(this.data.TimeStamp, [Validators.required]),
'Temperature2': new FormControl(this.data.Temperature2, [Validators.required]),
'Salinity1': new FormControl(this.data.Salinity1, [Validators.required]),
'Salinity2': new FormControl(this.data.Salinity2, [Validators.required]),
'DeviceId': new FormControl(this.data.DeviceId, []),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const Id= params['Id']

      if (Id && Id!='add') {
        this.devicedataService.getOneDevicedata(Id).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Devicedata not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.devicedataform.valid) {
      if (this.isEditMode) {
        //Edit
        this.devicedataService.updateDevicedata(this.data.Id,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("devicedata");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.devicedataService.addDevicedata(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("devicedata");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
