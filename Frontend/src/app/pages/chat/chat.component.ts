import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';

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
    this.chatService.getUsersOnline().subscribe(users => {
      this.usersOnline = users;
    });

    this.chatService.onEnterRoom().subscribe(user => {
      this.usersOnline.unshift(user);
      console.log(user);
      this.change.detectChanges();
    });

    this.chatService.onLeaveRoom().subscribe(user => {
      //this.usersOnline(user);
      console.log(user);
      this.change.detectChanges();
    });
  }

  startChat(user: User){
    this.currentUserTalk = user;
  }

  sendMessage(){

  }

}
