import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationInterceptorService implements HttpInterceptor {

  constructor(private authenticationService: AuthenticationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler):Observable<HttpEvent<any>>{

    const user = this.authenticationService.userValue;
    const isLoggedIn = user && user.authdata;
    if(isLoggedIn){
      request = request.clone({
        setHeaders: {          
          Authorization: `Basic ${user.authdata}`
        }
      });
      console.log("added Authorization Header");
    }
    return next.handle(request);
  }
}
