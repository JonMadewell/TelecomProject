import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { first } from 'rxjs/operators';
import { User } from 'src/app/Models/user.model';
import { AuthenticationService } from 'src/app/Services/authentication.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  user: User = new User;
  signInForm: FormGroup = this.fb.group({
  userName: ['',Validators.required],
  password: ['',Validators.required]
  });;
  invalidLogin = false;
  
  
  constructor(
    private fb: FormBuilder,
   private router: Router,
    private _auth: AuthenticationService
  ) { }

  ngOnInit(): void{
    
  }
  get f(){return this.signInForm.controls;}

  checkLogin(){
    (this._auth.authentiate(this.f.userName.value, this.f.password.value).subscribe(
      data => {
        this.router.navigate(['app-portal'])
        this.invalidLogin= false
      },
      error => {
        this.invalidLogin = true
      }
    )
    );
  }
  
  
}


