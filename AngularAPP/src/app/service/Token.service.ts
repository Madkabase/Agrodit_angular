import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class TokenService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }
    tokenAPICall(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("Token",data);
            resolve(res);
        });
    }

    
}


