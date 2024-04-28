import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { DevicesService } from '../../../service/devices.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-devices',
  templateUrl: './devices-form.component.html',
  styleUrls: ['./devices-form.component.scss']
})
export class AddEditDevicesComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  devicesform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private devicesService: DevicesService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.devicesform = new FormGroup({
    'Name': new FormControl(this.data.Name, [Validators.required]),
'DevEUI': new FormControl(this.data.DevEUI, [Validators.required]),
'JoinEUI': new FormControl(this.data.JoinEUI, [Validators.required]),
'AppKey': new FormControl(this.data.AppKey, [Validators.required]),
'CalibrationMoisture1Max': new FormControl(this.data.CalibrationMoisture1Max, []),
'CalibrationMoisture1Min': new FormControl(this.data.CalibrationMoisture1Min, []),
'CalibrationMoisture2Max': new FormControl(this.data.CalibrationMoisture2Max, []),
'CalibrationMoisture2Min': new FormControl(this.data.CalibrationMoisture2Min, []),
'CalibrationSalinity1Max': new FormControl(this.data.CalibrationSalinity1Max, []),
'CalibrationSalinity1Min': new FormControl(this.data.CalibrationSalinity1Min, []),
'CalibrationSalinity2Max': new FormControl(this.data.CalibrationSalinity2Max, []),
'CalibrationSalinity2Min': new FormControl(this.data.CalibrationSalinity2Min, []),
'Location': new FormControl(this.data.Location, []),
'Id': new FormControl(this.data.Id, [Validators.required]),
'Status': new FormControl(this.data.Status, []),
'CompanyId': new FormControl(this.data.CompanyId, []),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const FieldId= params['FieldId']

      if (FieldId && FieldId!='add') {
        this.devicesService.getOneDevices(FieldId).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Devices not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.devicesform.valid) {
      if (this.isEditMode) {
        //Edit
        this.devicesService.updateDevices(this.data.FieldId,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("devices");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.devicesService.addDevices(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("devices");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
