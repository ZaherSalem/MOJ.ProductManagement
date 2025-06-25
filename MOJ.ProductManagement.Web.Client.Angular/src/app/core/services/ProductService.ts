import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ICreateProductDto,
  IUpdateProductDto,
  IPaginatedRequest,
  IPaginatedResult,
  IProductStatisticsDto,
  IResult,
  IProductDto,
} from '../../core/generated/Interfaces';
@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = `${environment.apiUrl}/products`;

  constructor(private http: HttpClient) {}

  createProduct(dto: ICreateProductDto): Observable<IResult<IProductDto>> {
    return this.http.post<IResult<IProductDto>>(this.baseUrl, dto);
  }

  updateProduct(
    id: number,
    dto: IUpdateProductDto
  ): Observable<IResult<IProductDto>> {
    return this.http.put<IResult<IProductDto>>(`${this.baseUrl}/${id}`, dto);
  }

  getProduct(id: number): Observable<IResult<IProductDto>> {
    return this.http.get<IResult<IProductDto>>(`${this.baseUrl}/${id}`);
  }

  getProducts(
    params: IPaginatedRequest
  ): Observable<IPaginatedResult<IProductDto>> {
    return this.http.get<IPaginatedResult<IProductDto>>(this.baseUrl, {
      params: { ...params },
    });
  }

  deleteProduct(id: number): Observable<IResult<boolean>> {
    return this.http.delete<IResult<boolean>>(`${this.baseUrl}/${id}`);
  }

  getProductStatistics(): Observable<IResult<IProductStatisticsDto>> {
    return this.http.get<IResult<IProductStatisticsDto>>(
      `${this.baseUrl}/statistics`
    );
  }
}
