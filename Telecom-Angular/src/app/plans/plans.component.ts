import { Component, OnInit } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Plan } from '../plan.model';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {
  plans: Plan[] = [];

  constructor(private httpService: HttpClientService) { }

  ngOnInit(): void {
    this.httpService.getPlans().subscribe(
      (      response: any) => this.handleSuccessfulResponse(response),
    );
  }

  handleSuccessfulResponse(response: Plan[]){
    this.plans= response;
  }

}
