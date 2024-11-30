import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http'
import { IProduct } from '../shared/models/product';
import { IPagination } from '../shared/models/pagination';
import { IProductBrand } from '../shared/models/product-brand';
import { IProductType } from '../shared/models/product-type';
import { map } from 'rxjs';
import { ShopParams } from '../shared/models/shop-params';
@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    const url = new URL(this.baseUrl + 'products');

    if (shopParams.brandId) {
      url.searchParams.append('brandId', shopParams.brandId.toString())
    }
    if (shopParams.typeId) {
      url.searchParams.append('typeId', shopParams.typeId.toString());
    }
    if (shopParams.search) {
      debugger
      url.searchParams.append('searchTerm', shopParams.search);
    }
    url.searchParams.append('sort', shopParams.sort);
    url.searchParams.append('pageIndex', shopParams.pageNumber.toString());
    url.searchParams.append('pageSize', shopParams.pageSize.toString());

    return this.http.get<IPagination<IProduct>>(url.toString())
  }
  getProduct(productId: number) {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + productId);
  }
  getBrands() {
    return this.http.get<IProductBrand[]>(this.baseUrl + 'products/brands');
  }
  getTypes() {
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }

}
