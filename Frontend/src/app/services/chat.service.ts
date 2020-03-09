import { Injectable } from '@angular/core';
import { SignalRService } from './signal-r.service';
import { Message } from '../models/message';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserInRoom } from '../models/user-in-room';
import { RoomMessage } from '../models/room-message';
import { Room } from '../models/room';
import { ViewedMessage } from '../models/viewed-message';
import { Call } from '../models/call';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private signalR: SignalRService, private http: HttpClient){
    this.signalR.connect("chat");
  }

  public getMyRooms(): Observable<Room[]> {
    return this.signalR.listen<Room[]>("my-rooms");
  }

  public sendMessage(message: RoomMessage): Observable<void> {
    return this.signalR.send<RoomMessage>("send-message", message);
  }

  public viewedRoom(roomKey: string): Observable<void> {
    return this.signalR.send<string>("viewed-room", roomKey);
  }

  public listenViewedRoom(): Observable<ViewedMessage> {
    return this.signalR.listen<ViewedMessage>("viewed-room");
  }

  public listenForMessages(): Observable<RoomMessage>{
    return this.signalR.listen<RoomMessage>("received-message");
  }

  public onEnterRoom(): Observable<UserInRoom>{
    return this.signalR.listen<UserInRoom>("enter-room");
  }

  public onLeaveRoom(): Observable<UserInRoom>{
    return this.signalR.listen<UserInRoom>("leave-room");
  }

  public getChatWith(userId: number): Observable<Room> {
    return this.http.get<Room>(`api/talk/with-user/${userId}`);
  }

  public call(call: Call): Observable<void>{
    return this.signalR.send("call", call)
  }

  public onCalled(): Observable<Call> {
    return this.signalR.listen<Call>("call");
  }

  public whenSomeoneAskMyPeer(): Observable<any> {
    return this.signalR.listen("ask-peer");
  }
}
