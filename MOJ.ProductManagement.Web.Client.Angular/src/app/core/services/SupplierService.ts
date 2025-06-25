import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ICreateSupplierDto, IPaginatedRequest, IPaginatedResult, IResult, ISupplierDto, IUpdateSupplierDto } from '../generated/Interfaces';

@Injectable({ providedIn: 'root' })
export class SupplierService {
  private baseUrl = `${environment.apiUrl}/Suppliers`;

  constructor(private http: HttpClient) {}

  // POST: Create supplier
  createSupplier(dto: ICreateSupplierDto): Observable<any> {
    return this.http.post<any>(this.baseUrl, dto);
  }

  // PUT: Update supplier
  updateSupplier(id: number, updateDto: IUpdateSupplierDto): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, updateDto);
  }

  // GET: Get supplier by id
  getSupplier(id: number): Observable<ISupplierDto> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  // GET: Get paginated suppliers
  getSuppliers(paramsObj: IPaginatedRequest): Observable<IPaginatedResult<ISupplierDto>> {
    let params = new HttpParams();
    (Object.keys(paramsObj) as (keyof IPaginatedRequest)[]).forEach(key => {
      const value = paramsObj[key];
      if (value !== undefined && value !== null) {
        params = params.set(key as string, value as any);
      }
    });
    return this.http.get<any>(this.baseUrl, { params });
  }

  // DELETE: Delete supplier
  deleteSupplier(id: number): Observable<IResult<any>> {
    return this.http.delete<any>(`${this.baseUrl}/${id}`);
  }
}