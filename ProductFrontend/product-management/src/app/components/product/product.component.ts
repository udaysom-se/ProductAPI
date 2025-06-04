import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductService, Product } from '../../services/product.service';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent implements OnInit {
  products: Product[] = [];
  currentProduct: Product = { id: 0, name: '', price: 0 };
  editMode = false;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe(data => this.products = data);
  }

  addProduct(): void {
    this.productService.addProduct(this.currentProduct).subscribe(() => {
      this.loadProducts();
      this.resetForm();
    });
  }

  editProduct(product: Product): void {
    this.currentProduct = { ...product };
    this.editMode = true;
  }

  updateProduct(): void {
    this.productService.updateProduct(this.currentProduct.id, this.currentProduct).subscribe(() => {
      this.loadProducts();
      this.resetForm();
    });
  }

  deleteProduct(id: number): void {
    if (window.confirm('Are you sure you want to delete this product?')){
      this.productService.deleteProduct(id).subscribe(() => this.loadProducts());
    }
  }

  onSubmit(): void {
    if (this.editMode) {
      this.updateProduct();
    } else {
      this.addProduct();
    }
  }

  resetForm(): void {
    this.currentProduct = { id: 0, name: '', price: 0 };
    this.editMode = false;
  }
}
