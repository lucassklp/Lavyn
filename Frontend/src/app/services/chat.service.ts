import { Injectable } from '@angular/core';
import { SignalRService } from './signal-r.service';
import { Message } from '../models/message';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private signalR: SignalRService, private http: HttpClient){
    this.signalR.connect("chat");
  }
  
  public getUsersOnline(): Observable<User[]> {
    return this.http.get<User[]>("api/users/online");
  }

  public sendMessage(message: Message): Observable<void> {
    return this.signalR.send<Message>("send-message", message);
  }

  public listen(): Observable<Message>{
    return this.signalR.listen<Message>("send-message");
  }

  public onEnterRoom(): Observable<User>{
    return this.signalR.listen<User>("enter-room");
  }

  public onLeaveRoom(): Observable<User>{
    return this.signalR.listen<User>("leave-room");
  }
}
