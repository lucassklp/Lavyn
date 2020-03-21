import { Component, OnInit, ChangeDetectorRef, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RoomMessage } from 'src/app/models/room-message';
import { Room } from 'src/app/models/room';
import { UserInRoom } from 'src/app/models/user-in-room';
import { Message } from 'src/app/models/message';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Call, CallType } from 'src/app/models/call';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, OnDestroy {

  selectedRoom: Room;
  rooms: { [roomKey: string]: Room } = {};
  notRead: { [roomKey: string]: number } = {};
  form: FormGroup;

  @ViewChild('callModal') callModal: ElementRef;
  onCalledSubscription: Subscription;
  roomSubscription: Subscription;


  constructor(
    private chatService: ChatService,
    private change: ChangeDetectorRef,
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private modalService: NgbModal,
    private route: Router
  ) {
    this.form = fb.group({
      message: ['', Validators.required]
    });
  }

  ngOnDestroy(): void {
  }

  getRooms(): Room[] {
    const rooms = [];
    for (const key in this.rooms) {
      if (this.rooms.hasOwnProperty(key)) {
        rooms.push(this.rooms[key]);
      }
    }
    return rooms;
  }

  selectRoom(room: Room) {
    this.selectedRoom = room;
    this.chatService.viewedRoom(this.selectedRoom.key).subscribe(_ => {
      delete this.notRead[this.selectedRoom.key];
    });
  }

  private addRooms(rooms: Room[]): void {
    rooms.forEach(room => {
      this.rooms[room.key] = room;
      const lastSeen = room.lastViews.find(x => x.userId === this.authService.userId).lastSeen;
      const notReads = room.messages.filter(message => message.date > lastSeen).length;
      if (notReads > 0) {
        this.notRead[room.key] = notReads;
      }
    });
  }

  ngOnInit(): void {
    if(this.chatService.isConnected()){
      console.log("http");
      this.chatService.getMyRooms().subscribe(rooms => this.addRooms(rooms));
      this.subscribe();
    } else {
      this.chatService.connect()
      this.chatService.onConnected().subscribe(() => {
        let myRoomsSubscriptions = this.chatService.listenMyRooms().subscribe(rooms => {
          this.addRooms(rooms);
          myRoomsSubscriptions.unsubscribe();
        });
        this.subscribe();
      });      
    }
  }

  private subscribe(){
    this.chatService.onEnterRoom()
      .subscribe(userInRoom => this.updateUserStatus(userInRoom, true));

    this.chatService.onLeaveRoom()
      .subscribe(userInRoom => this.updateUserStatus(userInRoom, false));

    this.chatService.listenForMessages()
      .subscribe(receivedMsg => this.putMessage(receivedMsg));

    this.chatService.listenViewedRoom().subscribe(lastViewed => {
      const index = this.rooms[lastViewed.roomKey].lastViews.findIndex(x => x.userId == lastViewed.userId);
      this.rooms[lastViewed.roomKey].lastViews[index].lastSeen = lastViewed.lastSeen;
      this.change.detectChanges();
    });

    this.chatService.onCalled().subscribe(call => {
      if (call.callerId !== this.authService.userId) {
        this.modalService.open(this.callModal).result.then((result: boolean) => {
          if (result) {
            this.route.navigate(['call', call.key]);
          }
        });
      } else {
        this.route.navigate(['call', call.key]);
      }
    });
  }


  updateUserStatus(userInRoom: UserInRoom, status: boolean) {
    const room = this.rooms[userInRoom.roomKey];
    if(room){
      const user = room.participants.find(user => user.id == userInRoom.userId);
      user.isOnline = status;
    }
    this.change.detectChanges();
  }

  isEveryoneOnline(room: Room): boolean {
    if (room && room.participants) {
      return !room.participants.some(x => !x.isOnline);
    }
    return false;
  }

  getWhoHaveSeen(message: Message): Array<number> {
    return this.selectedRoom.lastViews.filter(x => x.lastSeen >= message.date)
      .map(viewer => viewer.userId)
  }

  private putMessage(receivedMsg: RoomMessage) {
    const room = this.rooms[receivedMsg.roomKey];
    if (room) {
      room.messages.push(receivedMsg);
    } else {
      this.rooms[receivedMsg.roomKey].messages = [receivedMsg];
    }
    if (!this.selectedRoom || this.selectedRoom.key !== room.key) {
      if (!this.notRead.hasOwnProperty(room.key)) {
        this.notRead[room.key] = 1;
      } else {
        this.notRead[room.key] += 1;
      }
    } else {
      this.chatService.viewedRoom(this.selectedRoom.key).subscribe()
    }
  }

  getNotRead(roomKey: string): number {
    return this.notRead[roomKey] || 0;
  }

  sendMessage() {
    const msg = new RoomMessage();
    msg.roomKey = this.selectedRoom.key;
    msg.message = this.form.controls.message.value as string;
    this.chatService.sendMessage(msg).subscribe();
    this.form.controls.message.setValue('');
  }

  call(room: Room, callType: CallType, event: Event) {
    event.stopPropagation();
    const call = new Call();
    call.callType = callType;
    call.key = room.key;
    this.chatService.call(call)
      .subscribe(
        () => console.log("Entering on call " + room.key),
        (err) => console.error(err)
      );
  }

}
