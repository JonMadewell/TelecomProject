import { Component, OnInit } from '@angular/core';
import {FormControl, FormArray, FormBuilder, Validators} from '@angular/forms'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    registerForm = this.fb.group({
      userName: ['',Validators.required],
      password: ['',Validators.required]
  
    });
  constructor(private fb:FormBuilder) { }

  ngOnInit(): void {
  }

  onSubmit(){
    console.warn(this.registerForm.value);
  }

}
