import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Helpers } from '../../helpers/helpers';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { catchError, map } from 'rxjs';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent {

    public email = new FormControl('');
    public password = new FormControl('');
    @Input() public authenticated?: boolean = this.helpers.isAuthenticated();

    constructor(private helpers: Helpers, private router: Router, private userService: UserService) { }

    async login(e: Event) {
        e.preventDefault();
        let loginAttempt: User = new User(this.email.value || undefined, this.password.value || undefined);
        console.log(`Submitting login: ${loginAttempt}`);
        let userWithToken = await this.userService.authenticate(loginAttempt);
        userWithToken.pipe(
            map(u => new User(u.email, undefined, u.firstName, u.lastName, u.userId, u.token))
        ).subscribe(u => {
            console.log(u);
            this.helpers.setLocalStorage({token: u.token!, userId: u.userId! });
            this.router.navigate(['/dashboard']);
        })
    }
} 
