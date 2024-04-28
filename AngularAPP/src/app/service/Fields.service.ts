import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class FieldsService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getFields(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("Fields?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("Fields/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneFields(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("Fields/"+Id);
            resolve(res);
        });
    }

    addFields(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("Fields", data);
            resolve(res);
        });
    }

    updateFields(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("Fields/"+Id, data);
            resolve(res);
        });
    }

    deleteFields(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`Fields/${Id}`, {Id});
            resolve(res);
        });
    }
    filterFields(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`Fields/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

