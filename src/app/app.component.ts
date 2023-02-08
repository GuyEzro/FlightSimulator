import { Component, HostListener } from '@angular/core';
import FlightRepo from './repo/FlightService';
import { Flight } from './models/Flight';
import { SignalR } from './repo/SignalR';
import { SharedService } from './repo/SharedNewPlane';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'final-view';
  flights:any = [10];
  service:any;
  signal:any;
  newflight:Flight;
  sharedService: SharedService;
 rotate:boolean;

  constructor(Service:FlightRepo,Signal:SignalR,sharedService: SharedService) {
    this.sharedService = sharedService;
    this.service = Service;
    this.signal = Signal;
    this.signal.startConnection();
    this.signal.addTransferChartDataListener();
    this.service.get().subscribe(data => this.flights = data as Flight);
    this.sharedService.currentData.subscribe(data => this.ListenToServer(data));

    setInterval(() => {
      this.rotate = false;
    },5000)
  }

  ListenToServer(data:any) {
    if(data == null)
       return;
       this.rotate = true;
   let Index =  this.flights.findIndex(plane => plane.id == data.id)
    if (Index != -1) {
      this.flights.splice(Index, 1,data);
    }
    else{
      if(this.flights[0].leg == 8)
      this.flights.splice(0, 1);
      this.flights.push(data);
    }
  }
}
