import * as signalR from "@microsoft/signalr";
import { Observable, from, Observer } from 'rxjs';
import { AuthenticationService } from './authentication.service';

export abstract class SignalRService {

  private hubConnection?: signalR.HubConnection

  constructor(private authService: AuthenticationService) { }

  private onConntected$?: Observable<void>

  protected connectTo(channel: string): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5000/${channel}`, {
        accessTokenFactory: () => this.authService.token
      })
      .build();

      this.onConntected$ = from(this.hubConnection.start());
  }

  public abstract connect(): void;

  public onConnected(): Observable<void> {
    return this.onConntected$!;
  }

  protected send<T>(method: string, message: T): Observable<void> {
    return from(this.hubConnection!.send(method, message));
  }

  protected listen<T>(method: string): Observable<T> {
    return Observable.create((observer: Observer<T>) => {
      this.hubConnection!.on(method, message => {
        observer.next(message);
      })
    })
  }

  protected sendAndListen<T, R>(method: string, message: T): Observable<R> {
    this.send<T>(method, message);
    return this.listen<R>(method);
  }

  public disconect(): void {
    this.hubConnection!.stop();
  }

  public isConnected(): boolean {
    if(!this.hubConnection)
      return false;
    return this.hubConnection.state === signalR.HubConnectionState.Connected;
  }

}
