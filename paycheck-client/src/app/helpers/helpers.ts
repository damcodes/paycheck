import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class Helpers {

    private authenticationChanged = new Subject<boolean>();

    constructor() { }

    isAuthenticated(): boolean {
        return (!(window.localStorage['token'] === undefined ||
            window.localStorage['token'] === null ||
            window.localStorage['token'] === 'null' ||
            window.localStorage['token'] === 'undefined' ||
            window.localStorage['token'] === ''));
    }

    isAuthenticationChanged(): Observable<boolean> {
        return this.authenticationChanged.asObservable();
    }

    setLocalStorage(data: { token: string, userId: number }): void {
        // this.setStorageToken(JSON.stringify(data));
        window.localStorage.setItem('token', JSON.stringify(data.token));
        window.localStorage.setItem('userId', JSON.stringify(data.userId));
        this.authenticationChanged.next(this.isAuthenticated());
    }

    getUserId(): number {
        return JSON.parse(window.localStorage['userId']);;
    }

    // failToken(): void {
    //     this.setStorageToken(undefined);
    // }

    logout(): void {
        // this.setStorageToken(undefined);
        window.localStorage.clear();
        this.authenticationChanged.next(this.isAuthenticated());
    }

    getToken(): any {
        if (window.localStorage['token'] === undefined ||
            window.localStorage['token'] === null ||
            window.localStorage['token'] === 'null' ||
            window.localStorage['token'] === 'undefined' ||
            window.localStorage['token'] === '') {
            return '';
        }
        // debugger;
        // let obj = JSON.parse(window.localStorage['token']);
        return JSON.parse(window.localStorage['token']);
    }

    // private setStorageToken(value: any): void {
    //     Object.keys(value)
    //     window.localStorage['token'] = value;
    //     this.authenticationChanged.next(this.isAuthenticated());
    // }
}