import { Component, EventEmitter, Output } from '@angular/core';
import { Helpers } from '../helpers/helpers';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
    public title: string = 'Paycheck';
    @Output() toggleDrawer: EventEmitter<boolean> = new EventEmitter();
    public authenticated: boolean = this.helpers.isAuthenticated();

    constructor(private helpers: Helpers) {}

    toggle() {
        this.toggleDrawer.emit();
    }
}
