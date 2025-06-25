import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-product-form',
  standalone: true,
  templateUrl: './product-form.component.html',
  imports: [
    CommonModule,
    FormsModule,
    InputTextModule,
    DropdownModule,
    InputNumberModule,
    ButtonModule
  ],
})
export class ProductFormComponent {
  @Input() selectedProduct: any;
  @Input() quantityOptions: any[] = [];
  @Input() suppliers: any[] = [];
  @Output() save = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  
  saveProduct() {
    this.save.emit();
  }
}
