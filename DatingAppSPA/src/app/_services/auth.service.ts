import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { filter, map, catchError } from 'rxjs/operators';


@Injectable()
export class AuthService {
 baseUrl = 'http://localhost:53146/api/auth/';
 usertoken: any;

constructor(private http: Http) { }

login(model: any) {
  return this.http
    .post(this.baseUrl + 'login', model, this.requestOptions())
    .pipe(map((response: Response) => {
      const user = response.json();
      if (user) {
        localStorage.setItem('token', user.tokenString);
        this.usertoken  = user.tokenString;
      }}));
}

  register(model: any) {
    return this.http
      .post(this.baseUrl + 'register', model, this.requestOptions());
  }

  private requestOptions() {
    const headers = new Headers({'Content-type' : 'application/json'});

    return new RequestOptions({headers: headers});
  }

  private handleError(error: any) {
    const applicationError = error.headers.get('Application-Error');

    if (applicationError) {
      return Observable.throw(applicationError);
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

    return Observable.create(new Error(modelStateErrors || 'Server error'));
  }

}
