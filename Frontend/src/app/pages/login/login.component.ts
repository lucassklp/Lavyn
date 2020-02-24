import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { AuthenticationService } from '../../services/authentication.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  ngOnInit(): void {
  }

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private loginService: LoginService,
    private router: Router
    ) {
      this.form = fb.group({
        email: ['', Validators.required, Validators.email],
        password: ['', Validators.minLength(6)]
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
