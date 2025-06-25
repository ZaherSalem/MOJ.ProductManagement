import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
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
    paramsObj: IPaginatedRequest
  ): Observable<IPaginatedResult<IProductDto>> {
    let params = new HttpParams();
    (Object.keys(paramsObj) as (keyof IPaginatedRequest)[]).forEach((key) => {
      const value = paramsObj[key];
      if (value !== undefined && value !== null) {
        params = params.set(key as string, value as any);
      }
    });
    return this.http.get<any>(this.baseUrl, { params });
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
