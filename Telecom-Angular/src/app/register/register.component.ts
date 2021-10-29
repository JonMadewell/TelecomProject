// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, Validators } from '@angular/forms';
// import { AuthenticationService } from '../authentication.service';
// import { Person } from '../person.model';

// @Component({
//   selector: 'app-register',
//   templateUrl: './register.component.html',
//   styleUrls: ['./register.component.css']
// })
// export class RegisterComponent implements OnInit {
//   registerForm = this.fb.group({
//     UserName: ['',Validators.required],
//     Password: ['',Validators.required],
//     FirstName: ['',Validators.required],
//     LastName: ['', Validators.required],
//     Email: ['', Validators.required]
//   });
//   loading = false;
//   submitted = false;
//   returnUrl: string= '';
//   error = '';
  
//   registerData:Person = {
//     PersonId: 0,
//     FirstName: '',
//     LastName: '',
//     Email: '',
//     UserName: '',
//     Password: ''
//   }

//   constructor(private fb:FormBuilder, private _auth: AuthenticationService) { }

//   ngOnInit(): void {
//   }

//   onSubmit(){
//     console.warn(this.registerForm.value);
//   }

//   registerUser(){
//     this._auth.registerUser(this.registerData)
//     .subscribe(
//       // res => console.log(res),
//       // err => console.log(err)
//     )
//   }

// }
