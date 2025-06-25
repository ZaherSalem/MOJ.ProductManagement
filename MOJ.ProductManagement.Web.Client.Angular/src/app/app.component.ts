import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterModule, MenubarModule, ToastModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [MessageService] 
})
export class AppComponent {
  constructor(private messageService: MessageService) {}

  title = 'Product Management';
  navItems = [
    { label: 'Statistics', icon: 'pi pi-home', routerLink: '/dashboard' },
    { label: 'Products', icon: 'pi pi-box', routerLink: '/products' },
    { label: 'Suppliers', icon: 'pi pi-users', routerLink: '/suppliers' },

  ];
}