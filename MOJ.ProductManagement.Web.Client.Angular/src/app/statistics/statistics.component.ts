// statistics.component.ts
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ProgressBarModule } from 'primeng/progressbar';
import { TimelineModule } from 'primeng/timeline';
import { TagModule } from 'primeng/tag';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';
import { ProductService } from '../core/services/ProductService';
import { MessagesModule } from 'primeng/messages';
import { ListboxModule } from 'primeng/listbox';
import { TableModule } from 'primeng/table';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-statistics',
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    RippleModule,
    CardModule,
    ProgressBarModule,
    TimelineModule,
    TagModule,
    DatePipe,
    MessagesModule,
    ListboxModule,
    TableModule,
  ],
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss'],
  providers: [],
  encapsulation: ViewEncapsulation.None,
})
export class StatisticsComponent implements OnInit {
  stats$!: Observable<any>;
  loading = true;
  error: string | null = null;
  reorderTableColumns = [
    { field: 'id', header: '#' },
    { field: 'name', header: 'Name' },
    { field: 'quantityPerUnitName', header: 'quantity Per Unit' },
    { field: 'supplierName', header: 'supplier Name' },
    { field: 'unitsInStock', header: 'Current Stock' },
    { field: 'reorderLevel', header: 'Reorder Level' },
    { field: 'lastOrdered', header: 'Last Ordered' },
  ];

  constructor(private statsService: ProductService) {}

  ngOnInit(): void {
    this.loadStatistics();
  }

  loadStatistics(): void {
    this.loading = true;
    this.error = null;
    this.stats$ = this.statsService.getProductStatistics();
    this.stats$.subscribe({
      next: () => {
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load statistics';
        this.loading = false;
        console.error(err);
      },
    });
  }

  refresh(): void {
    this.loadStatistics();
  }

  getSeverity(
    currentStock: number,
    reorderLevel: number
  ):
    | 'success'
    | 'secondary'
    | 'info'
    | 'warning'
    | 'danger'
    | 'contrast'
    | undefined {
    if (currentStock < reorderLevel) {
      return 'danger';
    } else if (currentStock === reorderLevel) {
      return 'warning';
    } else {
      return 'success';
    }
  }

  getOrderSeverity(count: number): string {
    if (count < 5) return 'danger';
    if (count < 15) return 'warning';
    return 'success';
  }

  log(test: any) {
    console.log(test);
  }

  getStockStatusClass(currentStock: number, reorderLevel: number): string {
    if (currentStock <= 0) return 'critical';
    if (currentStock <= reorderLevel) return 'warning';
    return 'normal';
  }
}
