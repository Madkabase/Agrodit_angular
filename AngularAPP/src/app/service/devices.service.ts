import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class DevicesService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getDevices(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("devices?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("devices/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneDevices(FieldId) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("devices/"+FieldId);
            resolve(res);
        });
    }

    addDevices(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("devices", data);
            resolve(res);
        });
    }

    updateDevices(FieldId,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("devices/"+FieldId, data);
            resolve(res);
        });
    }

    deleteDevices(FieldId) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`devices/${FieldId}`, {FieldId});
            resolve(res);
        });
    }
    filterDevices(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`devices/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

