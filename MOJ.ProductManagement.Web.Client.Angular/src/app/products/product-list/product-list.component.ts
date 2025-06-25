import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CardModule } from 'primeng/card';
import { trigger } from '@angular/animations';
import { ProductFormComponent } from '../product-form/product-form.component';
import { ProductService } from '../../core/services/ProductService';
import {
  ICreateProductDto,
  IPaginatedRequest,
  IProductDto,
  IUpdateProductDto,
} from '../../core/generated/Interfaces';
import { SupplierService } from '../../core/services/SupplierService';
import { QuantityPerUnit } from '../../core/generated/generated-enums';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    TableModule,
    ButtonModule,
    RippleModule,
    DialogModule,
    InputTextModule,
    DropdownModule,
    InputNumberModule,
    ToastModule,
    ConfirmDialogModule,
    CardModule,
    ProductFormComponent,
  ],
  templateUrl: './product-list.component.html',
  providers: [ConfirmationService, MessageService],
  animations: [
    // Define your animations here if using @animation.start
    trigger('animation', [
      // animation states and transitions
    ]),
  ],
})
export class ProductListComponent implements OnInit {
  products: any[] = [];
  loading = true;
  displayDialog = false;
  selectedProduct: IProductDto = {} as IProductDto;
  suppliers: any[] = [];
  quantityOptions = Object.entries(QuantityPerUnit)
    .filter(([key, value]) => isNaN(Number(key)))
    .map(([key, value]) => ({ label: key, value }));

  columnHeaders = [
    { label: 'Name', field: 'name' },
    { label: 'Quantity Per Unit', field: 'quantityPerUnitName' },
    { label: 'Unit Price', field: 'unitPrice' },
    { label: 'In Stock', field: 'unitsInStock' },
    { label: 'On Order', field: 'unitsOnOrder' },
    { label: 'Reorder Level', field: 'reorderLevel' },
    { label: 'Supplier', field: 'supplierName' },
    { label: 'Actions', field: 'actions' },
  ];

  searchTerm: string = '';

  constructor(
    private productApiService: ProductService,
    private supplierApiService: SupplierService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadSuppliers();
  }

  loadProducts(dto: IPaginatedRequest | null = null): void {
    this.loading = true;
    // Provide default pagination parameters
    const paginatedRequest: IPaginatedRequest = dto || {
      PageNumber: 1,
      PageSize: 10,
      SearchValue: '',
    };
    this.productApiService.getProducts(paginatedRequest).subscribe({
      next: (data: any) => {
        this.products = data.data;
        this.loading = false;
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load products',
        });
        this.loading = false;
      },
    });
  }

  filterSuppliers() {
    const search = this.searchTerm?.toLowerCase() || '';
    const dto: IPaginatedRequest = {
      PageNumber: 1,
      PageSize: 20,
      SearchValue: this.searchTerm,
    };
    this.loadProducts(dto);
  }

  loadSuppliers(): void {
    const paginatedRequest: IPaginatedRequest = {
      PageNumber: 1,
      PageSize: 10,
      SearchValue: '',
    };
    this.supplierApiService.getSuppliers(paginatedRequest).subscribe({
      next: (data: any) => {
        this.suppliers = data.data;
      },
    });
  }

  showEditDialog(product?: any): void {
    this.selectedProduct = product ? { ...product } : {};
    // if(product) {
    //   this.selectedProduct.QuantityPerUnitId = product.quantityPerUnitName;
    // }
    this.displayDialog = true;
  }

  saveProduct(): void {
    // Exclude quantityPerUnitName from the DTO
    const { QuantityPerUnitName, SupplierName, ...rest } = this.selectedProduct;
    const dto: any =
      this.selectedProduct.Id == null
        ? ({ ...rest } as ICreateProductDto)
        : ({ ...rest } as IUpdateProductDto);

    const operation =
      this.selectedProduct.Id == null
        ? this.productApiService.createProduct(dto)
        : this.productApiService.updateProduct(this.selectedProduct.Id, dto);

    operation.subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: `Product ${
            this.selectedProduct.Id ? 'updated' : 'created'
          } successfully`,
        });
        this.displayDialog = false;
        this.loadProducts();
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: `Failed to ${
            this.selectedProduct.Id ? 'update' : 'create'
          } product`,
        });
      },
    });
  }

  confirmDelete(product: any): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this product?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.productApiService.deleteProduct(product.id).subscribe({
          next: () => {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Product deleted successfully',
            });
            this.loadProducts();
          },
          error: () => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete product',
            });
          },
        });
      },
    });
  }

  onSearch(): void {
    // Optionally implement filtering logic here, e.g.:
    // this.filteredProducts = this.products.filter(product =>
    //   product.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    // );
  }
}
