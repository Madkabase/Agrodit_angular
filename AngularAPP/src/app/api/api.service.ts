import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import axios, { AxiosError } from 'axios';
import { ToastrService } from 'ngx-toastr';
@Injectable({
    providedIn: 'root'
})
export class APIService {

    constructor(private authService: AuthService,private toastr: ToastrService) { }
    api() {
        return axios.create({
            baseURL: 'api/',
            headers: this.authService.getHeaders(),
        })
    }
    apiUpload() {
        return axios.create({
            baseURL: 'api/',
            headers: this.authService.getHeadersUpload(),
        })
    }
    get(url: string) {
        return new Promise(async (resolve) => {
            this.api().get(url).then((res: any) => {
                resolve(res.data);
            }).catch((error) => {
                if (error.response && error.response.status==401) {
                    this.toastr.error('Session expired!');
                    location.replace('/login');
                }
                else if (axios.isAxiosError(error)) {
                    let e = error.response.data
                    console.log(e);
                    resolve({ code: 0, message: e.message });
                } else {
                    resolve({ code: 0, message: 'Some error occured' });
                }
            });
        });
    }
    post(url: string, data: any) {
        return new Promise(async (resolve) => {
            this.api().post(url, data).then((res: any) => {
                resolve(res.data);
            }).catch((error) => {
                if (error.response && error.response.status==401) {
                    this.toastr.error('Session expired!');
                    location.replace('/login');
                }
                else if (axios.isAxiosError(error)) {
                    let e = error.response.data
                    console.log(e);
                    resolve({ code: 0, message: e.message });
                } else {
                    resolve({ code: 0, message: 'Some error occured' });
                }
            });
        });
    }
    patch(url: string, data: any) {
        return new Promise(async (resolve) => {
            this.api().patch(url, data).then((res: any) => {
                resolve(res.data);
            }).catch((error) => {
                if (error.response && error.response.status==401) {
                    this.toastr.error('Session expired!');
                    location.replace('/login');
                }else if (axios.isAxiosError(error)) {
                    let e = error.response.data
                    console.log(e);
                    resolve({ code: 0, message: e.message });
                } else {
                    resolve({ code: 0, message: 'Some error occured' });
                }
            });
        });
    }
    upload(url: string, data: any) {
        return new Promise(async (resolve) => {
            this.apiUpload().post(url, data).then((res: any) => {
                resolve(res.data);
            }).catch((error) => {
                if (error.response && error.response.status==401) {
                    this.toastr.error('Session expired!');
                    location.replace('/login');
                }else if (axios.isAxiosError(error)) {
                    let e = error.response.data
                    console.log(e);
                    resolve({ code: 0, message: e.message });
                } else {
                    resolve({ code: 0, message: 'Some error occured' });
                }
            });
        });
    }
    put(url: string, data: any) {
        return new Promise(async (resolve) => {
            this.api().put(url, data).then((res: any) => {
                resolve(res.data);
            }).catch((error) => {
                if (error.response && error.response.status==401) {
                    this.toastr.error('Session expired!');
                    location.replace('/login');
                }else if (axios.isAxiosError(error)) {
                    let e = error.response.data
                    console.log(e);
                    resolve({ code: 0, message: e.message });
                } else {
                    resolve({ code: 0, message: 'Some error occured' });
                }
            });
        });
    }
    delete(url: string, data: any) {
        return new Promise(async (resolve) => {
            this.api().delete(url, data).then((res: any) => {
                resolve(res.data);
            }).catch((error) => {
                if (error.response && error.response.status==401) {
                    this.toastr.error('Session expired!');
                    location.replace('/login');
                }else if (axios.isAxiosError(error)) {
                    let e = error.response.data
                    console.log(e);
                    resolve({ code: 0, message: e.message });
                } else {
                    resolve({ code: 0, message: 'Some error occured' });
                }
            });
        });
    }
}




