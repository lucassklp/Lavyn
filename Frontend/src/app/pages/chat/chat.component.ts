import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { Message } from 'src/app/models/message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  usersOnline: User[] = [];

  currentUserTalk: User;

  constructor(private chatService: ChatService, private change: ChangeDetectorRef) { }

  ngOnInit(): void {
    // this.chatService.getUsersOnline().subscribe(users => {
    //   this.usersOnline = users;
    // });

    this.chatService.onEnterRoom().subscribe(user => {
      this.usersOnline.unshift(user);
      console.log(user);
      this.change.detectChanges();
    });

    this.chatService.onLeaveRoom().subscribe(user => {
      const index = this.usersOnline.findIndex(x => x.id === user.id);
      if (index > -1) {
        this.usersOnline.splice(index, 1);
      }
      console.log(user);
      this.change.detectChanges();
    });
  }

  getChat(userId: number) {
    this.chatService.getChatId(userId).subscribe(id => console.log(id));
  }

  startChat(user: User){
    this.currentUserTalk = user;
    console.log(user);
    this.getChat(user.id);
  }

  sendMessage(){
    const msg = new Message();
    msg.message = "hello"
    this.chatService.sendMessage(msg);
  }

}
