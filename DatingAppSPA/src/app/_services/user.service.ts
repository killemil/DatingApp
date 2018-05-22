import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Observable, throwError } from 'rxjs';
import { User } from '../_models/User';
import { map, filter, catchError, mergeMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

constructor(private http: Http) { }

 getUsers(): Observable<User[]> {
   return this.http
   .get(this.baseUrl + 'users', this.jwt())
   .pipe(
     map(response => <User[]>response.json()), catchError(this.handleError));
 }

 getUser(id): Observable<User> {
   return this.http
   .get(this.baseUrl + 'users/' + id, this.jwt())
   .pipe(
     map(response => <User>response.json()), catchError(this.handleError));
 }

 updateUser(id: number, user: User) {
   return this.http.put(this.baseUrl + 'users/' + id, user, this.jwt())
   .pipe(
     map(response => response.json()), catchError(this.handleError));
 }

 private jwt() {
   const token = localStorage.getItem('token');
   if (token) {
    const headers = new Headers({'Authorization' : 'Bearer ' + token});
    headers.append('Content-type', 'application/json');

    return new RequestOptions({headers : headers});
   }
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
