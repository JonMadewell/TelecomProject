import { device } from "./device.model";
import { Plan } from "./plan.model";

export class Person {
    PersonId: number = 0;
    AccoutId: number= 0;
    plans : Plan [] = [];
    devices: device[] = [];
    FirstName: string = "";
    LastName: string = "";
    Email: string = "";
    UserName: string = "";
    Password: string = "";
}
