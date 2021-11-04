import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { Person } from 'src/app/Models/person.model';
import { Plan } from 'src/app/Models/plan.model';
import { User } from 'src/app/Models/user.model';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { HttpClientService } from 'src/app/Services/http-client.service';

@Component({
  selector: 'app-portal',
  templateUrl: './portal.component.html',
  styleUrls: ['./portal.component.css']
})
export class PortalComponent implements OnInit{
 error: string= "";
 subscription: Subscription = new Subscription;
 person: Person = new Person;
 plans: Plan []= []
  constructor(private _auth: AuthenticationService) {
   this.person= this._auth.userValue
    
  }

  ngOnInit(): void { 
    var user = JSON.parse(JSON.stringify(this.person));
    this.person.FirstName = user.firstName;
    this.person.LastName= user.lastName;
    this.person.Email = user.email;
    this.person.account.accountId= user.account.accountId;
    this.person.account.personId = user.account.personId;
    this.plans= user.account.plans
  }
    

  
  
}
