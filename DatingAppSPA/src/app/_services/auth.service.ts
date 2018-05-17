import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthService {
 baseUrl = 'http://localhost:53146/api/auth/';
 usertoken: any;

constructor(private http: Http) { }

login(model: any) {
  return this.http.post(this.baseUrl + 'login', model, this.requestOptions())
   .toPromise().then((response: Response) => {
   const user = response.json();
   if (user) {
     localStorage.setItem('token', user.tokenString);
     this.usertoken  = user.tokenString;
   }});
}

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model, this.requestOptions());
  }

  private requestOptions() {
    const headers = new Headers({'Content-type' : 'application/json'});

    return new RequestOptions({headers: headers});
  }
}
