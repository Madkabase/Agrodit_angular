import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { CompaniesService } from '../../../service/Companies.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-Companies',
  templateUrl: './Companies-form.component.html',
  styleUrls: ['./Companies-form.component.scss']
})
export class AddEditCompaniesComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  Companiesform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private CompaniesService: CompaniesService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.Companiesform = new FormGroup({
    'Name': new FormControl(this.data.Name, [Validators.required]),
'OwnerId': new FormControl(this.data.OwnerId, [Validators.required]),
'AppId': new FormControl(this.data.AppId, [Validators.required]),
'AppName': new FormControl(this.data.AppName, [Validators.required]),
'MaxDevices': new FormControl(this.data.MaxDevices, [Validators.required]),
'CompanyType': new FormControl(this.data.CompanyType, [Validators.required]),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const Id= params['Id']

      if (Id && Id!='add') {
        this.CompaniesService.getOneCompanies(Id).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Companies not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.Companiesform.valid) {
      if (this.isEditMode) {
        //Edit
        this.CompaniesService.updateCompanies(this.data.Id,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Companies");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.CompaniesService.addCompanies(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Companies");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
