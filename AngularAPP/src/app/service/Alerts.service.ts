import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class AlertsService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getAlerts(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("Alerts?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("Alerts/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneAlerts(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("Alerts/"+Id);
            resolve(res);
        });
    }

    addAlerts(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("Alerts", data);
            resolve(res);
        });
    }

    updateAlerts(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("Alerts/"+Id, data);
            resolve(res);
        });
    }

    deleteAlerts(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`Alerts/${Id}`, {Id});
            resolve(res);
        });
    }
    filterAlerts(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`Alerts/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

