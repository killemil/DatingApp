import { Router } from '@angular/router';
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

 constructor(private authService: AuthService, private alerify: AlertifyService, private router: Router) {}

 ngOnInit() { }

 login () {
  this.authService.login(this.model).subscribe(data => {
    this.alerify.success('Login successfully');
    }, err => {
      this.alerify.error('Invalid username or password!');
    }, () => {
      this.router.navigate(['/members']);
    });
 }


 logout() {
   this.authService.usertoken = null;
   localStorage.removeItem('token');
   this.alerify.message('Logged out!');
   this.router.navigate(['/home']);
 }

 loggedIn() {
   return this.authService.loggedIn();
 }
}
