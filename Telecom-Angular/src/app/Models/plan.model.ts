import { device } from "./device.model";

export class Plan {
    planId: number= 0;
    planName: string = "";
    deviceLimit: number = 0;
    isUnlimited: boolean= false;
    price: number = 0;
    devices: device []= [];
}
