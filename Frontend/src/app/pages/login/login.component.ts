import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { AuthenticationService } from '../../services/authentication.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ChatService } from 'src/app/services/chat.service';
import { CallService } from 'src/app/services/call.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  ngOnInit(): void {
    this.authService.logout();
    if(this.callService.isConnected()){
      this.callService.disconect();
    }
    if(this.chatService.isConnected()){
      this.chatService.disconect();
      this.authService.logout();
    }
  }

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private loginService: LoginService,
    private router: Router,
    private chatService: ChatService,
    private callService: CallService
    ) {
      this.form = fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.minLength(6)]]
      });
    }

  login(){
    this.loginService.login({
      login: this.form.controls['email'].value,
      password: this.form.controls['password'].value
    }).subscribe(res => {
      this.authService.token = res.token;
      this.authService.user;
      this.router.navigate(['chat'])
    });
  }
}
