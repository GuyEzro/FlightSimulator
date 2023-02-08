import { AfterContentInit, Component, HostBinding, Input, OnInit } from '@angular/core';
import { Flight } from '../models/Flight';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrls: ['./flight.component.css']
})
export class FlightComponent {
@Input() flight?:Flight;
@Input() rotate:boolean;

}
