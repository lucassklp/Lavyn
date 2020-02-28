import { Injectable } from '@angular/core';
import { SignalRService } from './signal-r.service';
import { Message } from '../models/message';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';
import { RoomMessage } from '../models/room-message';
import { Talk } from '../models/chat';

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

  public sendMessage(message: RoomMessage): Observable<void> {
    return this.signalR.send<RoomMessage>("send-message", message);
  }

  public listen(): Observable<RoomMessage>{
    return this.signalR.listen<RoomMessage>("received-message");
  }

  public onEnterRoom(): Observable<User>{
    return this.signalR.listen<User>("enter-room");
  }

  public onLeaveRoom(): Observable<User>{
    return this.signalR.listen<User>("leave-room");
  }

  public getChatWith(userId: number): Observable<Talk> {
    return this.http.get<Talk>(`api/chat/with-user/${userId}`);
  }
}
