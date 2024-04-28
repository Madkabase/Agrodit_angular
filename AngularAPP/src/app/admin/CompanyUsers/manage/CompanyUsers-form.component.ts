import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { CompanyusersService } from '../../../service/CompanyUsers.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-CompanyUsers',
  templateUrl: './CompanyUsers-form.component.html',
  styleUrls: ['./CompanyUsers-form.component.scss']
})
export class AddEditCompanyusersComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  CompanyUsersform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private CompanyUsersService: CompanyusersService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.CompanyUsersform = new FormGroup({
    'UserId': new FormControl(this.data.UserId, [Validators.required]),
'CompanyId': new FormControl(this.data.CompanyId, [Validators.required]),
'CompanyRole': new FormControl(this.data.CompanyRole, [Validators.required]),
'FieldId': new FormControl(this.data.FieldId, [Validators.required]),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const Id= params['Id']

      if (Id && Id!='add') {
        this.CompanyUsersService.getOneCompanyusers(Id).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Companyusers not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.CompanyUsersform.valid) {
      if (this.isEditMode) {
        //Edit
        this.CompanyUsersService.updateCompanyusers(this.data.Id,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("CompanyUsers");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.CompanyUsersService.addCompanyusers(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("CompanyUsers");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
