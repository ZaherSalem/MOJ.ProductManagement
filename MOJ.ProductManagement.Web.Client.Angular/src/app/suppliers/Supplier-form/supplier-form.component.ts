import {
  Component,
  EventEmitter,
  Input,
  Output,
  OnChanges,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Supplier } from '../supplier.model';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { SupplierService } from '../../core/services/SupplierService';
import {
  ICreateSupplierDto,
  ISupplierDto,
} from '../../core/generated/Interfaces';

@Component({
  selector: 'app-supplier-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ButtonModule],
  templateUrl: './supplier-form.component.html',
  styleUrls: ['./supplier-form.component.scss'],
})
export class SupplierFormComponent implements OnChanges {
  @Input() supplier: Supplier | null = null;
  @Output() saved = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  form: FormGroup;

  constructor(private fb: FormBuilder, private apiService: SupplierService) {
    this.form = this.fb.group({
      id: [null],
      name: ['', Validators.required],
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
      const dto: ISupplierDto = { ...this.form.value };
      this.apiService
        .updateSupplier(dto.Id, dto)
        .subscribe(() => this.saved.emit());
    } else {
      const dto: ICreateSupplierDto = { ...this.form.value };
      this.apiService.createSupplier(dto).subscribe(() => this.saved.emit());
    }
  }

  cancel() {
    this.cancelled.emit();
  }
}
