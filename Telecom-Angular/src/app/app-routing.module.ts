import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Component/home/home.component';
import { PlansComponent } from './Component/plans/plans.component';
import { PortalComponent } from './Component/portal/portal.component';
import { RegisterComponent } from './Component/register/register.component';
import { SignInComponent } from './Component/sign-in/sign-in.component';
import { PlanDetailsComponent } from './Component/plan-details/plan-details.component';


const routes: Routes = [
  {
    path: 'app-sign-in', component: SignInComponent
  },
  {
    path: 'app-plans/:planId', component:PlansComponent
  },
  {
    path: 'app-portal', component:PortalComponent
  },
  {
    path: '', component:HomeComponent
  },
  {
    path: 'app-register', component:RegisterComponent
  },
  {
    path: 'plan-details/:planId', component: PlanDetailsComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
