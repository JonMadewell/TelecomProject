import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getSourceFileOrError } from '@angular/compiler-cli/src/ngtsc/file_system';
import { User } from './user.model';
import { Plan } from './plan.model';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {

  constructor( private httpClient: HttpClient) { }


  getUser( UserName: string, Password: string){
    
      const headers = new HttpHeaders({Authorization: 'basic' + btoa(UserName + ':' + Password)});
      return this.httpClient.post<User>("https://localhost:44394/api/People/Login", {headers})
  }

  getPlans(){
    return this.httpClient.get<Plan>("https://localhost:44394/api/Plans");
  }
}
