import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'util';

@Component({
 selector: 'app-nav',
 templateUrl: './nav.component.html',
 styleUrls: ['./nav.component.css']
  })

export class NavComponent implements OnInit {
 model: any = {};

 constructor(private authService: AuthService) {}

 ngOnInit() { }

 login () {
  this.authService.login(this.model).subscribe(data => {
    console.log('login successfully');
    }, err => {
      console.log(err);
    });
 }


 logout() {
   this.authService.usertoken = null;
   localStorage.removeItem('token');
   console.log('logged out!');
 }

 loggedIn() {
  const token = localStorage.getItem('token');

  return !!token;
 }
}
