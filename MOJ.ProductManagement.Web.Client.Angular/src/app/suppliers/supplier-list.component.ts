import { Component, OnInit } from '@angular/core';
import { Supplier } from './supplier.model';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CardModule } from 'primeng/card';
import { ConfirmationService, MessageService } from 'primeng/api';
import { SupplierFormComponent } from './supplier-form.component';
import { ApiService } from '../core/services/api.service';

@Component({
  selector: 'app-supplier-list',
  standalone: true,
  imports: [
    CommonModule,
    SupplierFormComponent,
    TableModule,
    ButtonModule,
    DialogModule,
    ToastModule,
    ConfirmDialogModule,
    CardModule
  ],
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.scss'],
  providers: [ConfirmationService, MessageService]
})
export class SupplierListComponent implements OnInit {
//   suppliers: Supplier[] = [];
  suppliers: any[] = [];
  selectedSupplier: Supplier | null = null;
  displayDialog = false;
  loading = true;

  constructor(
    private apiService: ApiService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadSuppliers();
  }

  loadSuppliers(): void {
    this.loading = true;
    this.apiService.getSuppliers().subscribe({
      next: (data: any) => {
        this.suppliers = data.data;
        this.loading = false;
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load Suppliers'
        });
        this.loading = false;
      }
    });
  }

  editSupplier(supplier: Supplier) {
    this.selectedSupplier = { ...supplier };
    this.displayDialog = true;
  }

  deleteSupplier(id: number) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this supplier?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.apiService.deleteSupplier(id).subscribe({
          next: () => {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Supplier deleted successfully'
            });
            this.loadSuppliers();
          },
          error: () => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete supplier'
            });
          }
        });
      }
    });
  }

  addSupplier() {
    this.selectedSupplier = { id: null, name: '' };
    this.displayDialog = true;
  }

  onFormSaved() {
    this.displayDialog = false;
    this.selectedSupplier = null;
    this.loadSuppliers();
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Supplier saved successfully'
    });
  }

  onFormCancelled() {
    this.displayDialog = false;
    this.selectedSupplier = null;
  }
}
