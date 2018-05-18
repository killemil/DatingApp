import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { Observable, throwError } from 'rxjs';
import { map, filter, catchError, mergeMap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthService {
 baseUrl = 'http://localhost:53146/api/auth/';
 usertoken: any;
 decodedToken: any;
 jwtHelper: JwtHelperService = new JwtHelperService();

constructor(private http: Http) { }


login(model: any) {
  return this.http
    .post(this.baseUrl + 'login', model, this.requestOptions())
    .pipe(
      map((response: Response) => {
      const user = response.json();
      if (user) {
        localStorage.setItem('token', user.tokenString);
        this.decodedToken = this.jwtHelper.decodeToken(user.tokenString);
        this.usertoken  = user.tokenString;
      }}),
      catchError(this.handleError));
}

  register(model: any) {
    return this.http
      .post(this.baseUrl + 'register', model, this.requestOptions())
      .pipe(catchError(this.handleError));
  }

  loggedIn() {
    return localStorage.getItem('token') ? true : false;
  }

  private requestOptions() {
    const headers = new Headers({'Content-type' : 'application/json'});

    return new RequestOptions({headers: headers});
  }

  private handleError(error: any) {
    const applicationError = error.headers.get('Application-Error');

    if (applicationError) {
      return throwError(applicationError);
    }

    const serverError = error.json();
    let modelStateErrors = '';

    if (serverError) {
      for (const key in serverError) {
        if (serverError[key]) {
          modelStateErrors += serverError[key] + '\n';
        }
      }
    }

    return throwError(modelStateErrors || 'Server error');
  }

}
