import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { SignInComponent } from './Components/sign-in/sign-in.component';
import { PortalComponent } from './Components/portal/portal.component';
import { RegisterComponent } from './Components/register/register.component';


const routes: Routes = [
  {
    path: 'app-home', component: HomeComponent
  },
  {
    path: 'app-sign-in', component: SignInComponent
  },
  {
    path: 'app-portal', component: PortalComponent
  },
  {
    path: 'app-register', component: RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
