import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class CompanyusersService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getCompanyusers(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("CompanyUsers?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("CompanyUsers/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneCompanyusers(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("CompanyUsers/"+Id);
            resolve(res);
        });
    }

    addCompanyusers(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("CompanyUsers", data);
            resolve(res);
        });
    }

    updateCompanyusers(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("CompanyUsers/"+Id, data);
            resolve(res);
        });
    }

    deleteCompanyusers(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`CompanyUsers/${Id}`, {Id});
            resolve(res);
        });
    }
    filterCompanyusers(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`CompanyUsers/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

