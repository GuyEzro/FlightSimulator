import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { Flight } from "../models/Flight";
import { SharedService } from "./SharedNewPlane";

@Injectable()

export class SignalR{
private connection: signalR.HubConnection;

constructor(private sharedService: SharedService) {}

public startConnection(){
  this.connection = new signalR.HubConnectionBuilder()
  .withUrl('https://localhost:7052/chat',{
    skipNegotiation:true,
    transport: signalR.HttpTransportType.WebSockets,
    accessTokenFactory: () => {
      return localStorage.getItem("access_token")
    }
  })
  .build();

this.connection.start().then(() => {
  console.log("connection started");
}).catch(err => console.log('error while starting connection ' + err))
}

public addTransferChartDataListener() {
  this.connection.on('Receive', (data) => {
    this.sharedService.updateData(data);
  });
}
}
