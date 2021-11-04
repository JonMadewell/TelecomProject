import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Plan } from '../../Models/plan.model';
import { HttpClientService } from 'src/app/Services/http-client.service';


@Component({
  selector: 'app-plan-details',
  templateUrl: './plan-details.component.html',
  styleUrls: ['./plan-details.component.css']
})
export class PlanDetailsComponent implements OnInit {

  planId: any;
  plan!: Plan;

  constructor(private httpClientService: HttpClientService, private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activeRoute.data.subscribe(id => {
      this.planId = id;
      this.httpClientService.getPlan(this.planId).subscribe(data => {
        this.plan = data;
      });
    });
  }

}
