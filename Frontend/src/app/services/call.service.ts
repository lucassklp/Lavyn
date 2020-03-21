import { Injectable } from '@angular/core';
import { SignalRService } from './signal-r.service';
import { EnterCall } from '../models/enter-call';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class CallService extends SignalRService {
  public connect(): void {
    this.connectTo("call")
  }

  constructor(authService: AuthenticationService) {
    super(authService);
  }

  public enter(enterCall: EnterCall): Observable<string[]>{
    return this.sendAndListen<EnterCall, string[]>("enter-call", enterCall);
  }

  public onSomeoneLeave(): Observable<string> {
    return this.listen<string>("leave-call");
  }
}
