import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from './model/product';
import { ApiResponse } from '../../shared/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = "https://localhost:44372/api/";
  constructor(private httpClient: HttpClient) { }
  getAllProducts(): Observable<ApiResponse<Product[]>> {
    return this.httpClient.get<ApiResponse<Product[]>>(this.apiUrl + "product/getAll")
  }

  createProduct(request: Product): Observable<ApiResponse<Product>> {
    return this.httpClient.post<ApiResponse<Product>>(this.apiUrl + "product", request, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }
  updateProduct(id: number, request: Product): Observable<ApiResponse<Product>> {
    const url = `${this.apiUrl}product/${id}`;
    return this.httpClient.put<ApiResponse<Product>>(url, request);
  }
  getById(id: number): Observable<ApiResponse<Product>> {
    return this.httpClient.get<ApiResponse<Product>>(this.apiUrl + "product/" + id)
  }

  deleteProduct(productId: number): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}product/${productId}`);
  }
}
