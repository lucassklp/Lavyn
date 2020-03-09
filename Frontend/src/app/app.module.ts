import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule, TranslateService } from '@ngx-translate/core';
import { TranslateHttpLoader}  from '@ngx-translate/http-loader';
import { NgxErrorsModule } from '@hackages/ngxerrors';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { GravatarModule } from  'ngx-gravatar';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { LoginComponent } from './pages/login/login.component';
import { LoginService } from './services/login.service';
import { AuthenticationService } from './services/authentication.service';
import { ErrorHandlingInterceptor } from './interceptors/error.handling.interceptor';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import { RegisterComponent } from './pages/register/register.component';
import { ChatComponent } from './pages/chat/chat.component';
import { SignalRService } from './services/signal-r.service';
import { CallComponent } from './pages/call/call.component';
import { WebrtcService } from './services/webrtc.service';


export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ChatComponent,
    CallComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    NgxErrorsModule,
    ToastrModule.forRoot(),
    GravatarModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    }),
  ],
  providers: [
    AuthenticationService,
    LoginService,
    SignalRService,
    WebrtcService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlingInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(translate: TranslateService){
    // this language will be used as a fallback when a translation isn't found in the current language
    translate.setDefaultLang('pt');

    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use('pt');
  }
}
