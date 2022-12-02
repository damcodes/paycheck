import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class AppConfig {

    private _config: { [key: string]: string };

    constructor() {
        this._config = {
            apiBaseUrl: "http://localhost:5000/api/"
        }
    }

    get setting():{ [key: string]: string } {
        return this._config;
    }
    
    get(key: any) {
        return this._config[key];
    }
}
