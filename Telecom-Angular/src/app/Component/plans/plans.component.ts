import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Plan } from 'src/app/Models/plan.model';
import { HttpClientService } from 'src/app/Services/http-client.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {
  plans: Plan[] = [];

  

  constructor(private httpService: HttpClientService, private router: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.httpService.getPlans().subscribe((  
          response: any) => this.handleSuccessfulResponse(response),
    );
  }

  handleSuccessfulResponse(response: Plan[]){
    this.plans= response;
  }
}
