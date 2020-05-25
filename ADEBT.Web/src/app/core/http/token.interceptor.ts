import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { CredentialsService } from '../authentication/credentials.service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication/authentication.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(
    private authenticationService: AuthenticationService,
    private credentialsService: CredentialsService,
    private router: Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.credentialsService.isAuthenticated())
    {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.credentialsService.credentials.token}`
        }
      });
    }
    // return next.handle(request);
    return next.handle(request).pipe(
      tap(() => {},
      (err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status !== 401) {
            return;
          }
          this.authenticationService.logout().subscribe(() => this.router.navigate(['/login'], { replaceUrl: true }));
        }
      })
    );
  }
}
