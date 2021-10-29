import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlansComponent } from './plans/plans.component';
import { PortalComponent } from './portal/portal.component';
import { SignInComponent } from './sign-in/sign-in.component';

const routes: Routes = [
  {
    path: 'app-sign-in', component: SignInComponent
  },
  {
    path: 'app-plans', component:PlansComponent
  },
  {
    path: 'app-portal', component:PortalComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
