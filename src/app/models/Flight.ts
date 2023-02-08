export class Flight{
flightCode:string;
leg:number;
id:string;
brand:string;
model:string;
passengerCount:number;
isCriticalLanding:boolean;
speed:number;
isInLanding:boolean;
isReady:boolean
t1Time:string;
t2Time:string;
t3Time:string;
t4Time:string;
t5Time:string;
t6Time:string;
t7Time:string;
t8Time:string;
t9Time:string;
origin:string;
destination:string;
constructor(flightCode:string = "",leg:number = 0,id:string = "",brand:string="",model:string="",passengerCount:number=0,isCriticalLanding:boolean=false,speed:number=0,isInLanding:boolean=false,isReady:boolean=false,t1Time:string="",t2Time:string="", t3Time:string="", t4Time:string="",t5Time:string="",t6Time:string="",t7Time:string="",t8Time:string="",t9Time:string="",origin:string,destination:string)
{
  this.flightCode = flightCode;
  this.leg = leg;
  this.id = id;
  this.brand = brand;
  this.model = model;
  this.passengerCount = passengerCount;
  this.isCriticalLanding = isCriticalLanding;
  this.speed = speed;
  this.isInLanding = isInLanding;
  this.isReady =isReady;
  this.t1Time = t1Time;
  this.t2Time = t2Time;
  this.t3Time = t3Time;
  this.t4Time = t4Time;
  this.t5Time = t5Time;
  this.t6Time = t6Time;
  this.t7Time = t7Time;
  this.t8Time = t8Time;
  this.t9Time = t9Time;
  this.origin = origin;
  this.destination = destination;
}

}
