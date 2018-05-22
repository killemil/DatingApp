import { Observable, throwError } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';
import { UserService} from '../_services/user.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot  } from '@angular/router';
import { User } from '../_models/User';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class MemberDetailResolver implements Resolve<User> {

    constructor(private userService: UserService,
        private router: Router, private alertify: AlertifyService) {}

        resolve(route: ActivatedRouteSnapshot): Observable<User> {
            return this.userService.getUser(route.params['id'])
            .pipe(map( u => {
                if (u) {
                    return u;
                } else {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['/members']);
                    return null;
                }
            }));
        }
}
