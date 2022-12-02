import { Component, OnInit } from '@angular/core';
import { Helpers } from 'src/app/helpers/helpers';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { map } from 'rxjs';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {

    public user?: User;

    constructor(private usersService: UserService, private helpers: Helpers) {}

    ngOnInit(): void {
        this.getUser();
    }

    async getUser() {
        let user = await this.usersService.getUserById(this.helpers.getUserId());
        user.pipe(
            map(u => new User(u.email, undefined, u.firstName, u.lastName, u.userId))
        ).subscribe(u => {
            this.user = u;
            console.log(this.user);
        })
    }
}
