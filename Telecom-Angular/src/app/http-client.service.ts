import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getSourceFileOrError } from '@angular/compiler-cli/src/ngtsc/file_system';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {

  constructor( private httpClient: HttpClient) { }


  getUser( UserName: string, Password: string){
    
      const headers = new HttpHeaders({Authorization: 'basic' + btoa(UserName + ':' + Password)});
      return this.httpClient.get<User>('https://localhost:44394/api/People/getPeople', {headers})
  }
}
