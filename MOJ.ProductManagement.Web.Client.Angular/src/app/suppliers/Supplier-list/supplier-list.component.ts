import { Component, OnInit } from '@angular/core';
import { Supplier } from '../supplier.model';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CardModule } from 'primeng/card';
import { ConfirmationService, MessageService } from 'primeng/api';
import { SupplierFormComponent } from '../Supplier-form/supplier-form.component';
import { FormsModule } from '@angular/forms';
import { IPaginatedRequest } from '../../core/generated/Interfaces';
import { SupplierService } from '../../core/services/SupplierService';

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
    CardModule,
    FormsModule,
  ],
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.scss'],
  providers: [ConfirmationService, MessageService],
})
export class SupplierListComponent implements OnInit {
  //   suppliers: Supplier[] = [];
  suppliers: any[] = [];
  filteredSuppliers: any[] = [];
  searchTerm: string = '';
  selectedSupplier: Supplier | null = null;
  displayDialog = false;
  loading = true;

  constructor(
    private apiService: SupplierService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadSuppliers();
  }

  loadSuppliers(): void {
    this.loading = true;
    const paginatedRequest: IPaginatedRequest = {
      PageNumber: 1,
      PageSize: 20,
      SearchValue: '',
    };
    this.apiService.getSuppliers(paginatedRequest).subscribe({
      next: (data: any) => {
        this.suppliers = data.data;
        this.filterSuppliers();
        this.loading = false;
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load Suppliers',
        });
        this.loading = false;
      },
    });
  }

  filterSuppliers() {
    const search = this.searchTerm?.toLowerCase() || '';
    this.filteredSuppliers = this.suppliers.filter((supplier) =>
      supplier.name?.toLowerCase().includes(search)
    );
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
              detail: 'Supplier deleted successfully',
            });
            this.loadSuppliers();
          },
          error: () => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete supplier',
            });
          },
        });
      },
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
      detail: 'Supplier saved successfully',
    });
  }

  onFormCancelled() {
    this.displayDialog = false;
    this.selectedSupplier = null;
  }

  onSearch(): void {
    // Optionally implement filtering logic here, e.g.:
    // this.filteredProducts = this.products.filter(product =>
    //   product.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    // );
  }
}
