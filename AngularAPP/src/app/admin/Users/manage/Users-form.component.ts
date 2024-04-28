import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ThemePalette } from '@angular/material/core';
import { UsersService } from '../../../service/Users.service';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-Users',
  templateUrl: './Users-form.component.html',
  styleUrls: ['./Users-form.component.scss']
})
export class AddEditUsersComponent implements OnInit {

  isEditMode: boolean = false;
  
  color: ThemePalette = 'accent';
  data: any;
  formError: any[] = [];
  Usersform: FormGroup;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private UsersService: UsersService,
    
    private toastr: ToastrService
  ) {
    this.data = {};
    
    this.Usersform = new FormGroup({
    'FirstName': new FormControl(this.data.FirstName, [Validators.required]),
'LastName': new FormControl(this.data.LastName, [Validators.required]),
'Email': new FormControl(this.data.Email, [Validators.required]),
'Password': new FormControl(this.data.Password, [Validators.required]),
'IsVerified': new FormControl(this.data.IsVerified, [Validators.required]),
'ConfirmationCode': new FormControl(this.data.ConfirmationCode, [Validators.required]),
'ConfirmationExpirationDate': new FormControl(this.data.ConfirmationExpirationDate, [Validators.required]),
'ConfirmationTriesCounter': new FormControl(this.data.ConfirmationTriesCounter, [Validators.required]),

    });
   
  }

  ngOnInit() {
    
    this.activatedRoute.params.subscribe(params => {
     const Id= params['Id']

      if (Id && Id!='add') {
        this.UsersService.getOneUsers(Id).then((res: any) => {
          if (res.code === 1) {
            this.isEditMode = true;
            this.data = res.document;
          } else {
            this.isEditMode = false;
            this.toastr.error("Users not found for edit");
          }
        })
      }
    });
  }

  public confirmSubmit(): void {
    if (this.Usersform.valid) {
      if (this.isEditMode) {
        //Edit
        this.UsersService.updateUsers(this.data.Id,this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Users");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
      else {
        //Add
        this.UsersService.addUsers(this.data).then((res: any) => {
          if (res.code === 1) {
            this.toastr.success(res.message);
            this.router.navigateByUrl("Users");
          } else {
            this.toastr.error(res.message);
          }
        });
      }
    }
  }

}
