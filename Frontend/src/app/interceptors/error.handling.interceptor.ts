import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorHandlingInterceptor implements HttpInterceptor {

    constructor(private toastr: ToastrService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<any> {
        return next.handle(req).pipe(
            catchError((error: HttpErrorResponse) => {
                const msg = error.error;

                if (msg.message instanceof Array) {
                    let message = `<ul>`;
                    const errors = msg.message as Array<any>;
                    for (const element of errors) {
                        message += `<li> ${element.error} </li>`;
                    }
                    message += `</ul>`;
                    this.toastr.error(message, msg.token, {
                        closeButton: true,
                        enableHtml: true
                    });
                } else {
                    this.toastr.error(msg.message, msg.token, {
                        closeButton: true
                    });
                }

                return throwError(error.error);
            })
        );
    }
}
