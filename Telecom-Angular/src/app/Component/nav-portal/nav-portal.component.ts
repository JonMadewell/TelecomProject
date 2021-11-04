import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/Services/authentication.service';

@Component({
  selector: 'app-nav-portal',
  templateUrl: './nav-portal.component.html',
  styleUrls: ['./nav-portal.component.css']
})
export class NavPortalComponent implements OnInit, OnDestroy {

  constructor(private router: Router, private _auth: AuthenticationService) { }
  ngOnDestroy(): void {
    this._auth.logOut();
  }

  ngOnInit(): void {
  }

}
