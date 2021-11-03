import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Component/home/home.component';
import { PlansComponent } from './Component/plans/plans.component';
import { PortalComponent } from './Component/portal/portal.component';
import { RegisterComponent } from './Component/register/register.component';
import { SignInComponent } from './Component/sign-in/sign-in.component';

const routes: Routes = [
  {
    path: 'app-sign-in', component: SignInComponent
  },
  {
    path: 'app-plans', component:PlansComponent
  },
  {
    path: 'app-portal', component:PortalComponent
  },
  {
    path: '', component:HomeComponent
  },
  {
    path: 'app-register', component:RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
