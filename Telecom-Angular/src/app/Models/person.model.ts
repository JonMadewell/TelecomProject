import { Account } from "./account.model";


export class Person {
    PersonId: number = 0;
    account: Account = new Account
    FirstName: string = "";
    LastName: string = "";
    Email: string = "";
    UserName: string = "";
    Password: string = "";
    authdata?: string = "";
}
