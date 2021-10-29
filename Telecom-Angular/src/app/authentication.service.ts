import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'src/app/user.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private httpClient: HttpClient){}

  authentiate(username:string, password:string){
    const headers = new HttpHeaders ({Authorization: 'Basic ' + btoa(username + ':' + password)})
    return this.httpClient.post<User>('https://localhost:44394/api/People/Login', {headers}).pipe(
      map(
        userData =>{
          sessionStorage.setItem('username', username)
          return userData;
        }
      )
    );
  }

  isUserLoggedIn(){
    let user = sessionStorage.getItem('username')
    console.log(!(user== null))
    return !(user === null)
  }

  logOut(){
    sessionStorage.removeItem('username')
  }


}







// private currentUserSubject: BehaviorSubject<User>;
//   public currentUser: Observable<User>
//   registerUser: any;
//   constructor(private http: HttpClient )
//   { 
//     this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')|| '{}'));
//     this.currentUser = this.currentUserSubject.asObservable();
//   }

//   public get currentUserValue(): User{
//     return this.currentUserSubject.value;
//   }

// login(username: string, password: string){
//     return this.http.post<any>('${envirment.apiURL}/person/authenticate', {username, password})
//     .pipe(map(User => {
//       User.authdate = window.btoa(username + ':' + password);
//       localStorage.setItem('currentUser', JSON.stringify(User));
//       this.currentUserSubject.next(User);
//       return User;
//     }));
//   }

//   logout(){
//     localStorage.removeItem('currentUser');
//     this.currentUserSubject= new BehaviorSubject<User>(JSON.parse('{}'));

//   }