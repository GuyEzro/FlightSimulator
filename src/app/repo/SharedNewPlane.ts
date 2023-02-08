import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Flight } from '../models/Flight';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private data = new BehaviorSubject<any>(null);
  currentData = this.data.asObservable();

  constructor() { }

  updateData(data) {
    this.data.next(data);
  }
}
