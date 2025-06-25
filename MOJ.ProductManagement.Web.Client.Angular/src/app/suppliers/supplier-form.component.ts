import { Component, EventEmitter, Input, Output, OnChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Supplier } from './supplier.model';
import { CommonModule } from '@angular/common';
import { ApiService } from '../core/services/api.service';
import { ButtonModule } from 'primeng/button';
import { SupplierDto } from '../models/supplier.model';
import { CreateSupplierDto } from '../models/CreateSupplierDto.model';

@Component({
  selector: 'app-supplier-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonModule],
  templateUrl: './supplier-form.component.html',
  styleUrls: ['./supplier-form.component.scss']
})
export class SupplierFormComponent implements OnChanges {
  @Input() supplier: Supplier | null = null;
  @Output() saved = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  form: FormGroup;

  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.form = this.fb.group({
      id: [null],
      name: ['', Validators.required]
    });
  }

  ngOnChanges() {
    if (this.supplier) {
      this.form.patchValue(this.supplier);
    }
  }

  save() {
    if (this.form.invalid) return;
    if (this.form.value && this.form.value.id !== null) {
        //   this.apiService.updateSupplier(value).subscribe(() => this.saved.emit());
        const dto: SupplierDto = {...this.form.value};
        this.apiService.updateSupplier((dto.id), dto).subscribe(() => this.saved.emit()); 
    } else {
        //   this.apiService.addSupplier(value).subscribe(() => this.saved.emit());
        const dto: CreateSupplierDto = {...this.form.value};
    this.apiService.createSupplier(dto).subscribe(() => this.saved.emit()); 
}
  }

  cancel() {
    this.cancelled.emit();
  }
}
