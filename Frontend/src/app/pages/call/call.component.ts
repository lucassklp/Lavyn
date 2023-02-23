import { Component, OnInit, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import Peer, { MediaConnection } from 'peerjs';
import { CallService } from 'src/app/services/call.service';
import { EnterCall } from 'src/app/models/enter-call';
import { zip } from 'rxjs';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  styleUrls: ['./call.component.scss']
})
export class CallComponent implements OnInit, OnDestroy {

  callId?: string = "";
  peer?: Peer;
  myStream?: MediaStream;
  participants: Participant[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute, 
    private callService: CallService,
    private changeDetector: ChangeDetectorRef, private chatServices: ChatService) { }

  ngOnDestroy(): void {
    this.callService.disconect();
    this.peer?.disconnect();
    this.myStream?.getTracks().forEach(track => track.stop());  
  }
  
  ngOnInit(): void {

    let myVideoElem = document.querySelector('#js-video-myself') as HTMLVideoElement;

    navigator.mediaDevices.getUserMedia({video: true}).then(stream => {
      myVideoElem.srcObject = stream;
      myVideoElem.play();
      this.myStream = stream;
    }, err => console.error(err));

    if(!this.callService.isConnected()){
      this.callService.connect();
      zip(this.route.paramMap, this.callService.onConnected())
        .subscribe(result => {
          const params = result[0];
          this.callId = params.get('id')!;
          this.peer = new Peer();
          
          this.peer.on("open", (id) => {
            var enterCall = new EnterCall();
            enterCall.key = this.callId;
            enterCall.peerId = id;
            console.log(enterCall);
  
            this.callService.enter(enterCall).subscribe(connectedIds => {
              connectedIds.forEach(id => {
                const call = this.peer!.call(id, this.myStream!);
                call.on("stream", stream => this.addParticipant(call.peer, stream));
              });
            });
          });
  
          this.peer.on("call", call => {
            call.answer(this.myStream);
            call.on("stream", stream => this.addParticipant(call.peer, stream));
            call.on("close", () => this.removeParticipant(call.peer));
          });
        }, err => {
          this.router.navigate(['login']);
        });
  
        this.callService.onConnected().subscribe(() => {
          this.callService.onSomeoneLeave().subscribe(peerId => this.removeParticipant(peerId));
        });
    }
  }

  private removeParticipant(id: string) {
    console.log(id);
    let participantIndex = this.participants.findIndex(participant => participant.id === id)
    if(participantIndex > -1){
      this.participants.splice(participantIndex, 1);
      this.changeDetector.detectChanges();
    }
  }

  private addParticipant(id: string, stream: MediaStream){
    const participant = new Participant()
    participant.id = id;
    participant.stream = stream;
    participant.call;
    this.participants.push(participant);
    this.changeDetector.detectChanges();
    let videoElem = document.querySelector('#js-video-partner-' + participant.id) as HTMLVideoElement;
    videoElem.srcObject = stream;
    videoElem.play();
  }
}

export class Participant {
  id?: string;
  stream?: MediaStream;
  call?: MediaConnection;
}