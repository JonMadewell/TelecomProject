import { Component, OnInit } from '@angular/core';
import { device } from 'src/app/Models/device.model';
import { HttpClientService } from 'src/app/Services/http-client.service';

@Component({
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.css']
})
export class DevicesComponent implements OnInit {

  devices: device[] = [];

  constructor(private httpService: HttpClientService) { }

  ngOnInit(): void {
    this.httpService.getDevices().subscribe(
      ( response: any) => this.handleSuccessfulResponse(response),
    );
  }

  handleSuccessfulResponse(response: device[]){
    this.devices= response;
  }

}
