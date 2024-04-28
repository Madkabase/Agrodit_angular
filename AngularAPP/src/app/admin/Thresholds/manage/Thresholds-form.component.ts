import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { ThresholdsService } from '../../../service/Thresholds.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-Thresholds',
  templateUrl: './Thresholds-form.component.html',
  styleUrls: ['./Thresholds-form.component.scss']
})
export class AddEditThresholdsComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  Thresholdsform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private ThresholdsService: ThresholdsService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.Thresholdsform = new FormGroup({
    'Moisture1Min': new FormControl(this.data.Moisture1Min, [Validators.required]),
'Moisture1Max': new FormControl(this.data.Moisture1Max, [Validators.required]),
'Moisture2Min': new FormControl(this.data.Moisture2Min, [Validators.required]),
'Moisture2Max': new FormControl(this.data.Moisture2Max, [Validators.required]),
'Temperature1Min': new FormControl(this.data.Temperature1Min, [Validators.required]),
'Temperature1Max': new FormControl(this.data.Temperature1Max, [Validators.required]),
'MainSensor': new FormControl(this.data.MainSensor, [Validators.required]),
'Temperature2Max': new FormControl(this.data.Temperature2Max, [Validators.required]),
'Temperature2Min': new FormControl(this.data.Temperature2Min, [Validators.required]),
'Salinity1Max': new FormControl(this.data.Salinity1Max, [Validators.required]),
'Salinity1Min': new FormControl(this.data.Salinity1Min, [Validators.required]),
'Salinity2Max': new FormControl(this.data.Salinity2Max, [Validators.required]),
'Salinity2Min': new FormControl(this.data.Salinity2Min, [Validators.required]),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const Id= params['Id']

      if (Id && Id!='add') {
        this.ThresholdsService.getOneThresholds(Id).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Thresholds not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.Thresholdsform.valid) {
      if (this.isEditMode) {
        //Edit
        this.ThresholdsService.updateThresholds(this.data.Id,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Thresholds");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.ThresholdsService.addThresholds(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Thresholds");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
