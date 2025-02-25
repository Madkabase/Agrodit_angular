import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class UploadService {
    dialogData: any;

    constructor(private apiService: APIService) { }

    uploadFile(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.upload("/Upload/upload",data);
            resolve(res);
        });
    }
    uploadImage(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.upload("/Upload/upload",data);
            resolve(res);
        });
    }
}


