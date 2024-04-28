import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class CompaniesService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getCompanies(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("Companies?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("Companies/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneCompanies(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("Companies/"+Id);
            resolve(res);
        });
    }

    addCompanies(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("Companies", data);
            resolve(res);
        });
    }

    updateCompanies(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("Companies/"+Id, data);
            resolve(res);
        });
    }

    deleteCompanies(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`Companies/${Id}`, {Id});
            resolve(res);
        });
    }
    filterCompanies(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`Companies/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

