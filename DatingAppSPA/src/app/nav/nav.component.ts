import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'util';
import { AlertifyService } from '../_services/alertify.service';

@Component({
 selector: 'app-nav',
 templateUrl: './nav.component.html',
 styleUrls: ['./nav.component.css']
  })

export class NavComponent implements OnInit {
 model: any = {};

 constructor(private authService: AuthService, private alerify: AlertifyService) {}

 ngOnInit() { }

 login () {
  this.authService.login(this.model).subscribe(data => {
    this.alerify.success('Login successfully');
    }, err => {
      this.alerify.error(err);
    });
 }


 logout() {
   this.authService.usertoken = null;
   localStorage.removeItem('token');
   this.alerify.message('Logged out!');
 }

 loggedIn() {
   return this.authService.loggedIn();
 }
}
