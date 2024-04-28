import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class ThresholdsService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getThresholds(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("Thresholds?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("Thresholds/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneThresholds(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("Thresholds/"+Id);
            resolve(res);
        });
    }

    addThresholds(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("Thresholds", data);
            resolve(res);
        });
    }

    updateThresholds(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("Thresholds/"+Id, data);
            resolve(res);
        });
    }

    deleteThresholds(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`Thresholds/${Id}`, {Id});
            resolve(res);
        });
    }
    filterThresholds(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`Thresholds/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

