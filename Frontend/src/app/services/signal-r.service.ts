import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Observable, from, Observer } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection

  constructor(private authService: AuthenticationService) { }

  public connect(channel: string): Observable<void> {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5000/${channel}`, {
        accessTokenFactory: () => {
          this.authService.user;
          return this.authService.token
        }
      })
      .build();

    return from(this.hubConnection.start())
  }

  public send<T>(method: string, message: T): Observable<void> {
    return from(this.hubConnection.send(method, message));
  }

  public listen<T>(method: string): Observable<T> {
    return Observable.create((observer: Observer<T>) => {
      this.hubConnection.on(method, message => {
        observer.next(message);
      })
    })
  }
}
