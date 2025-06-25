import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Supplier } from '../../suppliers/supplier.model';
import { Observable } from 'rxjs';
import { SupplierDto } from '../../models/supplier.model';
import { CreateSupplierDto } from '../../models/CreateSupplierDto.model';
import { CreateProductDto } from '../../models/CreateProductDto';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  post<T>(url: string, body: any): Observable<T> {
    return this.http.post<T>(url, body);
  }

  // Products
  getProducts() {
    return this.http.get(`${this.apiUrl}/products`);
  }

  getProduct(id: number) {
    return this.http.get(`${this.apiUrl}/products/${id}`);
  }

  createProduct(product: CreateProductDto): Observable<any> {
    return this.post(`${this.apiUrl}/products`, product);
  }

  updateProduct(id: number, product: any) {
    return this.http.put(`${this.apiUrl}/products/${id}`, product);
  }

  deleteProduct(id: number) {
    return this.http.delete(`${this.apiUrl}/products/${id}`);
  }

  searchProducts(term: number) {
    return this.http.get(`${this.apiUrl}/products/search?term=${term}`);
  }

  getProductsNeedReorder() {
    return this.http.get(`${this.apiUrl}/products/reorder`);
  }

  getProductWithMinOrders() {
    return this.http.get(`${this.apiUrl}/products/min-orders`);
  }

  // Suppliers
  getSuppliers() {
    return this.http.get(`${this.apiUrl}/Suppliers`);
  }

  getSupplier(id: number) {
    return this.http.get(`${this.apiUrl}/Suppliers/${id}`);
  }

  createSupplier(supplier: CreateSupplierDto): Observable<any> {
    return this.post(`${this.apiUrl}/Suppliers`, supplier);
  }
  updateSupplier(id: number, supplier: any) {
    return this.http.put(`${this.apiUrl}/Suppliers/${id}`, supplier);
  }

  deleteSupplier(id: number) {
    return this.http.delete(`${this.apiUrl}/Suppliers/${id}`);
  }
  // getSuppliers(): Observable<Supplier[]> {
  //     return this.http.get<Supplier[]>(`${this.apiUrl}/Suppliers`);
  //   }

  //   getSupplier(id: number): Observable<Supplier> {
  //     return this.http.get<Supplier>(`${this.apiUrl}/Suppliers/${id}`);
  //   }
  //   addSupplier(supplier: Partial<Supplier>): Observable<Supplier> {
  //     return this.http.post<Supplier>(`${this.apiUrl}/Suppliers`, supplier);
  //   }

  //   updateSupplier(supplier: Supplier): Observable<void> {
  //     return this.http.put<void>(`${this.apiUrl}/Suppliers/${supplier.id}`, supplier);
  //   }

  //   deleteSupplier(id: number): Observable<void> {
  //     return this.http.delete<void>(`${this.apiUrl}/Suppliers/${id}`);
  //   }
}
