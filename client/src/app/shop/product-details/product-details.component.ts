import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product!: IProduct;
  constructor(private shopService: ShopService, private route: ActivatedRoute) { }
  ngOnInit(): void {
    const productId = +this.route.snapshot.paramMap.get('id')!;
    this.getProduct(productId);
  }
  getProduct(productId: number) {
    this.shopService.getProduct(productId).subscribe(response => this.product = response);
  }

}
