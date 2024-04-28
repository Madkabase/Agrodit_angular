import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class GlobalthresholdpresetsService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getGlobalthresholdpresets(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("GlobalThresholdPresets?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("GlobalThresholdPresets/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneGlobalthresholdpresets(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("GlobalThresholdPresets/"+Id);
            resolve(res);
        });
    }

    addGlobalthresholdpresets(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("GlobalThresholdPresets", data);
            resolve(res);
        });
    }

    updateGlobalthresholdpresets(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("GlobalThresholdPresets/"+Id, data);
            resolve(res);
        });
    }

    deleteGlobalthresholdpresets(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`GlobalThresholdPresets/${Id}`, {Id});
            resolve(res);
        });
    }
    filterGlobalthresholdpresets(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`GlobalThresholdPresets/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

