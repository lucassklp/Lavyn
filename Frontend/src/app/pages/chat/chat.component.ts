import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import { User } from 'src/app/models/user';
import { Message } from 'src/app/models/message';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RoomMessage } from 'src/app/models/room-message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  usersOnline: User[] = [];
  form: FormGroup;
  currentRoomId: number;
  talks: {[roomId: number]: Message[]};

  constructor(private chatService: ChatService, private change: ChangeDetectorRef, private fb: FormBuilder) {
    this.form = fb.group({
      message: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.chatService.getUsersOnline().subscribe(users => {
      this.usersOnline = users;
    });

    this.chatService.onEnterRoom().subscribe(user => {
      this.usersOnline.unshift(user);
      this.change.detectChanges();
      this.getChat(user.id);
    });

    this.chatService.onLeaveRoom().subscribe(user => {
      const index = this.usersOnline.findIndex(x => x.id === user.id);
      if (index > -1) {
        this.usersOnline.splice(index, 1);
      }
      this.change.detectChanges();
    });

    this.chatService.listen().subscribe(receivedMsg => this.putMessage(receivedMsg.roomId, receivedMsg));
  }

  private putMessage(roomId: number, receivedMsg: Message) {
    const talk = this.talks[roomId];
    if (talk) {
      talk.push(receivedMsg);
    } else {
      this.talks[roomId] = [receivedMsg];
    }
  }

  getChat(userId: number) {
    this.chatService.getChatWith(userId).subscribe(talk => {
      this.currentRoomId = talk.roomId;
      for (const message of talk.messages) {
        this.putMessage(talk.roomId, message);
      }
    });
  }

  sendMessage() {
    const msg = new RoomMessage();
    msg.roomId = this.currentRoomId;
    msg.message = this.form.controls.message.value as string;
    this.chatService.sendMessage(msg).subscribe();
    this.form.controls.message.setValue('');
  }

}
