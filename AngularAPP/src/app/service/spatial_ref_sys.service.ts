import { Injectable } from '@angular/core';
import { APIService } from '../api/api.service';

@Injectable({
    providedIn: 'root'
})
export class Spatial_Ref_SysService {
    dialogData: any;

    constructor(private apiService: APIService) { }


    getDialogData() {
        return this.dialogData;
    }

    getSpatial_Ref_Sys(pageNo: number = 1, pageSize: number = 30, searchKey: string = "",orderBy:string=""): Promise<any> {
        return new Promise(async (resolve) => {
            if (searchKey.length === 0) {
                const res = await this.apiService.get("spatial_ref_sys?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            } else {
                const res = await this.apiService.get("spatial_ref_sys/search/" + searchKey + "/?page=" + pageNo + "&itemsPerPage=" + pageSize+"&orderBy="+orderBy);
                resolve(res);
            }
        });
    }

    getOneSpatial_Ref_Sys(srid) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.get("spatial_ref_sys/"+srid);
            resolve(res);
        });
    }

    addSpatial_Ref_Sys(data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.post("spatial_ref_sys", data);
            resolve(res);
        });
    }

    updateSpatial_Ref_Sys(srid,data) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.put("spatial_ref_sys/"+srid, data);
            resolve(res);
        });
    }

    deleteSpatial_Ref_Sys(srid) {
        return new Promise(async (resolve) => {
            const res = await this.apiService.delete(`spatial_ref_sys/${srid}`, {srid});
            resolve(res);
        });
    }
    filterSpatial_Ref_Sys(data,pageNo: number = 1, pageSize: number = 30, orAnd: string = "OR", orderBy:string=""){
        return new Promise(async (resolve) => {
            const res = await this.apiService.post(`spatial_ref_sys/filter?andOr=${orAnd}&page=${pageNo}&itemsPerPage=${pageSize}&orderBy=${orderBy}`, data);
            resolve(res);
        });
    }
}

