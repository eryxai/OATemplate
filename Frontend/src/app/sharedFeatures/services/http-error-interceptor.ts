import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      tap(
        () => {},
        error => {
          if (error.status === 500) {
            console.log(error)
            // Redirect to error page for specific screens
            if (this.isErrorPageRequired(request.url)) {
              this.router.navigate(['/error/page-not-found']);
            }
          }
        }
      )
    );
  }

  private isErrorPageRequired(url: string): boolean {
    // Define the screens where you want to redirect to the error page
    const requiredScreens = [
      'Violations/GetByID',
      'Advertising/GetViewAdvertising',
    ];
    return requiredScreens.some(screen => url.includes(screen));
  }
}
