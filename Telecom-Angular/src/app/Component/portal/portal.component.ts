import { Component, OnInit } from '@angular/core';
import { Person } from 'src/app/Models/person.model';
import { User } from 'src/app/Models/user.model';

@Component({
  selector: 'app-portal',
  templateUrl: './portal.component.html',
  styleUrls: ['./portal.component.css']
})
export class PortalComponent implements OnInit {

  constructor() { }
  person!: Person
  user!: User
  ngOnInit(): void {

  }

}
