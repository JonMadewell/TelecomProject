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
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  constructor(private httpClient: HttpClient){
    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem("currentUser") || '{}'));
    this.user = this.userSubject.asObservable();
  }

  authentiate(username:string, password:string){
    return this.httpClient
    .post<any>('https://localhost:44394/api/People/Login' , {
      username,
      password
    })
    .pipe(
      map(user => {
        console.log("user info", user);
        let authData = window.btoa(username + ":" + password);
        console.log("user info", authData);

        localStorage.setItem("currentUser", JSON.stringify(user))


        return user;
      })
    )
  }

  public get userValue(): User{
    return this.userSubject.value;
  }

  isUserLoggedIn(){
    let user = localStorage.getItem('currentUser')
    console.log(!(user== null))
    return !(user === null)
  }

  logOut(){
    localStorage.removeItem('currentUser')
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