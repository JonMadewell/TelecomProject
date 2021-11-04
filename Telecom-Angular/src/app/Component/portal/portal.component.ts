import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from 'src/app/Models/person.model';
import { User } from 'src/app/Models/user.model';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { HttpClientService } from 'src/app/Services/http-client.service';

@Component({
  selector: 'app-portal',
  templateUrl: './portal.component.html',
  styleUrls: ['./portal.component.css']
})
export class PortalComponent implements OnInit {
 user: User= new User;
  constructor(private httpService: HttpClientService, private _auth:AuthenticationService) {
  }

  ngOnInit(): void {
    this.user = this._auth.userValue;
    console.log(this.user.Username)
  }
  
}
