import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class UsersService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getUsers(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("Users?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("Users/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneUsers(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("Users/"+Id);
            resolve(res);
        });
    }

    addUsers(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("Users", data);
            resolve(res);
        });
    }

    updateUsers(Id,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("Users/"+Id, data);
            resolve(res);
        });
    }

    deleteUsers(Id) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`Users/${Id}`, {Id});
            resolve(res);
        });
    }
    filterUsers(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`Users/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

