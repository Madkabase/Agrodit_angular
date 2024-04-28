import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { AlertsService } from '../../../service/Alerts.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-Alerts',
  templateUrl: './Alerts-form.component.html',
  styleUrls: ['./Alerts-form.component.scss']
})
export class AddEditAlertsComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  Alertsform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private AlertsService: AlertsService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.Alertsform = new FormGroup({
    'Date': new FormControl(this.data.Date, [Validators.required]),
'AlertType': new FormControl(this.data.AlertType, [Validators.required]),
'FieldId': new FormControl(this.data.FieldId, []),
'Closed': new FormControl(this.data.Closed, [Validators.required]),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const Id= params['Id']

      if (Id && Id!='add') {
        this.AlertsService.getOneAlerts(Id).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Alerts not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.Alertsform.valid) {
      if (this.isEditMode) {
        //Edit
        this.AlertsService.updateAlerts(this.data.Id,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Alerts");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.AlertsService.addAlerts(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Alerts");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
