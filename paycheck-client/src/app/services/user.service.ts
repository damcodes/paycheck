import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppConfig } from '../config/config';
import { ApiBaseService } from './api-base.service';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { Helpers } from '../helpers/helpers';

@Injectable({
    providedIn: 'root'
})
export class UserService extends ApiBaseService<User> {
    private usersEndpoint = "user";

    async getAllUsers(): Promise<Observable<User[]>> {
        return this.get(this.usersEndpoint) as Promise<Observable<User[]>>;
    }

    async getUserById(id: number): Promise<Observable<User>> {
        return this.get(`${this.usersEndpoint}/${id}`) as Promise<Observable<User>>;
    }

    async authenticate(user: User): Promise<Observable<User>> {
        let userWithToken = await this.post(this.usersEndpoint + "/login", user);
        // userWithToken.subscribe(u => {
        //     this.handleError(u);
        //     console.log(u);
        // })
        return userWithToken;
    }
    // auth(data: any): any {
    //     let body = JSON.stringify(data);
    //     return this.getToken(body);
    // }

    // private getToken(body: any): Observable<any> {
    //     // return this.http.post<any>(this.apiUrl + 'token', body, super.header()).pipe(
    //     //     catchError(super.handleError)
    //     // );
    // }
}
