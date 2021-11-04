import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatCardModule} from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatFormFieldModule} from '@angular/material/form-field';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AuthenticationInterceptorService } from 'src/Interceptors/authentication-interceptor.service';
import { AuthGuardService } from './Services/auth-guard.service';
import { AuthenticationService } from './Services/authentication.service';

import { HttpClientService } from './Services/http-client.service';
import { SignInComponent } from './Component/sign-in/sign-in.component';
import { NavBarComponent } from './Component/nav-bar/nav-bar.component';
import { PortalComponent } from './Component/portal/portal.component';
import { PlansComponent } from './Component/plans/plans.component';
import { DevicesComponent } from './Component/devices/devices.component';
import { HomeComponent } from './Component/home/home.component';
import { RegisterComponent } from './Component/register/register.component';
import { NavPortalComponent } from './Component/nav-portal/nav-portal.component';
import { PlanDetailsComponent } from './Component/plan-details/plan-details.component';


@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    NavBarComponent,
    PortalComponent,
    PlansComponent,
    DevicesComponent,
    HomeComponent,
    RegisterComponent,
    NavPortalComponent,
    PlanDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatCardModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    FlexLayoutModule,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthenticationInterceptorService,
    multi: true
  }, AuthGuardService, HttpClientService, AuthenticationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
