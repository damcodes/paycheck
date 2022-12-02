export class User {
    userId?: number;
    firstName?: string;
    lastName?: string;
    email?: string;
    password?: string;
    token?: string;

    constructor(email?: string,
        password?: string,
        firstName?: string,
        lastName?: string,
        userId?: number, token?: string
    ) {
        this.email = email;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.userId = userId;
        this.token = token;
    }
}
