import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import FlightRepo from './repo/FlightService';
import { FlightComponent } from './flight/Flight.component';
import { SignalR } from './repo/SignalR';

@NgModule({
  declarations: [
    AppComponent,
    FlightComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [FlightRepo,SignalR],
  bootstrap: [AppComponent]
})
export class AppModule { }
