import { Injectable } from '@angular/core';
import { SignalRService } from './signal-r.service';
import { EnterCall } from '../models/enter-call';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CallService {

  constructor(private signalR: SignalRService) {
    signalR.connect("call");
  }

  public enter(enterCall: EnterCall): Observable<void>{
    return this.signalR.send("enter-call", enterCall);
  }

  public onEnter(): Observable<string[]> {
    return this.signalR.listen<string[]>("");
  }
}
