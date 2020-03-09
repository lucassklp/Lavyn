import { Injectable } from '@angular/core';
import * as Peer from 'peerjs'
import { Call } from '../models/call';

@Injectable({
  providedIn: 'root'
})
export class WebrtcService {

  private call: Call;
  private peer: Peer;
  private connection: Peer.DataConnection

  constructor() {
    this.peer = new Peer();
  }
}
