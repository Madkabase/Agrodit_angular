import { Injectable } from '@angular/core';
import { TokenService } from '../service/Token.Service';

import axios from 'axios';
@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private tokenService: TokenService) { }

    setToken(key, value) {
        localStorage.setItem(key, value);
    }

    generateToken(username: string, password: string): Promise<any> {
        return new Promise(async (resolve, reject) => {
            try {
                let item = {
                    "username": username,
                    "password": password
                };
                await this.tokenService.tokenAPICall(JSON.stringify(item)).then((res: any) => {
                    if (res && res.code==1  && res.document) {
                        const tokenData = res.document;
                        let token = tokenData.AccessToken;
                        let expiryDate = tokenData.ValidTo;
                        this.setToken('token', token);
                        this.setToken('token_exp', expiryDate);
                        resolve({ code: 1,response:res });
                    }else{
                        resolve({ code: 0,response:res });
                    }
                });
            } catch (error: any) {
                if (axios.isAxiosError(error)) {
                    resolve({ code: 0 ,response:error});
                } else {
                    resolve({ code: 0, message: 'Some error occured' });
                }
            }
        });
    }
    getHeaders() {
        let token = 'Bearer ' + localStorage.getItem('token');
        return {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json',
            'Authorization': token
        };
    }
    
    getHeadersUpload() {
        let token = 'Bearer ' + localStorage.getItem('token');
        return {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'multipart/form-data',
            'Authorization': token
        };
    }

}
