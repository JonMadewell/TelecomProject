import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Person } from 'src/app/Models/person.model';

import { AuthenticationService } from 'src/app/Services/authentication.service';

import { HttpClientService } from 'src/app/Services/http-client.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  person: Person = new Person;
  registerForm: FormGroup = new FormGroup({
    UserName: new FormControl(null, [Validators.required]),
    Password: new FormControl(null, [Validators.required]),
    PasswordConfirm: new FormControl(null, [Validators.required]),
    FirstName: new FormControl(null, [Validators.required]),
    LastName: new FormControl(null, [Validators.required],),
    Email: new FormControl(null, [Validators.required, Validators.email])
  }
  );
  
  constructor(private fb:FormBuilder, private http: HttpClientService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.person.FirstName = this.registerForm.value.FirstName;
    this.person.LastName = this.registerForm.value.LastName;
    this.person.Email= this.registerForm.value.Email;
    this.person.UserName= this.registerForm.value.UserName;
    this.person.Password= this.registerForm.value.Password;
    if(this.registerForm.valid){
      this.http.registerPerson(this.person
      ).pipe(
        tap(() => this.router.navigate(['app-sign-in']))).subscribe();
    }
  }

  get FirstName(): FormControl{
    return this.registerForm.get('FirstName') as FormControl;
  }

  get LastName(): FormControl{
    return this.registerForm.get('LastName') as FormControl;
  }
  
  get Email(): FormControl{
    return this.registerForm.get('Email') as FormControl;
  }

  get UserName(): FormControl{
    return this.registerForm.get('UserName') as FormControl;
  }

  get Password(): FormControl{
    return this.registerForm.get('Password') as FormControl;
  }

}
