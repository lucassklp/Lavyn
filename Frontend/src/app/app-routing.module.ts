import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { ChatComponent } from './pages/chat/chat.component';
import { CallComponent } from './pages/call/call.component';


const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'chat',
    component: ChatComponent
  },
  {
    path: 'call/:id',
    component: CallComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    useHash: true
})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
