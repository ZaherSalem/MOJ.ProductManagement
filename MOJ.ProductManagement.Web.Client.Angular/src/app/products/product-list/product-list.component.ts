import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ApiService } from '../../core/services/api.service';
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
import { eQuantityPerUnit } from '../../../Enums/eQuantityPerUnit';
import { CreateProductDto } from '../../models/CreateProductDto';
import { ProductDto } from '../../models/ProductDto';
import { UpdateProductDto } from '../../models/UpdateProductDto';
import { ProductFormComponent } from '../product-form/product-form.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    TableModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    DropdownModule,
    InputNumberModule,
    ToastModule,
    ConfirmDialogModule,
    CardModule,
    ProductFormComponent
  ],
  templateUrl: './product-list.component.html',
  providers: [ConfirmationService, MessageService],
  animations: [
    // Define your animations here if using @animation.start
    trigger('animation', [
      // animation states and transitions
    ])
  ]
})
export class ProductListComponent implements OnInit {
  products: any[] = [];
  loading = true;
  displayDialog = false;
  selectedProduct: ProductDto = {} as ProductDto;
  suppliers: any[] = [];
  quantityOptions = Object.entries(eQuantityPerUnit)
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
    { label: 'Actions', field: 'actions' }
  ];

  searchTerm: string = '';

  constructor(
    private apiService: ApiService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.loadProducts();
    this.loadSuppliers();
  }

  loadProducts(): void {
    this.loading = true;
    this.apiService.getProducts().subscribe({
      next: (data: any) => {
        this.products = data.data;
        this.loading = false;
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load products'
        });
        this.loading = false;
      }
    });
  }

  loadSuppliers(): void {
    this.apiService.getSuppliers().subscribe({
      next: (data: any) => {
        this.suppliers = data.data;
      }
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
      const { quantityPerUnitName,supplierName, ...rest } = this.selectedProduct;
      const dto: any = this.selectedProduct.id == null
        ? { ...rest } as CreateProductDto
        : { ...rest } as UpdateProductDto;
      

    const operation= this.selectedProduct.id == null?
      this.apiService.createProduct(dto): 
      this.apiService.updateProduct(this.selectedProduct.id, dto)

    operation.subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: `Product ${this.selectedProduct.id ? 'updated' : 'created'} successfully`
        });
        this.displayDialog = false;
        this.loadProducts();
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: `Failed to ${this.selectedProduct.id ? 'update' : 'create'} product`
        });
      }
    });
  }

  confirmDelete(product: any): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this product?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.apiService.deleteProduct(product.id).subscribe({
          next: () => {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Product deleted successfully'
            });
            this.loadProducts();
          },
          error: () => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete product'
            });
          }
        });
      }
    });
  }

  onSearch(): void {
    // Optionally implement filtering logic here, e.g.:
    // this.filteredProducts = this.products.filter(product =>
    //   product.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    // );
  }
}

