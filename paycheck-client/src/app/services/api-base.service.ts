import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { catchError, Observable, tap, throwError, firstValueFrom } from 'rxjs';
import { AppConfig } from '../config/config';
import { Helpers } from '../helpers/helpers';

@Injectable({
    providedIn: 'root'
})
export class ApiBaseService<T> {

    // private httpHeaders = {
    //     headers: new HttpHeaders({
    //         'Content-Type': 'application/json',
    //     })
    // };
    private apiUrl: string = this.config.setting["apiBaseUrl"];

    constructor(private http: HttpClient, private config: AppConfig, protected helpers: Helpers) { }

    public async get(endpoint: string): Promise<Observable<T> | Observable<T[]>> {
        return this.http.get<T>(this.apiUrl + endpoint, this.header());
    }

    public async post(endpoint: string, data: any) {
        return this.http.post<T>(this.apiUrl + endpoint, data, this.header());
    }

    public async patch(endpoint: string, data: any) {

    }

    public async destroy(endpoint: string, data: any) {

    }

    public extractData(res: Response) {
        let body = res.json();
        return body || {};
    }

    public header() {
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        if (this.helpers.isAuthenticated()) {
            headers = headers.append('Authorization', 'Bearer ' + this.helpers.getToken());
        }
        return { headers };
    }

    // public setToken(data: any) {
    //     this.helpers.setToken(data);
    // }

    // public failToken(error: Response | any) {
    //     this.helpers.failToken();
    //     return this.handleError(Response);
    // }

    public handleError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body || JSON.stringify(body);
            errMsg = `${error.status}---${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return catchError(() => errMsg);
    }
}
