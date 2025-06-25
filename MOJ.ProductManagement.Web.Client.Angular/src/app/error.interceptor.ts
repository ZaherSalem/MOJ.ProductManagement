import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MessageService } from 'primeng/api';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const messageService = inject(MessageService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const errorMessage = getErrorMessage(error);
      // Show error toast (only on client side)
      if (typeof window !== 'undefined') {
        messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: errorMessage,
          life: 5000,
        });
      }

      return throwError(() => error);
    })
  );
};

function getErrorMessage(error: HttpErrorResponse): string {
  if (error.error instanceof ErrorEvent) {
    return `Client error: ${error.error.message}`;
  }

  switch (error.status) {
    case 400:
      return handleBadRequest(error);
    case 401:
      return 'Unauthorized - Please login';
    case 403:
      return 'Forbidden - Insufficient permissions';
    case 404:
      return 'Resource not found';
    case 500:
      return 'Server error - Please try again later';
    default:
      return error.message || 'An unknown error occurred';
  }
}

function handleBadRequest(error: HttpErrorResponse): string {
  if (error.error?.errors) {
    return Object.values(error.error.errors).flat().join('\n');
  }
  return error.error?.message || 'Invalid request';
}
