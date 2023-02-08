import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";

@Injectable()

class FlightRepo{

constructor(private http: HttpClient) { }
url:string = 'https://localhost:7052/Flight/GetFlights';

get(){
 return this.http.get(this.url).pipe(map(res => res));
}

}
export default FlightRepo
