import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class ThresholdpresetsService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getThresholdpresets(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("ThresholdPresets?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("ThresholdPresets/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneThresholdpresets(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("ThresholdPresets/"+Id);
            resolve(res);
        });
    }

    addThresholdpresets(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("ThresholdPresets", data);
            resolve(res);
        });
    }

    updateThresholdpresets(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("ThresholdPresets/"+Id, data);
            resolve(res);
        });
    }

    deleteThresholdpresets(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`ThresholdPresets/${Id}`, {Id});
            resolve(res);
        });
    }
    filterThresholdpresets(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`ThresholdPresets/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

