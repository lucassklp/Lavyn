import { Injectable } from '@angular/core';
import { SignalRService } from './signal-r.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserInRoom } from '../models/user-in-room';
import { RoomMessage } from '../models/room-message';
import { Room } from '../models/room';
import { ViewedMessage } from '../models/viewed-message';
import { Call } from '../models/call';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService extends SignalRService {
  public connect(): void {
    this.connectTo("chat");
  }

  constructor(private http: HttpClient, authService: AuthenticationService){
    super(authService);
  }

  public getMyRooms(): Observable<Room[]> {
    return this.http.get<Room[]>("api/talk/my-talks");
  }

  public listenMyRooms(): Observable<Room[]> {
    return this.listen<Room[]>("my-rooms");
  }

  public sendMessage(message: RoomMessage): Observable<void> {
    return this.send<RoomMessage>("send-message", message);
  }

  public viewedRoom(roomKey: string): Observable<void> {
    return this.send<string>("viewed-room", roomKey);
  }

  public listenViewedRoom(): Observable<ViewedMessage> {
    return this.listen<ViewedMessage>("viewed-room");
  }

  public listenForMessages(): Observable<RoomMessage>{
    return this.listen<RoomMessage>("received-message");
  }

  public onEnterRoom(): Observable<UserInRoom>{
    return this.listen<UserInRoom>("enter-room");
  }

  public onLeaveRoom(): Observable<UserInRoom>{
    return this.listen<UserInRoom>("leave-room");
  }

  public getChatWith(userId: number): Observable<Room> {
    return this.http.get<Room>(`api/talk/with-user/${userId}`);
  }

  public call(call: Call): Observable<void>{
    return this.send("call", call)
  }

  public onCalled(): Observable<Call> {
    return this.listen<Call>("call");
  }
}
