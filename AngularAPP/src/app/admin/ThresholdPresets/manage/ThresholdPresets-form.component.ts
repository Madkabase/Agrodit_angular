import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { ThresholdpresetsService } from '../../../service/ThresholdPresets.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-ThresholdPresets',
  templateUrl: './ThresholdPresets-form.component.html',
  styleUrls: ['./ThresholdPresets-form.component.scss']
})
export class AddEditThresholdpresetsComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  ThresholdPresetsform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private ThresholdPresetsService: ThresholdpresetsService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.ThresholdPresetsform = new FormGroup({
    'Name': new FormControl(this.data.Name, [Validators.required]),
'CompanyId': new FormControl(this.data.CompanyId, [Validators.required]),
'Moisture1Min': new FormControl(this.data.Moisture1Min, [Validators.required]),
'Moisture1Max': new FormControl(this.data.Moisture1Max, [Validators.required]),
'Moisture2Min': new FormControl(this.data.Moisture2Min, [Validators.required]),
'Moisture2Max': new FormControl(this.data.Moisture2Max, [Validators.required]),
'Temperature1Min': new FormControl(this.data.Temperature1Min, [Validators.required]),
'Temperature1Max': new FormControl(this.data.Temperature1Max, [Validators.required]),
'Temperature2Max': new FormControl(this.data.Temperature2Max, [Validators.required]),
'Temperature2Min': new FormControl(this.data.Temperature2Min, [Validators.required]),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const Id= params['Id']

      if (Id && Id!='add') {
        this.ThresholdPresetsService.getOneThresholdpresets(Id).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Thresholdpresets not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.ThresholdPresetsform.valid) {
      if (this.isEditMode) {
        //Edit
        this.ThresholdPresetsService.updateThresholdpresets(this.data.Id,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("ThresholdPresets");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.ThresholdPresetsService.addThresholdpresets(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("ThresholdPresets");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
