import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-left-panel',
  templateUrl: './left-panel.component.html',
  styleUrls: ['./left-panel.component.scss']
})
export class LeftPanelComponent {

    @Output() public toggleDrawer: EventEmitter<boolean> = new EventEmitter()

    toggle() {
        this.toggleDrawer.emit();
    }
}
