import { Component, OnInit } from '@angular/core';
import { Device } from 'src/app/Models/device.model';
import { HttpClientService } from 'src/app/Services/http-client.service';

@Component({
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.css']
})
export class DevicesComponent implements OnInit {

  devices: Device[] = [];

  constructor(private httpService: HttpClientService) { }

  ngOnInit(): void {
    this.httpService.getDevices().subscribe(
      ( response: any) => this.handleSuccessfulResponse(response),
    );
  }

  handleSuccessfulResponse(response: Device[]){
    this.devices= response;
  }

}
