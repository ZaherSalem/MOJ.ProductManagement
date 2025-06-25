import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { errorInterceptor } from './error.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(
      BrowserAnimationsModule, // Add this line
      ToastModule
    ),
    MessageService,
    provideHttpClient(withInterceptors([errorInterceptor])),
    provideRouter(routes),
  ],
};
