import { Routes } from '@angular/router';
import { ProductListComponent } from './products/product-list/product-list.component';
import { SupplierListComponent } from './suppliers/Supplier-list/supplier-list.component';
import { StatisticsComponent } from './statistics/statistics.component';


export const routes: Routes = [
  { path: 'statistics', component: StatisticsComponent },
  { path: 'products', component: ProductListComponent },
  { path: 'suppliers', component: SupplierListComponent },
  { path: '', redirectTo: '/statistics', pathMatch: 'full' }
];