import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { User } from '../Models/user.model';
import { Plan } from '../Models/plan.model';
import { device } from '../Models/device.model';
import { Person } from '../Models/person.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {

  constructor( private httpClient: HttpClient) { }

  registerPerson(Person: Person):Observable<Person>{
    return this.httpClient.post<Person>("https://localhost:44394/api/People/", Person,)
  }

  getUser( UserName: string, Password: string){
    
      const headers = new HttpHeaders({Authorization: 'basic' + btoa(UserName + ':' + Password)});
      return this.httpClient.post<User>("https://localhost:44394/api/People/Login", {headers})
  }

  getPlans(): Observable<Plan[]>{
    return this.httpClient.get<Plan[]>("https://localhost:44394/api/Plans");
  }

  getPlan(planId: number): Observable<Plan>{
    return this.httpClient.get<Plan>("https://localhost:44394/api/Plans" + `/${planId}`);
  }

  getDevices(){
    return this.httpClient.get<device>("https://localhost:44394/api/Devices");
  }

  getPhone(PhoneId: number){
    return this.httpClient.get<Plan>("https://localhost:44394/api//Devices");
  }
}
