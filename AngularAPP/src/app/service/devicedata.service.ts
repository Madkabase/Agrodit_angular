import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class DevicedataService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getDevicedata(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("devicedata?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("devicedata/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneDevicedata(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("devicedata/"+Id);
            resolve(res);
        });
    }

    addDevicedata(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("devicedata", data);
            resolve(res);
        });
    }

    updateDevicedata(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("devicedata/"+Id, data);
            resolve(res);
        });
    }

    deleteDevicedata(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`devicedata/${Id}`, {Id});
            resolve(res);
        });
    }
    filterDevicedata(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`devicedata/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

