import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Subscription, startWith, delay } from 'rxjs';
import { Helpers } from '../helpers/helpers';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterViewInit {

    public subscription: Subscription | null = null;
    public authenticated: boolean = false;

    constructor(private helpers: Helpers) { }

    ngAfterViewInit() {
        this.subscription = this.helpers.isAuthenticationChanged()
            .pipe(
                startWith(this.helpers.isAuthenticated()),
                delay(0)
            ).subscribe((value: any) => this.authenticated = value);
    }

    ngOnDestroy() {
        if (this.subscription) this.subscription.unsubscribe();
    }
}
